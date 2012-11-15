using System;using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using LibHipChat.Domain;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain.Helpers;
using LibHipChat.Domain.Proxy;
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

        public String GetUserId(string email)
        {
            var list = GetUserList();


            if (!list.Any())
                return "";

            var user = list.SingleOrDefault(x => x.Email == email);

            return user != null ? list.SingleOrDefault(x => x.Email == email).UserId : "";
        }

        public void SetRoomTopic(string roomid, string newtopic)
        {
            throw new NotImplementedException();
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
                var response = ExecuteCall(_connection, actionParms);
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
                var response = ExecuteCall(connection, actionParms);
                var deserializer = new JsonModelDeserializer<JsonUserModel>();
                var model = deserializer.Deserialize(response.ResponseString);

                model.DeserializeModel();
                return model.User;
            }

            catch (WebException ex)
            {
                LastError = GetError(connection);
                return new NewUser() {UserId = null};   
            }
        }

        public User UpdateUser (NewUser user)
        {
            var currentUserState = GetUser(user.UserId);

            if (currentUserState.UserId != user.UserId)
                return new User() {UserId = null};

            var updatedUser = new User() {UserId = null};

            return updatedUser;

        }

        public User GetUser(String userId)
        {
            
            var actionParms = new Dictionary<string, string>
                                  {
                                      {"user_id", userId}                
                                  };
            
            var _connection = _factory.Create(ActionKey.ShowUser, actionParms);

            try
            {
                var response = ExecuteCall(_connection, actionParms);

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

            Enum.TryParse(_connection.ResponseCode, out resultCode);
            LastError = null;
            
            if (_connection.ErrorStream != null)
            {
                var responseString = new StreamReader(_connection.ErrorStream).ReadToEnd();
                var deserializer = new JsonModelDeserializer<JsonErrorModel>();

                var model = deserializer.Deserialize(responseString);
                model.DeserializeModel();

                LastError = model.ErrorModel;
            }

            return LastError;
        }

        public HipChatStatus MessageRoom(string roomId, string from, string message, MessageFormat format = MessageFormat.Html)
        {
            var connection = _factory.Create(ActionKey.MessageRoom);


            var actionParms = new Dictionary<string, string>
                                  {
                                      {"room_id", roomId},
                                      {"from", from},
                                      {"message", message},
                                      {"message_format", format.ToString()}
                                  };

            

            var response = ExecuteCall(connection, actionParms);

            var deserializer = new JsonModelDeserializer<HipChatStatus>();

            var model = deserializer.Deserialize(response.ResponseString);            

            return model;
        }

        public IList<User> GetUserList()
        {
            var connection = _factory.Create(ActionKey.ListUsers);                       

            var response = ExecuteCall(connection, null);

            var deserializer = new JsonModelDeserializer <JsonUsersModel>();

            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.Model;
        }

        public IList<Room> GetRoomList()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            

            var response = ExecuteCall(connection,null);

            var deserializer = new JsonModelDeserializer<JsonRoomsModel>();

            var model = deserializer.Deserialize(response.ResponseString);

            model.DeserializeModel();                        

            return model.Model;
        }

        public RoomDetail GetRoomInfo(string roomId)
        {
            var actionParms = new Dictionary<string, string>() { { "room_id", roomId } };

            var connection = _factory.Create(ActionKey.ShowRoom,actionParms);            

            var response = ExecuteCall(connection, actionParms);

            var deserializer = new JsonModelDeserializer<JsonRoomDetailModel>();
            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            return model.RoomInfo;
        }

        public IList<RoomMessage> GetRoomHistory(string roomid, DateTime date)
        {
            string formattedDate = date.ToString ("yyyy-MM-dd");

            return GetRoomHistory(roomid, formattedDate);
        }

        public IList<RoomMessage> GetRecentRoomHistory(string roomid)
        {
            return GetRoomHistory(roomid, "recent");
        }

        private IList<RoomMessage> GetRoomHistory (string roomid, string rangeSpecification)
        {
            var actionParms = new Dictionary<string, string>() { { "room_id", roomid }, { "date", rangeSpecification } };
            var connection = _factory.Create(ActionKey.GetRoomHistory, actionParms);
            var response = ExecuteCall(connection, actionParms);

            var deserializer = new JsonModelDeserializer<JsonRoomHistoryModel>();
            var model = deserializer.Deserialize(response.ResponseString);
            model.DeserializeModel();

            foreach (var message in model.Model)
            {
                message.MessageType = RoomMessageTypeIdentifier.GetMessageType(message);
            }
            return model.Model;
        }

        public HipChatResponse ExecuteCall (HipChatConnection connection, Dictionary<string, string>actionParms )
        {

            HipChatResponse response;
            try
            {
                response = _executor.Execute(connection, actionParms);
            }
            catch (Exception ex)
            {
                var error = GetError(connection) ?? new ErrorModel();
                throw new HipChatException(error, ex);                
            }
            return response;
        }
    }
}