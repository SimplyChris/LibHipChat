using LibHipChat.Contracts;

namespace LibHipChat.Entities
{
    public class UserMessage : RoomMessage
    {
        private RoomAction _roomAction;
            
        public ActionType GetActionType()
        {
            return _roomAction.Type;
        }
    }
}