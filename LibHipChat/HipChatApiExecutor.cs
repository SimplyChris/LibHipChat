﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LibHipChat.Contracts;
using LibHipChat.Entities;
using LibHipChat.Helpers;
using Newtonsoft.Json;

namespace LibHipChat
{
    public class HipChatApiExecutor
    {
        private HipChatConnection _connection;
        private IEnumerable<KeyValuePair<string, string>> _actionParms;
        private IJsonDeserializer _deserializer;

        public HipChatApiExecutor (HipChatConnection connection, IJsonDeserializer deserializer) : this (connection, deserializer, new Dictionary<string,string>())
        {                                                                                         
            
        }

        public HipChatApiExecutor(HipChatConnection connection, IJsonDeserializer deserializer, IEnumerable<KeyValuePair<string, string>> actionParms)
        {
            _connection = connection;
            _actionParms = actionParms;
            _deserializer = deserializer;
        }

        private String GetResponseString ()
        {
            var reader = new StreamReader(_connection.GetResponseStream());
            var responseString = reader.ReadToEnd();
//            responseString = responseString.Replace('"', '\'');
            
            
            
            return HttpUtility.UrlDecode(responseString);
        }
       
        
        public HipChatResponse Execute ()
        {
            WriteActionParms(_connection, _actionParms);
            var responseString = GetResponseString();            
                       
            var response = new HipChatResponse() {Model = _deserializer.Deserialize(responseString)};
            return response;
        }

        private void WriteActionParms (HipChatConnection connection, IEnumerable<KeyValuePair<string, string>> actionParms )
        {
            if (!actionParms.Any())
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