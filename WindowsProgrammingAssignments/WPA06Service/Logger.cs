using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace WPA06Service
{
    class Logger
    {
        public static void Log(string message)
        {
            EventLog serviceEventLog = new EventLog();
            if (!EventLog.SourceExists("MyEventSource"))
            {
                EventLog.CreateEventSource("MyEventSource", "MyEventLog");
            }
            serviceEventLog.Source = "MyEventSource";
            serviceEventLog.Log = "MyEventLog";
            serviceEventLog.WriteEntry(message);

            //Also write log to text file found in app.config

            string fileName = ConfigurationManager.AppSettings.Get("logFile");
            StreamWriter sw = new StreamWriter(@fileName, append: true);
            sw.WriteLine("Log time: " + DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString() + ", Message: " + message);
            sw.Close();
        }


    }
}
