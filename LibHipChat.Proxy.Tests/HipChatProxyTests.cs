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

            
            Assert.That(response.Count(), Is.GreaterThan(1));
            Assert.That(response.Count(x => x.Name == "ZenCode"), Is.EqualTo(1));
            Assert.That(response[0].LastActiveAt > response[0].CreatedAt);
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
        public void should_be_able_to_send_mention_message ()
        {
            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("@DharmaSoft :-) -- Integration Test [should_be_able_to_send_mention_message] Run At: {0}",
                                        DateTime.Now.ToString());

            var status = _proxy.MessageRoom("52400", "Automation", message, MessageFormat.Text);


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
            var expectedTopic = "Room For Test Notifications";
            
            var result = _proxy.GetRoomInfo(roomId);

            Assert.That(result.Name, Is.EqualTo(expectedName));            
            Assert.That(result.Topic, Is.EqualTo(expectedTopic));            
        }

        [Test]
        public void should_be_able_to_get_user ()
        {            
            var expectedEmail = "testing@losmorgans.com";            

            var result = _proxy.GetUser("80300");
          
            Assert.That(result.Email, Is.EqualTo(expectedEmail));           
        }

        [Test]
        public void should_get_correct_user_id ()
        {
            var expectedUserId = "80295";


            var id = _proxy.GetUserId("family@losmorgans.com");

            Assert.That(id, Is.EqualTo(expectedUserId));
        }

        [Test]
        public void should_return_empty_string_when_asking_for_invalid_user_id ()
        {
            Assert.That(_proxy.GetUserId("invalid@invalid.com"), Is.EqualTo(""));
        }

        [Test]
        public void test_api_user ()
        {
            var user = _proxy.GetUser("api");
            Assert.Pass();
        }

        [Test]
        public void should_get_recent_room_history ()
        {
            var roomId = "52403";
            
            var response = _proxy.GetRecentRoomHistory(roomId);            
            Assert.That(response.Count, Is.GreaterThan(0));
            Assert.That(response[0].Message != null, Is.EqualTo(true));
            Assert.That(response[0].Message.Length, Is.GreaterThan(0));
        }

        [Test]
        public void should_get_room_history_by_date ()
        {
            var roomId = "52400";

            var response = _proxy.GetRoomHistory(roomId, new DateTime(2012, 8, 5));

            Assert.That(response.Count, Is.GreaterThan(0));
        }

        [Test]
        public void should_get_file_information_from_upload_message_type ()
        {
            var roomId = "52403";

            var response = _proxy.GetRecentRoomHistory(roomId);
            var fileUploadMessage = response.SingleOrDefault(x => x.Message == "File Upload 1");

            Assert.That(fileUploadMessage.Message == "File Upload 1", Is.True);
            Assert.That(fileUploadMessage.UploadInformation.Name, Is.EqualTo("UML Quick Reference 2.pdf"));           
        }

        [Test]
        public void should_set_proper_message_type_for_file_upload ()
        {
            var roomId = "52400";

            var response = _proxy.GetRecentRoomHistory(roomId);
            var fileUploadMessage = response.SingleOrDefault(x => x.Message == "IT Test Upload 1");
            
            Assert.That(fileUploadMessage.MessageType, Is.EqualTo(RoomMessageType.FileUpload));
        }

        [Test]
        public void should_set_proper_message_type_user_message ()
        {
            var roomId = "52400";

            var response = _proxy.GetRecentRoomHistory(roomId);
            var userMessage = response.SingleOrDefault(x => x.Message == "IT Test Message 1");
            Assert.That(userMessage.MessageType, Is.EqualTo(RoomMessageType.UserMessage));
        }

        [Test]
        public void should_set_proper_message_type_for_api_message ()
        {
            var roomId = "52400";
            var response = _proxy.GetRecentRoomHistory(roomId);
            var apiMessage = response.SingleOrDefault(x => x.Message.Contains("[should_be_able_to_send_mention_message] Run At: 8/5/2012 1:09:30 AM"));
            Assert.That(apiMessage.MessageType, Is.EqualTo(RoomMessageType.ApiMessage));
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