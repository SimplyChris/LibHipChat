using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LibHipChat.Domain.Helpers;

namespace LibHipChat.Domain.Services
{
    public class HipChatApiExecutor
    {
        private HipChatConnection _connection;
        private IEnumerable<KeyValuePair<string, string>> _actionParms;
        private int _apiCallsRemaining;

        public Int32 ApiCallsRemaining { get { return _apiCallsRemaining; } }
        private String GetResponseString ()
        {
            var reader = new StreamReader(_connection.GetResponseStream());
            var responseString = reader.ReadToEnd();
            
                       
            return HttpUtility.UrlDecode(responseString);
        }       

        public HipChatResponse Execute (HipChatConnection connection, IEnumerable<KeyValuePair<string,string>> actionParms)
        {
            _connection = connection;
            _actionParms = actionParms ?? new Dictionary<string, string>();

            WriteActionParms(_connection, _actionParms);
            var responseString = GetResponseString();

            var headers = _connection.GetWebHeaders();

            Int32.TryParse(headers.Get("X-RateLimit-Remaining"), out _apiCallsRemaining);

            var response = new HipChatResponse() { ResponseString = responseString, ApiCallsRemaining = _apiCallsRemaining };

            return response;
        }
       
        
        private void WriteActionParms (HipChatConnection connection, IEnumerable<KeyValuePair<string, string>> actionParms )
        {
            if (!actionParms.Any() || connection.Method.ToUpper() == "GET")
                return;

            var request = connection.GetRequest();

            var queryStringHelper = new QueryStringHelper();


            foreach (var kvPair in actionParms)
            {
                queryStringHelper.Add(kvPair.Key, HttpUtility.UrlEncode(kvPair.Value));
            }
            
            var postString = queryStringHelper.PostStringValue;
            var bytes = System.Text.Encoding.UTF8.GetBytes(postString);

            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "HipChat API Client";
            request.ContentLength = bytes.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
        }
    }
}