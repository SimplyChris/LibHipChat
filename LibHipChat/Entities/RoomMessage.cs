using LibHipChat.Entities;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    [JsonObject (MemberSerialization = MemberSerialization.OptIn)]
    public class RoomMessage
    {
        public RoomMessageType MessageType { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("from")]
        public User User { get; set; }

        [JsonProperty("file")]
        public FileUploadInformation UploadInformation { get; set; }

    }
}