using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using LibHipChat.Entities;
using LibHipChat.Helpers;
using LibHipChat.Proxy;
using LibHipChat.Proxy.Contracts;
using NUnit.Framework;

namespace LibHipChat.Proxy.Tests
{
    [TestFixture]
    public class HipChatProxyTests
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
            var users = _proxy.GetUserList();            
            Assert.That(users.Count(x => x.Email == "family@losmorgans.com") == 1);            
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {            
            var response = _proxy.GetRoomList();

            var dt = response[0].CreatedAt;

            Assert.That(response.Count(), Is.GreaterThan(1));
            Assert.That(response.Count(x => x.Name == "ZenCode"), Is.EqualTo(1));
        }
        

        [Test]
        public void should_be_able_to_message_room ()
        {         
            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("Integration Test [should_be_able_to_message_room] Run At: {0}",
                                        DateTime.Now.ToString());
            
            var status = _proxy.MessageRoom("52400", "Automation", message);


            Assert.That(status.Status, Is.EqualTo("sent"));
        }

        [Test]
        public void should_be_able_to_create_user()
        {            
            var newUser = _proxy.CreateUser("user_call_testing@losmorgans.com", "Auto Created User.", "TESTER","0");
         
            
            Assert.That(newUser.Title, Is.EqualTo("TESTER"));
            Assert.That(newUser.Password.Length, Is.GreaterThan(1));
        }

        [Test]
        public void should_be_able_to_delete_user ()
        {
            var response = _proxy.DeleteUser("user_call_testing@losmorgans.com");

            Assert.That(response.WasDeleted, Is.EqualTo(true));
        }

        [Test]
        public void should_get_proper_repsonse_for_delete_of_invalid_user ()
        {
            var response = _proxy.DeleteUser("invalid_user@losmorgans.com");
            Assert.That(response.WasDeleted, Is.EqualTo(false));
        }

        [Test]
        public void should_set_proper_error_model_when_creating_a_duplicate_user ()
        {
            try
            {
                CreateDuplicateUser();
                CreateDuplicateUser();
                                
                Assert.That(_proxy.LastError.ErrorResult, Is.EqualTo(ResultCode.BadRequest));
                Assert.That(_proxy.LastError.Message, Is.EqualTo("Email already in use"));
            }
            catch (WebException ex)
            {
                var lastError = _proxy.LastError;

                Assert.That(lastError.ErrorResult, Is.EqualTo(ResultCode.BadRequest));
            }
            finally
            {
                var userList = _proxy.GetUserList();

                var dupUserId = userList.SingleOrDefault(x => x.Email == "duptest@tzoc.org").UserId.ToString();
                _proxy.DeleteUser(dupUserId);
            }
        }

        private void CreateDuplicateUser ()
        {
            _proxy.CreateUser("duptest@tzoc.org", "Duplicate User", "Minion", "0");
        }

        [Test]
        public void should_be_able_to_get_room_info ()
        {
            var roomId = "52403";
            var expectedName = "Notifications";
            var result = _proxy.GetRoomInfo(roomId);

            Assert.That(result.Name, Is.EqualTo(expectedName));
            Assert.That(result.Participants.Count(x => x.Name == "Dharma Soft"), Is.EqualTo(1));
        }

        [Test]
        public void should_be_able_to_get_user ()
        {            
            var expectedEmail = "testing@losmorgans.com";            

            var result = _proxy.GetUser(80300);

            

            Assert.That(result.Email, Is.EqualTo(expectedEmail));           
        }

        [Test]
        public void should_get_correct_user_id ()
        {
            var expectedUserId = 80295;


            var id = _proxy.GetUserId("family@losmorgans.com");

            Assert.That(id, Is.EqualTo(expectedUserId));

        }

        [Test]
        public void api_calls_remaning_should_decrease ()
        {            
            var roomList = _proxy.GetRoomList();
            var beforeCallsRemaining = _proxy.ApiCallsRemaining;

            var room = _proxy.GetRoomInfo(roomList[0].Id.ToString());

            Assert.That(beforeCallsRemaining, Is.GreaterThan(_proxy.ApiCallsRemaining));
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