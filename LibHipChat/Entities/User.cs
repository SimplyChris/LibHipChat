using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LibHipChat.Entities
{
    [DataContract]
    public  class User
    {
        [DataMember]
        public Int32 UserId { get; set; }

        [DataMember]
        public String Name { get; set; }
        
        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String PhotoUrl { get; set; }

        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public String StatusMessage { get; set; }
        
        [DataMember]
        public Int32 IsGroupAdmin { get; set; }
    }
}
