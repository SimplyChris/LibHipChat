using LibHipChat.Entities;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    [JsonObject (MemberSerialization = MemberSerialization.OptIn)]
    public class RoomMessage
    {
        private RoomAction _roomAction;

        
        public string Message { get; set; }

        [JsonProperty("from")]
        public User User { get; set; }

        

        public ActionType GetActionType()
        {
            return _roomAction.Type;
        }
    }
}