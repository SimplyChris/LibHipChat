using System.Collections.Generic;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    public class JsonUserModel : IJsonModel
    {
        [JsonProperty("users")]
        public IList<Dictionary<string, string>> Data { get; set; }        
    }
}