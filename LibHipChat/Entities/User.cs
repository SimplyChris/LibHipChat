using System;

namespace LibHipChat.Entities
{   
    
    public  class User
    {
        
        public Int32 UserId { get; set; }

        
        public String Name { get; set; }

        
        public String Email { get; set; }

        
        public String Title { get; set; }

        
        public String PhotoUrl { get; set; }

        
        public String Status { get; set; }

         
        public String StatusMessage { get; set; }

        
        public Int32 IsGroupAdmin { get; set; }
    }
}
