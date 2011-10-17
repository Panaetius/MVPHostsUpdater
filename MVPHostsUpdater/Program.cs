﻿using System;
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
            var installer = new MvpServiceInstaller();
            if (args.Count() > 0)
            {
                switch (args[0])
                {
                    case "-i":
                    case "-install":
                        installer.InstallService();
                        break;
                    case "-r":
                    case "-remove":
                        installer.RemoveService();
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
            base.OnStart(args);
        }

        /// <SUMMARY>
        /// Stop this service.
        /// </SUMMARY>
        protected override void OnStop()
        {
            base.OnStop();
        }

        private static void ShowUsageMessage()
        {
            
        }
    }
}
