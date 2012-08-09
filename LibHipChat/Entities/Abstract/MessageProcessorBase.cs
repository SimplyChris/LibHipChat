using System.Collections.Generic;
using LibHipChat.Contracts;

namespace LibHipChat.Entities.Abstract
{
    public abstract class MessageProcessorBase : IMessageProcessor
    {
        private IList<RoomMessageType> _messageTypeFilter;

        public MessageProcessorBase ()
        {
            _messageTypeFilter  = new List<RoomMessageType>();
        }

        public void SetMessageTypeFilter (IList<RoomMessageType> messageTypes)
        {
            _messageTypeFilter = messageTypes;
        }

        public IList<RoomMessageType> GetMessageTypeFilter ()
        {
            return _messageTypeFilter;
        }

        public void ProcessMessage(RoomMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}