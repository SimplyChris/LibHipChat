using System;
using System.Configuration;
using LibHipChat.Contracts;

namespace LibHipChat
{
    public class HipChatConnectionFactory
    {
        private HipChatConnectionSettings connectionSettings;
        private IJsonDeserializer _jsonDeserializer;

        public HipChatConnectionFactory (HipChatConnectionSettings settings)
        {
            connectionSettings = settings;
        }
        

        public HipChatConnection Create(ActionKey action)
        {
            var connection = new HipChatConnection (connectionSettings, CreateContext(action));
            
            return connection;
        }

        private HipChatContext CreateContext (ActionKey action)
        {
            return new HipChatContext(connectionSettings.BaseApiUrl, connectionSettings.AuthKey, action, HipChatResponseFormat.Json);
        }
    }
}               