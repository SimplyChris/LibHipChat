using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatConnection : WebRequest
    {
        private HttpWebRequest _webRequest;
        private string _responseString = "";
        private Stream _stream;
        
        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }
        public String Response { get { return _responseString; } }
        
        public HipChatConnection (String baseApiUrl, HipChatContext context)
        {
            _webRequest = CreateWebRequest(baseApiUrl, context);
        }
        
        public Stream GetResponseStream ()
        {
            var response = (HttpWebResponse)_webRequest.GetResponse();            
            _stream = response.GetResponseStream();
            return _stream;
        }

        public HttpWebRequest GetRequest ()
        {
            return _webRequest;
        }

        public new Stream GetRequestStream ()
        {            
            return _webRequest.GetRequestStream();
        }
        
        private HttpWebRequest CreateWebRequest (String baseApiUrl, HipChatContext context)
        {
            var apiUrl = new Uri(new Uri(baseApiUrl), UrlHelper.GetActionUrl(context.Action) + context.BuildQueryString());
            var webRequest = _webRequest = (HttpWebRequest)Create(apiUrl);
            webRequest.Method = UrlHelper.GetActionMethod(context.Action);

            return (webRequest);
        }
    }
}