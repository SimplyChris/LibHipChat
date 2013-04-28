using System;
using System.Collections.Generic;
using LibHipChat.Domain.Entities;

namespace LibHipChat.Domain.Interfaces
{
    public interface IMessageProcessor
    {
        String ProcessorDisplayName { get; }
        //TODO: Need to set a way to process history messages if requested to 
        void SetMessageTypeFilter (IList<RoomMessageType> messageTypes);
        bool IsRegisteredMessageType(RoomMessageType messageType);
        void ProcessMessage (RoomMessage message, IRepsonseClient proxy);        
    }
}