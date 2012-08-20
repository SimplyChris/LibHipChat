using System.Collections.Generic;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Entities;
using LibHipChat.Proxy.Contracts;

namespace LibHipChat.Services.Interfaces
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