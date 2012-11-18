namespace LibHipChat.Interfaces
{
    public interface ILogger <T>
    {
        void Debug(string message);
        void DebugFormat(string format, params object[] parameters);
    }
}