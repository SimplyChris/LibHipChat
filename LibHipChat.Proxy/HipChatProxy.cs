﻿using System;using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using LibHipChat.Entities;
using LibHipChat.Proxy.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Proxy
{
    public class HipChatProxy : IHipChatProxy
    {
        private HipChatConnectionFactory _factory;
        private HipChatApiExecutor _executor;

        public ErrorModel LastError { get; set; }
        public Int32 ApiCallsRemaining { get { return _executor.ApiCallsRemaining; } }
        public int GetUserId(string email)
        {
            var list = GetUserList();


            if (!list.Any())
                return -1;

            return list.SingleOrDefault(x => x.Email == email).UserId;
        }

        public HipChatProxy (HipChatConnectionFactory factory)
        {
            _factory = factory;

            _executor = new HipChatApiExecutor();
        }
      
        public HipChatDeleteResponse DeleteUser(string userId)
        {
            var _connection = _factory.Create(ActionKey.DeleteUser);
            var actionParms = new Dictionary<string, string>
                                  {
                                      {"user_id", userId}                                      
                                  };
           
            try
            {
                var response = _executor.Execute(_connection, actionParms);

                var deserializer = new JsonModelDeserializer<HipChatDeleteResponse>();
                
                var model = deserializer.Deserialize(response.ResponseString);
                
                return model;
            }

                
            catch(WebException ex)
            {
                var model = new HipChatDeleteResponse() {WasDeleted = false};
                return model;
            }            
        }

        public NewUser CreateUser(string email, string name, string title, string is_group_admin = "0")
        {
            var connection = _factory.Create(ActionKey.CreateUser);

            var actionParms = new Dictionary<string, string>
                                  {
                                      {"email", email},
                                      {"name", name},
                                      {"title", title},
                                      {"is_group_admin", is_group_admin}
                                  };
                      
            try
            {
                var response = _executor.Execute(connection, actionParms);

                var deserializer = new JsonModelDeserializer<JsonUserModel>();

                var model = deserializer.Deserialize(response.ResponseString);
                model.DeserializeModel();

                return model.User;
            }

            catch (WebException ex)
            {
                LastError = GetError(connection);
                return new NewUser() {UserId = -1};   
            }
        }

        public User UpdateUser (NewUser user)
        {
            var currentUserState = GetUser(user.UserId);

            if (currentUserState.UserId != user.UserId)
                return new User() {UserId = -1};

            var updatedUser = new User() {UserId = -1};

            return updatedUser;

        }

        public User GetUser(int userId)
        {
            
            var actionParms = new Dictionary<string, string>
                                  {
                                      {"user_id", userId.ToString()}                                      
                                  };
            
            var _connection = _factory.Create(ActionKey.ShowUser, actionParms);

            try
            {
                var response = _executor.Execute(_connection, actionParms);

                var deserializer = new JsonModelDeserializer<JsonUserModel>();

                var model = deserializer.Deserialize(response.ResponseString);
                model.DeserializeModel();

                return model.User;
            }


            catch (WebException ex)
            {
                //var model = new HipChatDeleteResponse() { WasDeleted = false };
                return null;
            }            
        }

        private ErrorModel GetError (HipChatConnection _connection)
        {
            ResultCode resultCode;

            ResultCode.TryParse(_connection.ResponseCode, out resultCode);

            if (resultCode == ResultCode.BadRequest)
            {
                var responseString = new StreamReader(_connection.ErrorStream).ReadToEnd();
                var deserializer = new JsonModelDeserializer<JsonErrorModel>();

                var model = deserializer.Deserialize(responseString);
                model.DeserializeModel();

                LastError = model.ErrorModel;
            }

            return LastError;
        }

        public HipChatStatus MessageRoom(string roomId, string from, string message)
        {
            var connection = _factory.Create(ActionKey.MessageRoom);


            var actionParms = new Dictionary<string, string>
                                  {
                                      {"room_id", roomId},
                                      {"from", from},
                                      {"message", message}
                                  };

            

            var response = _executor.Execute(connection, actionParms);

            var deserializer = new JsonModelDeserializer<HipChatStatus>();

            var model = deserializer.Deserialize(response.ResponseString);            

            return model;
        }
        
        public IList<User> GetUserList()
        {
            var connection = _factory.Create(ActionKey.ListUsers);                       

            var response = _executor.Execute(connection, null);

            var deserializer = new JsonModelDeserializer <JsonUsersModel>();

            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.Model;
        }

        public IList<Room> GetRoomList()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            

            var response = _executor.Execute(connection,null);

            var deserializer = new JsonModelDeserializer<JsonRoomsModel>();

            var model = deserializer.Deserialize(response.ResponseString);

            model.DeserializeModel();                        

            return model.Model;
        }

        public RoomDetail GetRoomInfo(string roomId)
        {
            var actionParms = new Dictionary<string, string>() { { "room_id", roomId } };

            var connection = _factory.Create(ActionKey.ShowRoom,actionParms);            

            var response = _executor.Execute(connection, actionParms);

            var deserializer = new JsonModelDeserializer<JsonRoomDetailModel>();
            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.RoomInfo;
        }

        
    }
}
