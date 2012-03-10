using System;
using LibHipChat.Contracts;

namespace LibHipChat
{
    public class HipChatResponse
    {
        public String ResponseString { get; set; }
        public IJsonModel Model { get; set; }
    }
}