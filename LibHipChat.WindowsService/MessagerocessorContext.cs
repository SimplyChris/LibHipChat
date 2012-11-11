using System;
using LibHipChat.Services.Contracts;


namespace LibHipChat.WindowsService
{
    public class MessagerocessorContext
    {
        public IMessageProcessor Processor;
        public TimeSpan IntervalTime;
        public DateTime LastActiveTime;
    }
}