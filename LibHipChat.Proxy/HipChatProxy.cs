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

        public NewUser AddUser(string email, string name, string title, string is_group_admin = "0")
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

//            var user = (JsonConvert.DeserializeObject<User>(response.ResponseString));
//            var model = user; 
            var deserializer = new JsonModelDeserializer<JsonUserModel>();

            var model = deserializer.Deserialize(response.ResponseString);            
            model.DeserializeList();

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
        
        public IList<User> GetUsers()
        {
            var connection = _factory.Create(ActionKey.ListUsers);
            
            var apiExecutor = new HipChatApiExecutor(connection);            

            var response = apiExecutor.Execute();

            var deserializer = new JsonUserDeserializer <User>();

            var model = deserializer.Deserialize(response.ResponseString);
            
            return model;
        }

        public HipChatResponse GetRooms()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            var executor = new HipChatApiExecutor(connection);

            var response = executor.Execute();
            var memoryStream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(response.ResponseString));
            
            var serializer = new DataContractSerializer(typeof (List<Room>));

            var rooms = serializer.ReadObject(memoryStream);

            //response.Model = rooms;

            return response;
        }
    }
}
