using System;
using System.Collections.Generic;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    public class JsonUserModel : IJsonModel <IDictionary<string,string>>
    {
        [JsonProperty("user")]
        public IDictionary<string, string> Data { get; set; }
        public NewUser User { get; set; }

        public void DeserializeModel()
        {
            User = new NewUser ()
            {
                Email = Data["email"],
                Name =Data["name"],
                Title = Data["title"],
                UserId = Convert.ToInt32(Data["user_id"]),
                Status = Data["status"],
                PhotoUrl = Data["photo_url"],
                StatusMessage = Data["status_message"],
                Password = Data.ContainsKey("password") ? Data["password"] : String.Empty
            };
        }
    }
}