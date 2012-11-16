using System.Collections.Generic;
using System.Linq;
using LibHipChat.Domain.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    public class JsonUsersModel : IJsonModel <IList<Dictionary<string,string>>> 
    {
        [JsonProperty("users")]
        public IList<Dictionary<string, string>> Data { get; set; }  

        public IList<User> Model { get; set; }

        public void DeserializeModel()
        {
            IList<User> list = new List<User>();
            foreach (User model in Data.Select(DeserializeListItem))
            {
                list.Add(model);
            }
            Model = list;
        }

        private User DeserializeListItem(Dictionary<string, string> dictionary)
        {
            return new User()
            {
                Email = dictionary["email"],
                Name = dictionary["name"],
                Title = dictionary["title"],
                UserId = dictionary["user_id"],
                Status = dictionary["status"],
                PhotoUrl = dictionary["photo_url"],
                StatusMessage = dictionary["status_message"]
            };
        }
    }


}