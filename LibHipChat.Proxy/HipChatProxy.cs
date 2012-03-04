using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat;
using LibHipChat.Proxy.Contracts;

namespace LibHipChat.Proxy
{
    public class HipChatProxy : IHipChatProxy
    {
        private HipChatContext _context;
        private HipChatConnectionFactory _factory;

        public HipChatProxy (HipChatConnectionFactory factory)
        {
            _factory = factory;
        }
      
        public HipChatResponse DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public HipChatResponse AddUser(string userId)
        {
            throw new NotImplementedException();
        }

        public HipChatResponse MessageRoom(string roomId, string from, string message)
        {
            var connection = _factory.Create(ActionKey.MessageRoom);


            var actionParms = new Dictionary<string, string>
                                  {
                                      {"room_id", roomId},
                                      {"from", from},
                                      {"message", message}
                                  };

            var executor = new HipChatApiExecutor(connection, actionParms);

            return executor.Execute();
        }
    }
}
