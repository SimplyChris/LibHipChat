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
        private HipChatConnectionSettings _connectionSettings;
        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }
        public String Response { get { return _responseString; } }
        

        public HipChatConnection (HipChatConnectionSettings settings, HipChatContext context)
        {

            _webRequest = CreateWebRequest(settings, context);
        }
                
        public Stream GetResponseStream ()
        {
            try
            {
               
                var response = (HttpWebResponse)_webRequest.GetResponse();
                _stream = response.GetResponseStream();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) {
                    var response = (HttpWebResponse)ex.Response;

                    _stream = response.GetResponseStream();
                }                
            }

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
        
        private HttpWebRequest CreateWebRequest (HipChatConnectionSettings settings, HipChatContext context)
        {
            var apiCallUrl = new Uri(new Uri(settings.BaseApiUrl), UrlHelper.GetActionUrl(context.Action) + context.BuildQueryString());
            var webRequest = _webRequest = (HttpWebRequest)Create(apiCallUrl);
            webRequest.Method = UrlHelper.GetActionMethod(context.Action);

            return (webRequest);
        }
    }
}