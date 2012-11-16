using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{   
    [JsonObject ("user", MemberSerialization = MemberSerialization.OptIn)]
    public  class User : HipChatModel
    {
        [JsonProperty ("user_id")]
        public String UserId { get; set; }

        [JsonProperty ("name")]
        public String Name { get; set; }

        [JsonProperty ("email")]
        public String Email { get; set; }

        [JsonProperty ("title")]
        public String Title { get; set; }

        [JsonProperty ("photo_url")]
        public String PhotoUrl { get; set; }

        [JsonProperty ("status")]
        public String Status { get; set; }

        [JsonProperty("status_message")]
        public String StatusMessage { get; set; }

        [JsonProperty("is_group_admin")]
        public Int32 IsGroupAdmin { get; set; }
    }
}
