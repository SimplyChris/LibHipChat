using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Contracts
{
    public interface IMessageProcessor
    {        
        void SetMessageTypeFilter (IList<RoomMessageType> messageTypes);
        bool IsRegisteredMessageType(RoomMessageType messageType);
        void ProcessMessage (RoomMessage message);
    }
}