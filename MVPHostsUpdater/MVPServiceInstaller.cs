using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Security.Permissions;
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
        }

        internal void InstallService()
        {
            // Instantiate installers for process and services.
            using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, null))
            {
                IDictionary state = new Hashtable();
                inst.Install(state);
                inst.Commit(state);
            }
        }

        internal void RemoveService()
        {
            using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, null))
            {
                IDictionary state = new Hashtable();
                inst.Rollback(state);
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