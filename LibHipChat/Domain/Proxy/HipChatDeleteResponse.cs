using System;
using LibHipChat.Domain.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Proxy
{
    public class HipChatDeleteResponse : IHipChatModel
    {
        [JsonProperty("deleted")]
        public Boolean WasDeleted { get; set; }
    }
}