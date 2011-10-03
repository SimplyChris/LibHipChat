using System.Collections.Generic;
using NUnit.Framework;

namespace LibHipChat
{
    [TestFixture]
    public class HipChatConnectionTests
    {
        private HipChatConnection _connection;
        private HipChatConnectionFactory _connectionFactory;
        private const string apiKey = "fc571e6af2aa7dc9ddd99bc043c3a8";
        private const string apiUrl = "https://api.hipchat.com/v1/";

        [SetUp]
        public void Setup ()
        {
            _connectionFactory = new HipChatConnectionFactory(apiKey, apiUrl);
        }

        [Test]
        public void should_be_able_to_execute_listusers ()
        {
            _connection = _connectionFactory.Create(ActionKey.ListUsers);

            var reader = new HipChatApiExecutor(_connection);
            var response = reader.GetResponseString();
            
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {
            _connection = _connectionFactory.Create(ActionKey.ListRooms);

            var reader = new HipChatApiExecutor(_connection);

            var response = reader.GetResponseString();            
        }

        [Test]
        public void should_be_able_to_message_room ()
        {
            _connection = _connectionFactory.Create(ActionKey.MessageRoom);

            var actionParms = new Dictionary<string, string>();

            actionParms.Add("room_id", "30937");
            actionParms.Add("from", "Test");
            actionParms.Add("message", "Test Message. This is one.");

            var executer = new HipChatApiExecutor(_connection, actionParms);

            executer.WriteActionParms();
                        
            var response = executer.GetResponseString();

            Assert.That(response.Contains(">sent<"), Is.True);
        }



    }
}