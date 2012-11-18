using LibHipChat.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LibHipChat.Services
{
    public class Log4NetLogger <T> : ILogger <T>
    {
        private ILog _logger;
        private ILogConfiguration _logConfiguration;

        public Log4NetLogger(ILogConfiguration configuration)
        {
            _logConfiguration = configuration;
            _logConfiguration.Configurate();
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