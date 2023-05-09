using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WPA06Service
{
    public partial class TheService : ServiceBase
    {
        private Thread t;
        private Server server;
        public TheService()
        {
            InitializeComponent();
            server = new Server();
        }


        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("Service started - version 3");
            //Turn on listener server with a thread
        
            t = new Thread(new ThreadStart(server.RunServer));
            t.Start();
            
        }

        protected override void OnStop()
        {
            server.Run = false;
            t.Join();
            server = null;
            t = null;
            Logger.Log("Service stopped");
        }

    }
}
