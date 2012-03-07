using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LibHipChat.Entities
{
    [DataContract]
    public class Room
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]        
        public String Topic { get; set; }
        [DataMember]
        public DateTime? LastActiveAt { get; set; }
        [DataMember]
        public DateTime? CreatedAt { get; set; }
        
        [DataMember]
        public IList<User> Participants { get; set; }
        
        [DataMember]
        public Int32 OwnerUserId { get; set; }
              
        //TODO: Move out to helper class
        public DateTime GetTimeFromUnixLong (long ticks)
        {
            return new DateTime(ticks);
        }
    }
}
