using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    public class JsonRoomsModel : IJsonModel<IList<IDictionary<string,string>>>
    {
        [JsonProperty("rooms")]
        public IList<IDictionary<string, string>> Data { get; set; }

        public IList<Room> Model; 

        public void DeserializeModel()
        {
            var rooms = new List<Room>();
            foreach (Room room in Data.Select (DeserializeListItem) )
            {
                rooms.Add(room);
            }

            Model = rooms;
        }

        private Room DeserializeListItem(IDictionary<string, string> dictionary)
        {
            return new Room()
            {
                Id = Convert.ToInt32(dictionary["room_id"]),
                Name = dictionary["name"],
                Topic = dictionary["topic"],
                LastActiveLong = Convert.ToInt32(dictionary["last_active"]),
                CreatedLong = Convert.ToInt32(dictionary["created"]),
                OwnerUserId = Convert.ToInt32(dictionary["owner_user_id"]),
                IsArvhived = Convert.ToBoolean(dictionary["is_archived"]),
                IsPrivate = Convert.ToBoolean(dictionary["is_private"]),
                JabberId = dictionary["xmpp_jid"]                
            };
        }
    }
}