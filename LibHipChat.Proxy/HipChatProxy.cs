using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using LibHipChat;
using LibHipChat.Entities;
using LibHipChat.Proxy.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibHipChat.Proxy
{
    public class HipChatProxy : IHipChatProxy
    {
        private HipChatContext _context;
        private HipChatConnectionFactory _factory;

        public HipChatProxy (HipChatConnectionFactory factory)
        {
            _factory = factory;
        }
      
        public HipChatResponse DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public HipChatResponse AddUser(string userId)
        {
            throw new NotImplementedException();
        }

        public HipChatResponse MessageRoom(string roomId, string from, string message)
        {
            var connection = _factory.Create(ActionKey.MessageRoom);


            var actionParms = new Dictionary<string, string>
                                  {
                                      {"room_id", roomId},
                                      {"from", from},
                                      {"message", message}
                                  };

            var executor = new HipChatApiExecutor(connection, new JsonUserDeserializer(), actionParms);

            return executor.Execute();
        }
        
        public IList<User> GetUsers()
        {
            var connection = _factory.Create(ActionKey.ListUsers);
            var apiExecutor = new HipChatApiExecutor(connection, new JsonUserDeserializer());

            var response = apiExecutor.Execute();            
                       
            var userList = new List<User>();
            foreach (var dictionary in response.Model.Data)
            {
                var user = DeserializeUser(dictionary);
                userList.Add(user);
            }
            return (userList);
        }

        //TODO: Refactor Me
        private User DeserializeUser (Dictionary<string,string> dictionary)
        {

            var user = new User()
                           {
                               Email = dictionary["email"].ToString(),
                               Name = dictionary["name"].ToString(),
                               Title = dictionary["title"].ToString(),
                               UserId = Convert.ToInt32(dictionary["user_id"].ToString()),
                               Status = dictionary["status"].ToString(),
                               PhotoUrl = dictionary["photo_url"].ToString(),
                               StatusMessage = dictionary["status_message"].ToString()
                           };

            return user;
        }

        public HipChatResponse GetRooms()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            var executor = new HipChatApiExecutor(connection, new JsonUserDeserializer());

            var response = executor.Execute();
            var memoryStream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(response.ResponseString));
            
            var serializer = new DataContractSerializer(typeof (List<Room>));

            var rooms = serializer.ReadObject(memoryStream);

            //response.Model = rooms;

            return response;
        }
    }
}
