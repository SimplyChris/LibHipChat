using System;using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public ErrorModel LastError { get; set; }

        public HipChatProxy (HipChatConnectionFactory factory)
        {
            _factory = factory;
        }
      
        public HipChatDeleteResponse DeleteUser(string userId)
        {
            var _connection = _factory.Create(ActionKey.DeleteUser);
            var actionParms = new Dictionary<string, string>
                                  {
                                      {"user_id", userId}                                      
                                  };
            var executer = new HipChatApiExecutor(_connection, actionParms);

            try
            {
                var response = executer.Execute();

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

            var executor = new HipChatApiExecutor(connection, actionParms);


            try
            {
                var response = executor.Execute();

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

        ErrorModel GetError (HipChatConnection _connection)
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

            var executor = new HipChatApiExecutor(connection, actionParms);

            var response = executor.Execute();

            var deserializer = new JsonModelDeserializer<HipChatStatus>();

            var model = deserializer.Deserialize(response.ResponseString);            

            return model;
        }
        
        public IList<User> GetUserList()
        {
            var connection = _factory.Create(ActionKey.ListUsers);
            
            var apiExecutor = new HipChatApiExecutor(connection);            

            var response = apiExecutor.Execute();

            var deserializer = new JsonModelDeserializer <JsonUsersModel>();

            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.Model;
        }

        public IList<Room> GetRoomList()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            var executor = new HipChatApiExecutor(connection);

            var response = executor.Execute();

            var deserializer = new JsonModelDeserializer<JsonRoomsModel>();

            var model = deserializer.Deserialize(response.ResponseString);

            model.DeserializeModel();                        

            return model.Model;
        }

        public RoomDetail GetRoomInfo(string roomId)
        {
            var actionParms = new Dictionary<string, string>() { { "room_id", roomId } };

            var connection = _factory.Create(ActionKey.ShowRoom,actionParms);

            var executor = new HipChatApiExecutor(connection,actionParms);

            var response = executor.Execute();

            var deserializer = new JsonModelDeserializer<JsonRoomDetailModel>();
            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.RoomInfo;
        }

        
    }
}
