using System;
using System.Collections.Generic;
using LibHipChat.Helpers;

namespace LibHipChat
{
    public class HipChatContext
    {
        private ActionKey _action;
        private string _apiKey;

        public String BaseUrl { get; set; }
        public ActionKey Action { get; set; }
        
        public HipChatContext (String baseApiUrl, String apiKey, ActionKey action  )
        {
            _action = action;   
            _apiKey = apiKey;
            Action = action;
            BaseUrl = baseApiUrl;
        }



        public String BuildQueryString ()
        {
            var helper = new QueryStringHelper();
            helper.Add("auth_token", _apiKey);
            return "";
        }


    }
}