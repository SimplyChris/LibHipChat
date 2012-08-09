using System.Collections.Generic;
using LibHipChat.Contracts;

namespace LibHipChat.Entities.Abstract
{
    public class DefaultMessageProcessor : IMessageProcessor
    {
        private IList<RoomMessageType> _messageTypeFilter;

        public DefaultMessageProcessor ()
        {
            _messageTypeFilter  = new List<RoomMessageType>();
        }

        public void SetMessageTypeFilter (IList<RoomMessageType> messageTypes)
        {
            _messageTypeFilter = messageTypes;
        }

        public bool IsRegisteredMessageType(RoomMessageType messageType)
        {
            return _messageTypeFilter.Contains(messageType);
        }        

        public void ProcessMessage(RoomMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}