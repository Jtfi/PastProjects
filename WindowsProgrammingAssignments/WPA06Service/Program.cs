using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WPA06Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

//#if DEBUG
//            TheService service = new TheService();
//            service.onDebug();
//#else


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                            new TheService()
            };
            ServiceBase.Run(ServicesToRun);
//#endif
        }
    }
}
