using System.Collections.Generic;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain.Interfaces;

namespace LibHipChat.Domain.Services
{
    public class RoomListener : IRoomListener
    {

        private string _roomId;
        private IHipChatProxy _proxy;
        private IList<RoomMessage> _previousRoomMessages;
        private bool _process_history = false;


        public IList<IMessageProcessor> MessageProcessors { get; set; }

        public RoomListener (IHipChatProxy hipChatProxy)
        {
            MessageProcessors = new List<IMessageProcessor>();
            _proxy = hipChatProxy;
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

        public IHipChatProxy GetHipChatProxy()
        {
            return _proxy;  
        }

        public IList<RoomMessage> RetrieveRecentMessages()
        {            
            var _roomMessages = _proxy.GetRecentRoomHistory(_roomId);
            return _roomMessages;
        }

        public void ProcessNewMessages ()
        {
            if (_previousRoomMessages == null)
            {
                _previousRoomMessages = RetrieveRecentMessages();
                if (_process_history)
                {
                    DispatchMessages(_previousRoomMessages, MessageProcessors);
                }
            }

            var newMessages  = GetNewMessages();

            DispatchMessages(newMessages, MessageProcessors);
        }

        public IList<RoomMessage> GetNewMessages()
        {
            var newRoomMessages = RetrieveRecentMessages();

            var newMessageArray = new RoomMessage[newRoomMessages.Count];
            newRoomMessages.CopyTo(newMessageArray,0);


            var messagesToRemove = _previousRoomMessages ?? new List<RoomMessage>();



            _previousRoomMessages = newMessageArray;
            foreach (var messageToRemove in messagesToRemove)
            {
                newRoomMessages.Remove(messageToRemove);
            }
            
            return newRoomMessages;
        }

        public void AddProcessor(IMessageProcessor processor)
        {
            MessageProcessors.Add(processor);
        }

        public bool RemoveProcessor(IMessageProcessor processorToRemove)
        {
            throw new System.NotImplementedException();
        }

        public void DispatchMessages (IList<RoomMessage> messages, IList<IMessageProcessor> processors )
        {
            foreach (var messageProcessor in processors )
            {
                ProcessMessages(messages, messageProcessor);
            }
        }

        private void ProcessMessages(IList<RoomMessage> messages, IMessageProcessor processor)
        {
            foreach (var message in messages)
            {
                if (processor.IsRegisteredMessageType(message.MessageType))
                {
                    processor.ProcessMessage(message, new RepsonseClient(_roomId, _proxy));
                }
            }
        }
    }
}