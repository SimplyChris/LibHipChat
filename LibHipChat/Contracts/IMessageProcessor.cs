using System.Collections.Generic;
using LibHipChat.Domain.Entities;

namespace LibHipChat.Domain.Contracts
{
    public interface IMessageProcessor
    {        
        void SetMessageTypeFilter (IList<RoomMessageType> messageTypes);
        bool IsRegisteredMessageType(RoomMessageType messageType);
        void ProcessMessage (RoomMessage message);
    }
}