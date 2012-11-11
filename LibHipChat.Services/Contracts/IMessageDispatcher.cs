using System.Collections.Generic;
using LibHipChat.Services.Contracts;

namespace LibHipChat.Domain.Services.Interfaces
{
    public interface IMessageDispatcher
    {
        void SetProcessors(IList<IMessageProcessor> processors);
        IList<IMessageProcessor> GetProcessors(); 
        void DispatchMessage(IMessageProcessor processor);
    }
}