using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{   
    [JsonObject (MemberSerialization.OptIn)]
    public  class User
    {
        [JsonProperty ("user_id")]
        public Int32 UserId { get; set; }

        [JsonProperty ("name")]
        public String Name { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        [JsonProperty("photo_url")]
        public String PhotoUrl { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("status_message")] 
        public String StatusMessage { get; set; }

        [JsonProperty("is_group_admin")]
        public Int32 IsGroupAdmin { get; set; }
    }
}
