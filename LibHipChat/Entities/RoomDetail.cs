using System.Collections.Generic;

namespace LibHipChat.Entities
{
    public class RoomDetail : Room
    {
        public IList<User> Participants { get; set; }   
    }
}