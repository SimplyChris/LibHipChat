using System;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    [JsonObject (MemberSerialization = MemberSerialization.OptIn)]
    public class RoomMessage
    {
        public RoomMessageType MessageType { get; set; }
        public DateTime TimeStamp { get { return ParseTimestamp(_stringTimeStamp); } }

        [JsonProperty("date")]
        private string _stringTimeStamp;


        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("from")]
        public User User { get; set; }

        [JsonProperty("file")]
        public FileUploadInformation UploadInformation { get; set; }

        public DateTime ParseTimestamp (string dateTime)
        {
            //example dateTime input: "2010-11-19T15:48:19-0800"

            var dateTimeParts = dateTime.Split(new[] {'T'});
            var stringDate = dateTimeParts[0];
            var stringTime = dateTimeParts[1].Split(new[] {'-'})[0];

            

            var dateParts = stringDate.Split(new[] {'-'});
            var dateParms = new {year = dateParts[0], month = dateParts[1], day = dateParts[2]};

            var timeParts = stringTime.Split(new[] {':'});

            var timeParms = new {hour = timeParts[0], minute = timeParts[1], second = timeParts[2]};

            int year, month, day;
            int hours, minute, second;

            Int32.TryParse(dateParms.year, out year);
            Int32.TryParse(dateParms.month, out month);
            Int32.TryParse(dateParms.day, out day);

            Int32.TryParse(timeParms.hour, out hours);
            Int32.TryParse(timeParms.minute, out minute);
            Int32.TryParse(timeParms.second, out second);
            
            var outDateTime = new DateTime(year, month, day, hours, minute, second);

            return outDateTime;
        }
    }
}