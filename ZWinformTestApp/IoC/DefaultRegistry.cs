using LibHipChat.Interfaces;
using LibHipChat.Services;
using StructureMap.Configuration.DSL;

namespace ZWinformTestApp.IoC
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For(typeof(ILogger<>)).Use(typeof(Log4NetLogger<>));
            For(typeof(ILogConfigurator)).Use(typeof(LogConfigurator));
            For(typeof(ILogLocationProvider)).Use(typeof(LogLocationProvider));
        }
    }
}