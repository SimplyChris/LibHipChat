using System;
using System.Collections.Generic;
using LibHipChat.Domain.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibHipChat.Domain.Entities
{
    [JsonObject (MemberSerialization.OptIn)]
    public class JsonRoomDetailModel : IJsonModel<IDictionary<string,object>>
    {
        [JsonProperty ("room")]
        public IDictionary<string, object> Data { get; set; }

        public RoomDetail RoomInfo;

        public void DeserializeModel()
        {
            var participants = (JArray) Data["participants"];
            

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

            foreach (var participant in participants)
            {
                RoomInfo.Participants.Add(new User()
                                              {
                                                  UserId = participant["user_id"].ToString(),
                                                  Name = participant["name"].ToString()
                                              });
            }
        }
    }
}