using LibHipChat.Interfaces;
using log4net;

namespace LibHipChat.Services
{
    public class Log4NetLogger <T> : ILogger <T>
    {
        private ILog _logger;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(typeof(T));
        }


        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void DebugFormat(string format, params object[] parameters)
        {
            _logger.DebugFormat(format, parameters);
        }

    }
}