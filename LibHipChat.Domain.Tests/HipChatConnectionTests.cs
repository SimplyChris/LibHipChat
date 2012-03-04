using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using LibHipChat.Helpers;
using LibHipChat.Proxy;
using NUnit.Framework;
using Rhino.Mocks;

namespace LibHipChat
{
    [TestFixture]
    public class HipChatConnectionTests
    {
        private HipChatConnection _connection;
        private HipChatConnectionFactory _connectionFactory;
        private string apiKey;// = "fc571e6af2aa7dc9ddd99bc043c3a8";
        private const string apiUrl = "http://api.hipchat.com/v1/";

        [SetUp]
        public void Setup ()
        {
            apiKey = System.Configuration.ConfigurationSettings.AppSettings["HipChatApiKey"];
            _connectionFactory = new HipChatConnectionFactory(apiKey, apiUrl);
        }

        [Test]
        public void should_be_able_to_execute_listusers ()
        {
            _connection = _connectionFactory.Create(ActionKey.ListUsers);

            var reader = new HipChatApiExecutor(_connection);
            var response = reader.GetResponseString();
            Assert.That(response.ToLower().Contains("<users>"));
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {
            _connection = _connectionFactory.Create(ActionKey.ListRooms);

            var reader = new HipChatApiExecutor(_connection);
            var response = reader.GetResponseString();
            Assert.That(response.ToLower().Contains("<rooms>"));
        }
        

        [Test]
        public void should_be_able_to_message_room ()
        {

            var proxy = new HipChatProxy(_connectionFactory);

            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("Integration Test [should_be_able_to_message_room] Run At: {0}",
                                        DateTime.Now.ToString());
            
            var response = proxy.MessageRoom("52400", "Automation", message);

            Assert.That(response.ResponseString, Is.EqualTo("{\"status\":\"sent\"}"));
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
            executer.Execute();
            var response = executer.GetResponseString();
            Assert.That(response.Contains("\"name\": \"Auto Created User\","), Is.True);
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

            executer.Execute();
            var response = executer.GetResponseString();
            Assert.That(response.Contains(""), Is.True);
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