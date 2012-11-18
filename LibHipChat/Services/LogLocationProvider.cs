using LibHipChat.Interfaces;

namespace LibHipChat.Services
{
    public class LogLocationProvider : ILogLocationProvider 
    {
        public string GetFilePath()
        {
            return @"logs\LibHipChatLog.txt";
        }
    }
}