using LibHipChat.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LibHipChat.Services
{
    public class LogConfiguration : ILogConfiguration
    {
        private ILogLocationProvider _locationProvider;
        
        public LogConfiguration(ILogLocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }

        public void Configurate()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders(); /*Remove any other appenders*/

            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendToFile = true;
            fileAppender.LockingModel = new FileAppender.MinimalLock();
            fileAppender.File = _locationProvider.GetFilePath();
            PatternLayout pl = new PatternLayout();
            pl.ConversionPattern = "%d [%2%t] %-5p [%-10c]   %m%n%n";
            pl.ActivateOptions();
            fileAppender.Layout = pl;
            fileAppender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(fileAppender);
            var log = LogManager.GetLogger(GetType());
            log.Debug("Logging Configured");
        }
    }
}