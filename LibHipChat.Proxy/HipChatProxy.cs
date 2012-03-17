using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public HipChatProxy (HipChatConnectionFactory factory)
        {
            _factory = factory;
        }
      
        public HipChatResponse DeleteUser(string userId)
        {
            throw new NotImplementedException();
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

            var response = executor.Execute();

            var deserializer = new JsonModelDeserializer<JsonUserModel>();

            var model = deserializer.Deserialize(response.ResponseString);            
            model.DeserializeModel();

            return model.User;
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

            response.Model = model;

            return (HipChatStatus) response.Model;
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
            

            //response.Model = rooms;

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
