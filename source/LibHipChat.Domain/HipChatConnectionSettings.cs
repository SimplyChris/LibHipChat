using System;

namespace LibHipChat.Domain
{
    public class HipChatConnectionSettings
    {
        public String BaseApiUrl;
        public String AuthKey;

        public HipChatConnectionSettings(string apiUrl, string authKey)
        {
            BaseApiUrl = apiUrl;
            AuthKey = authKey;
        }
    }
}