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
        void SetHipChatProxy(IHipChatProxy proxy);
        IList<RoomMessage> RetrieveRecentMessages();
        void AddProcessor (IMessageProcessor dispatcher);
        bool RemoveProcessor(IMessageProcessor dispatcherToRemove);
        void DispatchMessage(RoomMessage message, IMessageProcessor processor);
    }
}