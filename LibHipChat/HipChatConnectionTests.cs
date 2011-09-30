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
            var response = _connection.Execute();
        }

        [Test]
        public void should_be_able_to_execute_listrooms ()
        {
            _connection = _connectionFactory.Create(ActionKey.ListRooms);
            var response = _connection.Execute();
        }
    }
}