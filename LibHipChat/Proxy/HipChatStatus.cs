using System;
using LibHipChat.Domain.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Proxy
{    
    public class HipChatStatus : IHipChatModel 
    {
        [JsonProperty ("Status")]
        public String Status { get; set; } 
    }
}