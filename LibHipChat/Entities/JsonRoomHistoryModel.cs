using System.Collections.Generic;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    public class JsonRoomHistoryModel : IJsonModel<IList<IDictionary<string,string>>>
    {
        public IList<IDictionary<string, string>> Data { get; set; }
        
        [JsonProperty("messages")]        
        private  IList<RoomMessage> RoomMessageData { get;set; }
        

        public IList<RoomMessage> Model { get; set; } 
        
        public void DeserializeModel()
        {
            Model = RoomMessageData;

        }

        private RoomMessage DeserializeListItem (Dictionary<string,string> messageItem)
        {
            return new UserMessage() {Message = messageItem["message"]};

        }
    }
}