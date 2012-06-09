namespace LibHipChat.Entities
{
    public abstract class RoomMessage
    {
        private RoomAction _roomAction;


        public ActionType GetActionType()
        {
            return _roomAction.Type;
        }
    }
}