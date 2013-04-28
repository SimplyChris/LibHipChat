using System.Collections.Generic;
using LibHipChat.Domain.Entities;

namespace LibHipChat.Domain.Interfaces
{
    public interface IRoomListener
    {
        IList<IMessageProcessor> MessageProcessors { get; set; }

        void SetRoomId(string roomId);        
        IHipChatProxy GetHipChatProxy();        
        IList<RoomMessage> GetNewMessages();
        void ProcessNewMessages();
        void AddProcessor (IMessageProcessor processor);
        bool RemoveProcessor(IMessageProcessor processorToRemove);        
    }
}