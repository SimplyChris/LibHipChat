using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    public class RoomDetail : Room
    {
        public IList<User> Participants { get; set; }

        [JsonProperty ("guest_access_url") ]
        public string GuestAccessURL { get; set; }        


        public RoomDetail ()
        {
            Participants = new List<User>();
        }
    }
}