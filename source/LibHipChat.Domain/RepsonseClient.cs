﻿using LibHipChat.Domain.Constants;
using LibHipChat.Domain.Interfaces;

namespace LibHipChat.Domain
{
    public class RepsonseClient : IRepsonseClient
    {
        private string _roomId;
        private IHipChatProxy _proxy;
                

        public RepsonseClient (string roomId, IHipChatProxy proxy)
        {
            _roomId = roomId;
            _proxy = proxy;
        }

        public void Reply(string from, string message, MessageFormat format)
        {
            _proxy.MessageRoom(_roomId, from, message, format);
        }
    }
}