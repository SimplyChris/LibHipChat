using LibHipChat.Domain.Constants;

namespace LibHipChat.Domain.Interfaces
{
    public interface IRepsonseClient
    {
        void Reply(string from, string message, MessageFormat format);
    }
}