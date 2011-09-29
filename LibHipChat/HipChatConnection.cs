using System;
using System.Net;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatConnection : WebRequest
    {
        private HttpWebRequest _webRequest;


        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }

        public HipChatConnection (String baseApiUrl, HipChatContext context)
        {
            _webRequest = CreateWebRequest(baseApiUrl, context);            
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