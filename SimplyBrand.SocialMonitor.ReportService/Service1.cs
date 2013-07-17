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

        public ServiceHost serviceHost = null;
        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            serviceHost = new ServiceHost(typeof(SimplyBrand.SocialMonitor.ReportService.SimplyReportService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            if (serviceHost != null)
                try
                {
                    serviceHost.Close();
                }
                catch (Exception)
                {

                    serviceHost.Abort();
                }
        }
    }
}
