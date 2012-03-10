using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LibHipChat.Entities
{
    public class Room
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Topic { get; set; }

        public DateTime? LastActiveAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public IList<User> Participants { get; set; }

        public Int32 OwnerUserId { get; set; }

        //TODO: Move out to helper class
        public DateTime GetTimeFromUnixLong(long ticks)
        {
            return new DateTime(ticks);
        }
    }
}