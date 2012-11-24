using LibHipChat.Domain;

namespace LibHipChat.Interfaces
{
    public interface IRepsonseClient
    {
        void Reply(string from, string message, MessageFormat format);
    }
}