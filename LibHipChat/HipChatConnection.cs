using System;
using System.Net;

namespace LibHipChat
{
    public class HipChatConnection
    {
        private HttpWebRequest _webRequest;

        public String ConnectionUrl { get { return _webRequest.RequestUri.ToString(); } }

        public HipChatConnection (String baseApiUrl, HipChatContext context)
        {
            _webRequest = CreateWebRequest(baseApiUrl, context);            
        }

        private HttpWebRequest CreateWebRequest (String baseApiUrl, HipChatContext context)
        {
            var apiUrl = new Uri(new Uri(baseApiUrl), GetActionUrl(context.Action));
            var webRequest = _webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            webRequest.Method = GetActionMethod(context.Action);

            return (webRequest);
        }

        private String GetActionUrl(ActionKey action)
        {
            var actionUrl = "";

            switch (action)
            {
                case ActionKey.ListRooms:
                    actionUrl = "rooms/list";
                    break;

                case ActionKey.ListUsers:
                    actionUrl = "users/list";
                    break;

                //TODO: Create a custom HipChapException Type                
                default:
                    throw new NotImplementedException(String.Format("NotImplementedException: {0}", action.ToString()));
            }
            return actionUrl;
        }

        private String GetActionMethod(ActionKey action)
        {
            string actionMethod = "";

            switch (action)
            {

                case ActionKey.ListRooms:
                case ActionKey.ListUsers:
                    actionMethod = "GET";
                    break;

                default:
                    throw new NotImplementedException(String.Format("NotImplmentedException: {0}", action.ToString()));
            }
            return actionMethod;
        }
    }
}