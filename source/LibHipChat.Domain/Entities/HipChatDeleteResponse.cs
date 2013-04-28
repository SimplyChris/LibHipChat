using System;
using LibHipChat.Domain.Interfaces;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    public class HipChatDeleteResponse : IHipChatModel
    {
        [JsonProperty("deleted")]
        public Boolean WasDeleted { get; set; }
    }
}