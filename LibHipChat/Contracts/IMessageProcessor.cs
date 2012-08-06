using System.Collections.Generic;

namespace LibHipChat.Contracts
{
    public interface IMessageProcessor
    {        
        void SetMessageTypeFilter (IList<RoomMessageType> messageTypes);
        IList<RoomMessageType> GetMessageTypeFilter ();
    }
}