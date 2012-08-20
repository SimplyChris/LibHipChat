using System.Collections.Generic;

namespace LibHipChat.Services.Interfaces
{
    public interface IMessageDispatcher
    {
        void SetProcessors(IList<IMessageProcessor> processors);
        IList<IMessageProcessor> GetProcessors(); 
        void DispatchMessage(IMessageProcessor processor);
    }
}