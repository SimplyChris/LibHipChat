﻿using System.Collections.Generic;
using LibHipChat.Domain.Interfaces;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
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
    }
}