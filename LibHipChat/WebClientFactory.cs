using System;
using System.Collections;
using System.Net;

namespace LibHipChat
{
    public class WebClientFactory
    {
        public HipChatClient CreateWebClient(String baseApiUrl, ActionKey action, IDictionary queryParams)
        {                       
            string actionUrl;
            string actionMethod;
                        
            //TODO: Put this into a domain object
            const string postActionMethod = "POST";
            const string getActionMethod = "GET";

            //TODO: Don't use switch. Implement a strategy (for get vs post also?)
            switch (action)
            {
                case ActionKey.ListRooms:
                    actionUrl = "rooms/list";
                    actionMethod = getActionMethod;
                    break;

                case ActionKey.ListUsers:
                    actionUrl = "users/list";
                    actionMethod = getActionMethod;
                    break;

//TODO: Create a custom HipChapException Type                
                default:
                    throw new NotImplementedException(String.Format("NotImplementedException: {0}", action.ToString()));
            }

            var apiUrl = new Uri(new Uri(baseApiUrl), actionUrl);
            
            var request = (HttpWebRequest) WebRequest.Create(apiUrl);
            request.Method = actionMethod;            
            return new HipChatClient(request);
        }
    }
}