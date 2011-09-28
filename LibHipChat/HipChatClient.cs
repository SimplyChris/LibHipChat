using System.Net;

namespace LibHipChat
{
    public class HipChatClient
    {
        private HttpWebRequest _webRequest;
        //TODO: Use proper configuration 
        private const string _apiBaseUrl = "https://api.hipchat.com/v1";

        public HipChatClient ()
        {
            
        }

        public HipChatClient(HttpWebRequest request)
        {
            _webRequest = request;
        }        
    }
}