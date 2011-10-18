using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace MVPHostsUpdater
{
    [RunInstaller(true)]
    public class MvpServiceInstaller:ServiceInstaller
    {
        public MvpServiceInstaller()
        {
            this.ServiceName = "MVPS hosts Updater";
            this.Description = "Automagically updates the hosts file with the entries from MVPS";
            this.DisplayName = "MVPS hosts Updater";
            this.StartType = ServiceStartMode.Automatic;
            this.BeforeUninstall += this.MvpServiceInstaller_BeforeUninstall;

            this.Installers.Add(new MyServiceInstallerProcess());
        }

        public override void Install(IDictionary stateSaver)
        {
            this.AfterInstall += this.MvpServiceInstaller_AfterInstall;
            base.Install(stateSaver);
        }

        protected void MvpServiceInstaller_AfterInstall(object obj, InstallEventArgs args)
        {
            // Auto Start the Service Once Installation is Finished.
            var controller = new ServiceController(this.ServiceName);
            controller.Start();
        }

        void MvpServiceInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            var controller = new ServiceController(this.ServiceName);
            if (controller.Status == ServiceControllerStatus.Running)
            {
                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }
    }

    [RunInstaller(true)]
    public sealed class MyServiceInstallerProcess : ServiceProcessInstaller
    {
        public MyServiceInstallerProcess()
        {
            this.Account = ServiceAccount.LocalSystem;
        }
    } 
}