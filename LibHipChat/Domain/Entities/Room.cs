using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    [JsonObject("room", MemberSerialization = MemberSerialization.OptIn)]   
    public class Room
    {
        [JsonProperty ("room_id")]
        public Int32 Id { get; set; }

        [JsonProperty ("name")]
        public String Name { get; set; }

        [JsonProperty ("topic")]
        public String Topic { get; set; }

        [JsonProperty("last_active")]
        public long LastActiveLong { get; set; }
        
        [JsonProperty("created")]
        public long CreatedLong { get; set; }

        public DateTime? LastActiveAt { get { return GetTimeFromUnixLong(LastActiveLong); } }
        public DateTime? CreatedAt { get { return GetTimeFromUnixLong(CreatedLong); } }

        [JsonProperty("owner_user_id")]
        public Int32 OwnerUserId { get; set; }

        [JsonProperty("is_archived")]
        public Boolean IsArvhived { get; set; }
        
        [JsonProperty("is_private")]
        public Boolean IsPrivate { get; set; }

        [JsonProperty("xmpp_jid")]
        public String JabberId { get; set; }

//xmpp_jid



        //TODO: Move out to helper class
        public DateTime GetTimeFromUnixLong(long ticks)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(ticks).ToLocalTime();
            return dtDateTime;
        }
    }
}