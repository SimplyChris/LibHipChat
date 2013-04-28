using LibHipChat.Domain.Interfaces;

namespace LibHipChat.Domain.Services
{
    public class LogLocationProvider : ILogLocationProvider 
    {
        public string GetFilePath()
        {
            return @"logs\LibHipChatLog.txt";
        }
    }
}