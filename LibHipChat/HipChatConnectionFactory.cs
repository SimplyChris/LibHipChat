using System;
using System.Configuration;

namespace LibHipChat
{
    public class HipChatConnectionFactory
    {
        private HipChatConnectionSettings connectionSettings;

        public HipChatConnectionFactory (HipChatConnectionSettings settings)
        {
//            var apiKey = ConfigurationManager.AppSettings["HipChatApiKey"];
//            var apiUrl = ConfigurationManager.AppSettings["HipChatApiUrl"];
            connectionSettings = settings;
        }
        

        public HipChatConnection Create(ActionKey action)
        {
            var connection = new HipChatConnection(connectionSettings, CreateContext(action));
            
            return connection;
        }

        private HipChatContext CreateContext (ActionKey action)
        {
            return new HipChatContext(connectionSettings.BaseApiUrl, connectionSettings.AuthKey, action, HipChatResponseFormat.Json);
        }
    }
}               