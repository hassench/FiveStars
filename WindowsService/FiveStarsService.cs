using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class FiveStarsService : ServiceBase
    {
        EmailThread emailThread;
        Thread thread;
        public FiveStarsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            emailThread = new EmailThread();
            thread = new Thread(emailThread.DoWork);
            thread.Start();
        }

        protected override void OnStop()
        {
            thread.Abort();
        }
    }
}
