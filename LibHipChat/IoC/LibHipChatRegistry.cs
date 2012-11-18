using LibHipChat.Interfaces;
using LibHipChat.Services;
using StructureMap;
using StructureMap.Configuration.DSL;
using log4net.Core;

namespace LibHipChat.IoC
{
    public class LibHipChatRegistry : Registry 
    {
        public LibHipChatRegistry()
        {
            For(typeof (ILogger<>)).Use(typeof (Log4NetLogger<>));
            For(typeof (ILogConfiguration)).Use(typeof (LogConfiguration));
            For(typeof (ILogLocationProvider)).Use(typeof (LogLocationProvider));
        }
    }
}