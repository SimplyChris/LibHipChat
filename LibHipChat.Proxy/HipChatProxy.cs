using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LibHipChat;
using LibHipChat.Entities;
using LibHipChat.Proxy.Contracts;
using Newtonsoft.Json;

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

            var executor = new HipChatApiExecutor(connection, actionParms);

            return executor.Execute();
        }
        
        public HipChatResponse GetUsers()
        {
            var connection = _factory.Create(ActionKey.ListUsers);
            var apiExecutor = new HipChatApiExecutor(connection);

            var response = apiExecutor.Execute();
            //var anon_model = JsonConvert.DeserializeAnonymousType(response.ResponseString, new {user_id = "", email = "", name = ""});
            var settings = new JsonSerializerSettings();

            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            

            var model = JsonConvert.DeserializeObject <Dictionary<String, Object>>(response.ResponseString,settings) ;

            response.Model = model;
            
            //TODO: Set the model response
            return (response);
        }

        public HipChatResponse GetRooms()
        {

            var connection = _factory.Create(ActionKey.ListRooms);
            var executor = new HipChatApiExecutor(connection);

            var response = executor.Execute();
            var memoryStream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(response.ResponseString));
            
            var serializer = new DataContractSerializer(typeof (List<Room>));

            var rooms = serializer.ReadObject(memoryStream);

            response.Model = rooms;

            return response;
        }
    }
}
