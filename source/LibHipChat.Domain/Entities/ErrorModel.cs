using System;
using LibHipChat.Domain.Interfaces;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    [JsonObject ("error", MemberSerialization = MemberSerialization.OptIn)]
    public class ErrorModel : IHipChatModel
    {
        public ResultCode ErrorResult { get; set; }

        [JsonProperty("type")]
        public String ErrorType { get; set; }

        [JsonProperty("message")]
        public String Message { get; set; }
    }
}