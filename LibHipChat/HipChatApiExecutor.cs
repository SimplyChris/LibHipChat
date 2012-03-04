using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatApiExecutor
    {
        private HipChatConnection _connection;
        private IDictionary<string, string> _actionParms;

        public HipChatApiExecutor (HipChatConnection connection)
        {
            _connection = connection;            
        }

        public HipChatApiExecutor (HipChatConnection connection, IDictionary<string,string> actionParms)
        {
            _connection = connection;
            _actionParms = actionParms;
        }

        public String GetResponseString ()
        {
            var reader = new StreamReader(_connection.GetResponseStream());
            var responseString = reader.ReadToEnd();
            return responseString;
        }
       
        
        public HipChatResponse Execute ()
        {
            WriteActionParms(_connection, _actionParms);
            var response = new HipChatResponse() {ResponseString = GetResponseString()};
            return response;
        }

        private void WriteActionParms (HipChatConnection connection, IEnumerable<KeyValuePair<string, string>> actionParms )
        {
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