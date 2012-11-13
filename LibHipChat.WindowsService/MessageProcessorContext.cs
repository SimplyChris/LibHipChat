using System;
using LibHipChat.Services.Contracts;


namespace LibHipChat.WindowsService
{
    public class MessageProcessorContext
    {
        public IMessageProcessor Processor;        
    }
}