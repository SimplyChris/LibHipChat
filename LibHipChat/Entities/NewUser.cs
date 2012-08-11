using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{     
    public class NewUser : User
    {
        [JsonProperty("password")]
        public String Password { get; set; }
    }
}
