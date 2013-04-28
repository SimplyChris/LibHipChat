using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibHipChat.Domain.IoC;
using StructureMap;
using ZWinformTestApp.IoC;

namespace ZWinformTestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ObjectFactory.Configure(init => init.AddRegistry<DefaultRegistry>());
            IocContainer.Configure(); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
