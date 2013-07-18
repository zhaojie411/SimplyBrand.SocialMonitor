using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace SimplyBrand.SocialMonitor.ReportService
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        ServiceHost serviceHost = null;
        protected override void OnStart(string[] args)
        {
            serviceHost = new ServiceHost(typeof(SimplyReportService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            try
            {

                if (serviceHost != null)
                    serviceHost.Close();
            }
            catch (Exception)
            {

                serviceHost.Abort();
            }
        }
    }
}
