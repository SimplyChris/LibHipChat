using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace LibHipChat.WindowsService
{
    class Program 
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
                                {
                                    x.Service<ProcessorService>(s =>
                                                                    {
                                                                        s.ConstructUsing(name => new ProcessorService());
                                                                        s.WhenStarted(ps => ps.Start());
                                                                        s.WhenStopped(ps => ps.Stop());
                                                                    });
                                    x.RunAsLocalService();
                                    x.SetDescription("LibHipChat Message Dispatcher");
                                    x.SetDisplayName("LibHipChat Message Processor");
                                    x.SetServiceName("LibHipChatProcessor");
                                });
        }
    }
}
