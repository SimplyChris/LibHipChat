using System;

namespace LibHipChat
{
    public class HipChatConnectionFactory
    {

        private readonly string _apiKey;
        private readonly string _baseUrl;

        public HipChatConnectionFactory (String apiKey, String baseUrl)
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl;
        }

        public HipChatConnection Create(ActionKey action)
        {
            var connection = new HipChatConnection(_baseUrl, CreateContext(action));
            return connection;
        }

        private HipChatContext CreateContext (ActionKey action)
        {
            return new HipChatContext(_baseUrl, _apiKey, action);
        }
    }
}               