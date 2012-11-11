using LibHipChat.Domain;

namespace LibHipChat.Proxy.Contracts
{
    public interface IRepsonseClient
    {
        void Reply(string from, string message, MessageFormat format);
    }
}