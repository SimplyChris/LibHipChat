using System;

namespace LibHipChat.Domain
{
    public class HipChatResponse
    {
        public String ResponseString { get; set; }
        public object Model { get; set; }
        public Int32 ApiCallsRemaining { get; set; }
    }
}