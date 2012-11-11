using System.Collections.Generic;

namespace LibHipChat.Domain
{
    public class HipChatConnectionFactory
    {
        private HipChatConnectionSettings connectionSettings;        

        public HipChatConnectionFactory (HipChatConnectionSettings settings)
        {
            connectionSettings = settings;
        }
        

        //TODO: Factory vs Builder
        public HipChatConnection Create (ActionKey action, IDictionary<string,string> actionParms)
        {
            var connection = new HipChatConnection(connectionSettings, CreateContext(action,actionParms));
            return connection;
        }

        public HipChatConnection Create(ActionKey action)
        {
            return Create(action, null);            
        }

        private HipChatContext CreateContext (ActionKey action, IDictionary<string,string> actionParms )
        {
            return new HipChatContext(connectionSettings.BaseApiUrl, connectionSettings.AuthKey, action, actionParms);
        }
    }
}               