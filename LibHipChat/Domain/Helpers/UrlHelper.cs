using System;

namespace LibHipChat.Domain.Helpers
{
    public static class UrlHelper
    {
        public static String GetActionUrl(ActionKey action)
        {
            var actionUrl = "";

            switch (action)
            {
                case ActionKey.ListRooms:
                    actionUrl = "rooms/list";
                    break;


                case ActionKey.ShowRoom:
                    actionUrl = "rooms/show";
                    break;

                case ActionKey.CreateRoom:
                    actionUrl = "rooms/create";
                    break;

                case ActionKey.DeleteRoom:
                    actionUrl = "rooms/delete";
                    break;
                                    
                case ActionKey.MessageRoom:
                    actionUrl = "rooms/message";
                    break;

                case ActionKey.GetRoomHistory:
                    actionUrl = "rooms/history";
                    break;

                case ActionKey.ListUsers:
                    actionUrl = "users/list";
                    break;

                case ActionKey.ShowUser:
                    actionUrl = "users/show";
                    break;

                case ActionKey.CreateUser:
                    actionUrl = "users/create";
                    break;

                case ActionKey.DeleteUser:
                    actionUrl = "users/delete";
                    break;
                    
                case ActionKey.UpdateUser:
                    actionUrl = "users/update";
                    break;

                case ActionKey.SetTopic:
                    actionUrl = "rooms/topic";
                    break;

                    
                //TODO: Create a custom HipChapException Type                
                default:
                    throw new NotImplementedException(String.Format("NotImplementedException: {0}", action.ToString()));
            }
            return actionUrl;
        }

        public static String GetActionMethod(ActionKey action)
        {
            string actionMethod = "";

            switch (action)
            {
                case ActionKey.ListRooms:
                case ActionKey.ListUsers:
                case ActionKey.ShowRoom:
                case ActionKey.ShowUser:
                case ActionKey.GetRoomHistory:
                    actionMethod = "GET";
                    break;

                case ActionKey.DeleteUser:
                case ActionKey.CreateUser:
                case ActionKey.MessageRoom:
                case ActionKey.SetTopic:
                    actionMethod = "POST";
                    break;

                default:
                    throw new NotImplementedException(String.Format("NotImplmentedException: {0}", action.ToString()));
            }
            return actionMethod;
        } 
    }
}