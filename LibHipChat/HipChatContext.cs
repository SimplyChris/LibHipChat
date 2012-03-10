using System;
using System.Collections.Generic;
using LibHipChat.Contracts;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatContext
    {
        private HipChatResponseFormat _responseFormat;

        private string _apiKey;      
        public String BaseUrl { get; set; }
        public ActionKey Action { get; set; }
        
        public HipChatContext (String baseApiUrl, String apiKey, ActionKey action, HipChatResponseFormat responseFormat = HipChatResponseFormat.Xml  )
        {
            _apiKey = apiKey;
            Action = action;
            BaseUrl = baseApiUrl;
            _responseFormat = responseFormat;
        }
    
        public String BuildQueryString ()
        {
            var helper = new QueryStringHelper();
            helper.Add("format", _responseFormat.ToString().ToLower());
            helper.Add("auth_token", _apiKey);
            return helper.HtmlStringValue;
        }
    }
}