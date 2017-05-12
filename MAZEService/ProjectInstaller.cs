using MAZE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace MAZE
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            //Add custom code here
        }


        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            //Add custom code here
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            //Add custom code here
        }


        public override void Uninstall(IDictionary savedState)
        {

            ServiceController serviceController = new ServiceController("MAZE");
            try
            {

                if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)))
                {

                    serviceController.Stop();
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                    //File.WriteAllText("C:\\Program Files (x86)\\MAZE\\INSTALLOG.txt", "Unistalling service from ProjectINstaller.cs");
                }
            }
            catch (Exception exc)
            {
                LogFile.write_LogFile("Error trying to stop serv:" + exc.Message);
            }

            base.Uninstall(savedState);

        }
    }
}
