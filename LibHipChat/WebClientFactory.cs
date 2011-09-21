using System;
using System.Net;

namespace LibHipChat
{
    public class WebClientFactory
    {
        public WebClient CreateWebClient(String baseApiUrl, ActionKey action)
        {
            var actionUrl = string.Empty;
            var actionMethod = string.Empty;
            //TODO: Put this into a domain object
            string postActionMethod = "POST";
            string getActionMethod = "GET";

            //TODO: Don't use switch. Implement a strategy
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
            return new WebClient(request);
        }
    }
}