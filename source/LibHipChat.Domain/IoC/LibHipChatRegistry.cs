using LibHipChat.Domain.Interfaces;
using LibHipChat.Domain.Services;
using StructureMap.Configuration.DSL;

namespace LibHipChat.Domain.IoC
{
    public class LibHipChatRegistry : Registry 
    {
        public LibHipChatRegistry()
        {
            For(typeof (ILogger<>)).Use(typeof (Log4NetLogger<>));
            For(typeof (ILogConfigurator)).Use(typeof (LogConfigurator));
            For(typeof (ILogLocationProvider)).Use(typeof (LogLocationProvider));
            For<IHipChatProxy>().Use<HipChatProxy>();             

        }
    }
}