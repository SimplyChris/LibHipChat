using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{     
    public class NewUser : User
    {
        [JsonProperty("password")]
        public String Password { get; set; }
    }
}
