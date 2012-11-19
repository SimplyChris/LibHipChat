using LibHipChat.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LibHipChat.Services
{
    public class LogConfigurator : ILogConfigurator
    {
        private ILogLocationProvider _locationProvider;
        
        public LogConfigurator(ILogLocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }

        public void Configure()
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

            ConsoleAppender consoleAppender = new ConsoleAppender();
            consoleAppender.Layout = pl;
            consoleAppender.ActivateOptions();

//            log4net.Config.BasicConfigurator.Configure(fileAppender);
            IAppender[] appenders = {fileAppender, consoleAppender};
            log4net.Config.BasicConfigurator.Configure(appenders);
            var log = LogManager.GetLogger(GetType());
            log.Debug("Logging Configured");
        }
    }
}