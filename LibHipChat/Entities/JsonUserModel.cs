using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    public class JsonUserModel : IJsonModel
    {
        [JsonProperty("users")]
        public IList<Dictionary<string, string>> Data { get; set; }

        public IList<IHipChatModel> DeserializeList()
        {
            List<IHipChatModel> list = new List<IHipChatModel>();
            foreach (IHipChatModel model in Data.Select(DeserializeListItem))
            {
                list.Add(model);
            }
            return (list);
        }

        private IHipChatModel DeserializeListItem(Dictionary<string, string> dictionary)
        {
            return new User()
            {
                Email = dictionary["email"],
                Name = dictionary["name"],
                Title = dictionary["title"],
                UserId = Convert.ToInt32(dictionary["user_id"]),
                Status = dictionary["status"],
                PhotoUrl = dictionary["photo_url"],
                StatusMessage = dictionary["status_message"]
            };
        }
    }


}