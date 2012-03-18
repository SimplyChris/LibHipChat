using System;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Proxy
{
    public class HipChatDeleteResponse : IHipChatModel
    {
        [JsonProperty("deleted")]
        public Boolean WasDeleted { get; set; }
    }
}