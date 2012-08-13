using System.Collections.Generic;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Entities;
using LibHipChat.Proxy.Contracts;
using LibHipChat.Services.Interfaces;

namespace LibHipChat.Services
{
    public class RoomListener : IRoomListener
    {

        private string _roomId;
        private IHipChatProxy _proxy;
        private IList<RoomMessage> _previousRoomMessages; 
        private IList<RoomMessage> _roomMessages; 



        
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
            _previousRoomMessages = _roomMessages;
            _roomMessages = _proxy.GetRecentRoomHistory(_roomId);
            return _roomMessages;
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