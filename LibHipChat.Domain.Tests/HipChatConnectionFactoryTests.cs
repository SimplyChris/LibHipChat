using NUnit.Framework;

namespace LibHipChat.Domain.Tests
{
    [TestFixture]
    public class HipChatConnectionFactoryTests
    {
        private HipChatConnectionFactory _connectionFactory;
        private const string _apiKey = "TheApiKey";
        private const string _baseApiUrl = "https://api.hipchat.com/v1";
        [SetUp]
        public void Setup ()
        {
            _connectionFactory = new HipChatConnectionFactory(_apiKey, _baseApiUrl);
        }

        [Test]
        public void should_get_valid_complete_api_url_from_connection_factory ()
        {
            var connection = _connectionFactory.Create(ActionKey.ListUsers);

            var url = connection.ConnectionUrl;
        }

    }
}