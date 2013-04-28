using LibHipChat.Domain.Constants;
using NUnit.Framework;

namespace LibHipChat.Domain.Tests
{
    [TestFixture]
    public class HipChatConnectionFactoryTests
    {
        private HipChatConnectionFactory _connectionFactory;
        private const string _apiKey = "TheApiKey";
        private const string _baseApiUrl = "https://api.hipchat.com/v1/";
        [SetUp]
        public void Setup ()
        {
            _connectionFactory = new HipChatConnectionFactory(new HipChatConnectionSettings(_baseApiUrl, _apiKey));
        }

        [Test]
        public void should_get_valid_complete_api_url_from_connection_factory ()
        {
            const string expectedUrl = _baseApiUrl + "users/list" + "?format=json" + "&auth_token=" + _apiKey; 
            var connection = _connectionFactory.Create(ActionKey.ListUsers);


            var url = connection.ConnectionUrl;

            Assert.That(url, Is.EqualTo(expectedUrl));
        }

    }
}