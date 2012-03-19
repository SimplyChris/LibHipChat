using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using LibHipChat.Contracts;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatConnection : WebRequest
    {
        private HttpWebRequest _webRequest;
        private HttpWebResponse _webResponse;
        private string _responseString = "";
        private Stream _stream;
        private Stream _errorStream;
        private string _responseCode = "";
        private HipChatConnectionSettings _connectionSettings;
        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }
        public String Response { get { return _responseString; } }
        public String ResponseCode { get { return _webResponse.StatusCode.ToString(); } }
        public String Method { get { return _webRequest.Method; } }
        public Stream ErrorStream { get { return _errorStream; } }
        public HipChatConnection (HipChatConnectionSettings settings, HipChatContext context)
        {
            _connectionSettings = settings;
            _webRequest = CreateWebRequest(_connectionSettings, context);
        }

        public HipChatConnectionSettings GetHipChatConnectionSettings ()
        {
            return _connectionSettings;
        }
                
        public Stream GetResponseStream ()
        {
            try
            {
               
                _webResponse = (HttpWebResponse)_webRequest.GetResponse();                
                if (_webResponse.StatusCode.ToString() == ResultCode.BadRequest.ToString())
                {
                    throw (new WebException(
                        String.Format("WebException: {0} - {1}", _webResponse.StatusCode.ToString(),
                                      _webResponse.StatusDescription), new Exception(), WebExceptionStatus.ProtocolError,
                        _webResponse));
                }
                _stream = _webResponse.GetResponseStream();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) {
                    _webResponse = (HttpWebResponse)ex.Response;

                    _errorStream = _webResponse.GetResponseStream();
                    throw;
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