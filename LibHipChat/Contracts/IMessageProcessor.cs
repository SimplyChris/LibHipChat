using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Contracts
{
    public interface IMessageProcessor
    {        
        void SetMessageTypeFilter (IList<RoomMessageType> messageTypes);
        IList<RoomMessageType> GetMessageTypeFilter ();
        void ProcessMessage(RoomMessage message);
    }
}