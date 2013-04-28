using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibHipChat;
using LibHipChat.Domain.Constants;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain;
using LibHipChat.Domain.Interfaces;


namespace HipChatMessageProcessor.Processors
{
    public class EchoProcessor : IMessageProcessor
    {
        public IList<RoomMessageType> _typeFilters;
        private string _tags = "(hipchat)";
        public EchoProcessor ()
        {
            _typeFilters = new List<RoomMessageType>();
        }

        public string ProcessorDisplayName { get { return "Echo Processor"; }
        }

        public void SetMessageTypeFilter(IList<RoomMessageType> messageTypes)
        {
            _typeFilters = messageTypes;
        }

        public bool IsRegisteredMessageType(RoomMessageType messageType)
        {
            return _typeFilters.Contains(messageType);
        }

        public void ProcessMessage(RoomMessage message, IRepsonseClient context)
        {
            try
            {
                if (message.Message.StartsWith("ep set tags "))
                {
                    _tags = message.Message.Substring("ep set tags ".Length);
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }

            context.Reply(ProcessorDisplayName, _tags +" "+ message.Message + " " + _tags, MessageFormat.Text);

        }

        public void ProcessMessage(RoomMessage message)
        {
            Console.Write("EchoProcessor: {0}", message.Message);
        }
    }
}