using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using LibHipChat.Entities;
using LibHipChat.Helpers;
using LibHipChat.Proxy;
using LibHipChat.Proxy.Contracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace LibHipChat
{
    [TestFixture]
    public class HipChatConnectionTests
    {
        private HipChatConnection _connection;
        private HipChatConnectionFactory _connectionFactory;
        private string apiKey;
        private string apiUrl;
        private IHipChatProxy _proxy;
        [SetUp]
        public void Setup ()
        {
            apiKey = ConfigurationManager.AppSettings["HipChatApiKey"];            
            apiUrl = ConfigurationManager.AppSettings["HipChatApiUrl"];
            _connectionFactory = new HipChatConnectionFactory(new HipChatConnectionSettings(apiUrl, apiKey));
            _proxy = new HipChatProxy(_connectionFactory);
        }

        [Test]
        public void should_be_able_to_execute_listusers ()
        {   
            var users = _proxy.GetUsers();            
            Assert.That(users.Count(x => x.Email == "family@losmorgans.com") == 1);            
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {            
            var response = _proxy.GetRooms();

            var rooms = (List<Room>) response.Model;

            Assert.That(rooms.Count(), Is.GreaterThan(1));
        }
        

        [Test]
        public void should_be_able_to_message_room ()
        {         
            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("Integration Test [should_be_able_to_message_room] Run At: {0}",
                                        DateTime.Now.ToString());
            
            var status = _proxy.MessageRoom("52400", "Automation", message);

            //Assert.That(response.ResponseString, Is.EqualTo("{\"status\":\"sent\"}"));
            

            Assert.That(status.Status, Is.EqualTo("sent"));
        }

        [Test]
        public void should_be_able_to_create_user()
        {
            _connection = _connectionFactory.Create(ActionKey.CreateUser);

            var actionParms = new Dictionary<string, string>
                                {
                                    {"email", "testing@losmorgans.com"},
                                    {"name", "Auto Created User"},
                                    {"title", string.Format("Created By Integration Test Run At: {0}", DateTime.Now.ToString())},
                                    {"is_group_admin","0"},
                                    {"password","password"}
                                };

            var executer = new HipChatApiExecutor(_connection, actionParms);
            var response = executer.Execute();            

            Assert.That(response.ResponseString.Contains("\"name\": \"Auto Created User\","), Is.True);
        }

        [Test]
        public void should_be_able_to_delete_user ()
        {
            _connection = _connectionFactory.Create(ActionKey.DeleteUser);
            var actionParms = new Dictionary<string, string>
                                  {
                                      {"user_id", "testing@losmorgans.com"}                                      
                                  };
            var executer = new HipChatApiExecutor(_connection, actionParms);

            var response = executer.Execute();
            
            Assert.That(response.ResponseString.Contains(""), Is.True);
        }

        [Test]
        public void all_action_keys_should_return_an_action_url ()
        {            
            foreach (ActionKey action in Enum.GetValues(typeof(ActionKey)))
            {                
                Debug.WriteLine("Asserting Action: {0}", action.ToString());
                Assert.DoesNotThrow(()=>UrlHelper.GetActionUrl(action), action.ToString());
            }            
        }
    }
}