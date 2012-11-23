using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using LibHipChat.Domain;
using LibHipChat.Domain.Helpers;
using LibHipChat.Proxy;
using LibHipChat.Proxy.Contracts;
using NUnit.Framework;

namespace LibHipChat.Proxy.Tests
{
    [TestFixture]
    public class HipChatProxyTests
    {
        private HipChatConnectionFactory _connectionFactory;
        private string apiKey;
        private string apiUrl;
        private string testRoomId;
        private string testRoomName;

        private string testUserId;
        private string testUserEmail;
        
        private IHipChatProxy _proxy;
        private DateTime _historyStartDate = DateTime.Now - new TimeSpan(15, 0, 0, 0);

        [SetUp]
        public void Setup ()
        {
            apiKey = ConfigurationManager.AppSettings["HipChatApiKey"];            
            apiUrl = ConfigurationManager.AppSettings["HipChatApiUrl"];
            testRoomId = ConfigurationManager.AppSettings["TestRoomId"];
            testRoomName= ConfigurationManager.AppSettings["TestRoomName"];
            testUserId = ConfigurationManager.AppSettings["TestUserId"];
            testUserEmail = ConfigurationManager.AppSettings["TestUserEmail"];
            _connectionFactory = new HipChatConnectionFactory(new HipChatConnectionSettings(apiUrl, apiKey));
            _proxy = new HipChatProxy(_connectionFactory);            
        }

        [Test]
        public void should_be_able_to_execute_listusers ()
        {   
            var users = _proxy.GetUserList();            
            Assert.That(users.Count(x => x.Email == testUserEmail) == 1);            
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {            
            var response = _proxy.GetRoomList();

            
            Assert.That(response.Count(), Is.GreaterThan(1));
            Assert.That(response.Count(x => x.Name.Contains("API Development")), Is.EqualTo(1));
            Assert.That(response[0].LastActiveAt > response[0].CreatedAt);
        }
        

        [Test]
        public void should_be_able_to_message_room ()
        {         
            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("Integration Test [should_be_able_to_message_room] Run At: {0}",
                                        DateTime.Now.ToString());
            
            var status = _proxy.MessageRoom(testRoomId, "Automation", message);


            Assert.That(status.Status, Is.EqualTo("sent"));
        }

        [Test]
        public void should_be_able_to_send_mention_message ()
        {
            string.Format("Integration Test Run At: {0}", DateTime.Now.ToString());
            var message = string.Format("@DharmaSoft :-) -- Integration Test [should_be_able_to_send_mention_message] Run At: {0}",
                                        DateTime.Now.ToString());

            var status = _proxy.MessageRoom(testRoomId, "Automation", message, MessageFormat.Text);


            Assert.That(status.Status, Is.EqualTo("sent"));
        }

        [Test]
        public void should_be_able_to_create_user()
        {            
            var newUser = _proxy.CreateUser("user_call_testing@xx11.com", "Auto Created User.", "TESTER","0");
         
            
            Assert.That(newUser.Title, Is.EqualTo("TESTER"));
            Assert.That(newUser.Password.Length, Is.GreaterThan(1));
        }

        [Test]
        public void should_be_able_to_delete_user ()
        {
            var response = _proxy.DeleteUser("user_call_testing@xx11.com");

            Assert.That(response.WasDeleted, Is.EqualTo(true));
        }

        [Test]
        public void should_throw_hip_chat_exception_for_delete_of_invalid_user ()
        {
            Assert.Throws<HipChatException>(() => _proxy.DeleteUser("invalid_user@xx11.com"));           
        }

        [Test]
        public void should_throw_hip_chat_exception_on_duplicate_user ()
        {
            try
            {
                CreateDuplicateUser();
                Assert.Throws<HipChatException>(CreateDuplicateUser);
                                
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
            var expectedTopic = "API Development";
            
            var result = _proxy.GetRoomInfo(testRoomId);    

            Assert.That(result.Name, Is.EqualTo(testRoomName));            
            Assert.That(result.Topic.Contains(expectedTopic),Is.True);            
        }

        [Test]
        public void set_room_topic_should_return_proper_model ()
        {
            var newtopic = "API Development";
            var response = _proxy.SetRoomTopic(testRoomId, newtopic);

            Assert.That(response.Status, Is.EqualTo("ok"));        
        }


        [Test]
        public void should_set_room_topic()
        {            
            var newtopic = "API Development new room topic set at " + DateTime.Now.ToString();
            var response = _proxy.SetRoomTopic(testRoomId, newtopic);

            Thread.Sleep(1000);
            var roomInfo = _proxy.GetRoomInfo(testRoomId);

            Assert.That(roomInfo.Topic, Is.EqualTo(newtopic));
        }

        [Test]
        public void should_be_able_to_get_user ()
        {            

            var result = _proxy.GetUser(testUserId);
          
            Assert.That(result.Email, Is.EqualTo(testUserEmail));           
        }

        [Test]
        public void should_get_correct_user_id ()
        {
            var expectedUserId = "80295";


            var id = _proxy.GetUserId(testUserEmail);

            Assert.That(id, Is.EqualTo(testUserId));
        }

        [Test]
        public void should_return_empty_string_when_asking_for_invalid_user_id ()
        {
            Assert.That(_proxy.GetUserId("invalid@invalid.com"), Is.EqualTo(""));
        }

        [Test]
        public void should_get_recent_room_history ()
        {
            
            var response = _proxy.GetRecentRoomHistory(testRoomId);            
            Assert.That(response.Count, Is.GreaterThan(0));
            Assert.That(response[0].Message != null, Is.EqualTo(true));
            Assert.That(response[0].Message.Length, Is.GreaterThan(0));
        }

        [Test]
        public void should_get_room_history_by_date ()
        {

            var response = _proxy.GetRoomHistory(testRoomId,new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) );

            //Assert.That(response.Count, Is.GreaterThan(0));
        }

        [Test, Ignore]
        public void should_set_proper_message_type_user_message ()
        {

            var response = _proxy.GetRecentRoomHistory(testRoomId);
            var userMessage = response.SingleOrDefault(x => x.Message == "IT Test Message 1");
            Assert.That(userMessage.MessageType, Is.EqualTo(RoomMessageType.UserMessage));
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