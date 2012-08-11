﻿using System.Collections.Generic;
using System.Diagnostics;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain;


namespace HipChatMessageProcessor.Processors
{
    public class EchoProcessor : IMessageProcessor
    {
        public IList<RoomMessageType> _typeFilters;

        public void SetMessageTypeFilter(IList<RoomMessageType> messageTypes)
        {
            _typeFilters = messageTypes;
        }

        public bool IsRegisteredMessageType(RoomMessageType messageType)
        {
            return _typeFilters.Contains(messageType);
        }

        public void ProcessMessage(RoomMessage message)
        {
            Debug.Print("EchoProcessor: {0}", message.Message);
        }
    }
}