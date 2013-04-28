using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    public class HttpCallResponse 
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}