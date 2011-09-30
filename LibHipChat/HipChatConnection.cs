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
        
        
        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }
        public String Response { get { return _responseString; } }

        public HipChatConnection (String baseApiUrl, HipChatContext context)
        {
            _webRequest = CreateWebRequest(baseApiUrl, context);
        }

        public XDocument Execute ()
        {
            var response =  (HttpWebResponse)_webRequest.GetResponse();                        
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            _responseString = reader.ReadToEnd();

            var xDoc = XDocument.Parse(_responseString);

            return xDoc;
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