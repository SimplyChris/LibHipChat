using LibHipChat.Domain.Interfaces;
using log4net;

namespace LibHipChat.Domain.Services
{
    public class Log4NetLogger <T> : ILogger <T>
    {
        private ILog _logger;
        private ILogConfigurator _logConfigurator;

        public Log4NetLogger(ILogConfigurator configurator)
        {
            _logConfigurator = configurator;
            _logConfigurator.Configure();
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