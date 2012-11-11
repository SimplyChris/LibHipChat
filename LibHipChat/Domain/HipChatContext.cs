using System;
using System.Collections.Generic;
using LibHipChat.Domain.Helpers;

namespace LibHipChat.Domain
{
    public class HipChatContext
    {
        private HipChatResponseFormat _responseFormat;

        private string _apiKey;
        private IDictionary<string, string> _actionParms; 
        public String BaseUrl { get; set; }
        public ActionKey Action { get; set; }
        

        public HipChatContext (String baseApiUrl, String apiKey, ActionKey action, IDictionary<string,string> parms, HipChatResponseFormat responseFormat = HipChatResponseFormat.Json )
        {
            _apiKey = apiKey;
            Action = action;
            BaseUrl = baseApiUrl;
            _responseFormat = responseFormat;
            _actionParms = (parms != null ? parms : new Dictionary<string, string>());
        }
    
        public String BuildQueryString ()
        {
            var helper = new QueryStringHelper();
            helper.Add("format", _responseFormat.ToString().ToLower());
            
            helper.Add("auth_token", _apiKey);

            foreach (var key in _actionParms)
            {
                helper.Add(key.Key, key.Value);
            }

            return helper.HtmlStringValue;
        }
    }
}