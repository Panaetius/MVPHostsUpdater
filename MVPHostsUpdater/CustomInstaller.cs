using System;
using System.Collections;
using System.ComponentModel;
using System.IO;

using MVPHostsUpdater;

namespace InstallerCustomAction
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        public CustomInstaller()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                File.Copy(MvpHelper.GetPathOfHostsFile(), MvpHelper.GetPathOfHostsFile() + "_MVPHostsUpdaterBackup");
            }
            catch (Exception) { }

            base.Install(stateSaver);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            File.Copy(MvpHelper.GetPathOfHostsFile() + "_MVPHostsUpdaterBackup", MvpHelper.GetPathOfHostsFile(), true);
            File.Delete(MvpHelper.GetPathOfHostsFile() + "_MVPHostsUpdaterBackup");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            File.Copy(MvpHelper.GetPathOfHostsFile() + "_MVPHostsUpdaterBackup", MvpHelper.GetPathOfHostsFile(), true);
            File.Delete(MvpHelper.GetPathOfHostsFile() + "_MVPHostsUpdaterBackup");
        }
    }
}
