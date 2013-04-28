using System;
using LibHipChat.Domain.Interfaces;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{    
    public class HipChatStatus : IHipChatModel 
    {
        [JsonProperty ("Status")]
        public String Status { get; set; } 
    }
}