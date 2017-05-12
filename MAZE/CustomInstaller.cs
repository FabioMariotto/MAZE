using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MAZE
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        public CustomInstaller()
        {
            InitializeComponent();
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
            System.Diagnostics.Process application = null;
            foreach (var process in Process.GetProcesses())
            {
                if (!process.ProcessName.ToLower().Contains("maze")) continue;
                application = process;
                break;
            }

            if (application != null && application.Responding)
            {
                application.Kill();
                base.Uninstall(savedState);
            }
        }
    }
}
