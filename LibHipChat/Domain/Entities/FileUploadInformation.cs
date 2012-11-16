using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    [JsonObject ("file", MemberSerialization = MemberSerialization.OptIn)]    
    public class FileUploadInformation
    {
        [JsonProperty ("size")]
        public Int32 Size { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }     
    }
}