using System.Collections.Generic;
using LibHipChat.Contracts;
using LibHipChat.Entities;
using LibHipChat.Proxy.Contracts;
using LibHipChat.Services.Interfaces;

namespace LibHipChat.Services
{
    public class RoomListener : IRoomListener
    {

        private string _roomId;
        private IHipChatProxy _proxy;





        public IList<IMessageProcessor> MessageProcessors { get; set; }

        public RoomListener ()
        {
            MessageProcessors = new List<IMessageProcessor>();
        }

        public RoomListener(List<IMessageProcessor> processors)
        {
            MessageProcessors = processors;
        }

        public void SetRoomId(string roomId)
        {
            _roomId = roomId;
        }

        public void SetHipChatProxy(IHipChatProxy proxy)
        {
            _proxy = proxy;            
        }

        public IList<RoomMessage> RetrieveRecentMessages()
        {
            return _proxy.GetRecentRoomHistory(_roomId);
        }

        public void AddProcessor(IMessageProcessor processor)
        {
            MessageProcessors.Add(processor);
        }

        public bool RemoveProcessor(IMessageProcessor dispatcherToRemove)
        {
            throw new System.NotImplementedException();
        }

        public void DispatchMessage(RoomMessage message, IMessageProcessor processor)
        {
            processor.ProcessMessage(message);
        }
    }
}