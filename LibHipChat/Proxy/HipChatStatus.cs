using System;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Proxy
{    
    public class HipChatStatus : IHipChatModel 
    {
        [JsonProperty ("Status")]
        public String Status { get; set; } 
    }
}