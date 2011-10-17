using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MVPHostsUpdater
{
    class Program:ServiceBase
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                switch (args[0])
                {
                    case "-i":
                    case "-install":
                        InstallService();
                        break;
                    case "-r":
                    case "-remove":
                        RemoveService();
                        break;
                    default:
                        ShowUsageMessage();
                        break;
                }
            }
            else
            {
                ShowUsageMessage();
            }
            
        }

        /// <SUMMARY>
        /// Set things in motion so your service can do its work.
        /// </SUMMARY>
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        /// <SUMMARY>
        /// Stop this service.
        /// </SUMMARY>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down
            // necessary to stop your service.
        }
    }
}
