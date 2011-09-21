using System.Net;

namespace LibHipChat
{
    public class WebClient
    {
        private HttpWebRequest _webRequest;
        //TODO: Use proper configuration 
        private const string _apiBaseUrl = "https://api.hipchat.com/v1";

        public WebClient ()
        {

            
        }
        
        public HttpWebRequest CreateWebRequest ()
        {
            //TODO: Append rest url somehow
            var requestUrl = _apiBaseUrl;
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            return request;
        }
    }
}