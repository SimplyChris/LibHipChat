using System;
using System.Collections.Generic;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    [JsonObject (MemberSerialization.OptIn)]
    public class JsonRoomDetailModel : IJsonModel<IDictionary<string,object>>
    {
        [JsonProperty ("room")]
        public IDictionary<string, object> Data { get; set; }

        public RoomDetail RoomInfo;

        public void DeserializeModel()
        {
            RoomInfo = new RoomDetail()
                           {
                               Id = Convert.ToInt32(Data["room_id"]),
                               Name = (string)Data["name"],
                               Topic = (string)Data["topic"],
                               LastActiveLong = Convert.ToInt32(Data["last_active"]),
                               CreatedLong = Convert.ToInt32(Data["created"]),
                               IsArvhived = Convert.ToBoolean(Data["is_archived"]),
                               IsPrivate = Convert.ToBoolean(Data["is_private"]),
                               OwnerUserId = Convert.ToInt32(Data["owner_user_id"]),
                               GuestAccessURL = (string) Data["guest_access_url"],
                               JabberId = (string) Data["xmpp_jid"]
                           };
        }
    }
}