using System.ServiceProcess;

namespace MVPHostsUpdater
{
    class Program:ServiceBase
    {
        static void Main(string[] args)
        {
                RunService();
        }

        /// <SUMMARY>
        /// Set things in motion so your service can do its work.
        /// </SUMMARY>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            MvpHelper.UpdateMvpHostsFile();
            this.Stop();
        }

        private static void RunService()
        {
            ServiceBase[] services = { new Program() };
            ServiceBase.Run(services);
        }
    }
}
