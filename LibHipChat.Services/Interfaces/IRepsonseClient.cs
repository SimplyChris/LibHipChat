using LibHipChat.Domain;

namespace LibHipChat.Services.Interfaces
{
    public interface IRepsonseClient
    {
        void Reply(string from, string message, MessageFormat format);
    }
}