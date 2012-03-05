using System;

namespace LibHipChat
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