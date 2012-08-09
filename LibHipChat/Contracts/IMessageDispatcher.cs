using System.Collections.Generic;

namespace LibHipChat.Contracts
{
    public interface IMessageDispatcher
    {
        void SetProcessors(IList<IMessageProcessor> processors);
        IList<IMessageProcessor> GetProcessors(); 
        void DispatchMessage(IMessageProcessor processor);
    }
}