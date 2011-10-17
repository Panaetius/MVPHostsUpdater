using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
                        RunService();
                        break;
                }
            }
            else
            {
                RunService();
            }
            
        }

        /// <SUMMARY>
        /// Set things in motion so your service can do its work.
        /// </SUMMARY>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            MvpHelper.UpdateMvpHostsFile();
        }

        private static void RunService()
        {
            ServiceBase[] services = { new Program() };
            ServiceBase.Run(services);
        }
    }
}
