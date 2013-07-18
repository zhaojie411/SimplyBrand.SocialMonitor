using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace SimplyBrand.SocialMonitor.ReportService
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new Service1() 
            //};
            //ServiceBase.Run(ServicesToRun);

            ////SimplyBrand.SocialMonitor.Business.Report.SimplyReport report = new Business.Report.SimplyReport();

            //SimplyBrand.SocialMonitor.Controller.WCFClient client = new Controller.WCFClient();
            SimplyBrand.SocialMonitor.Business.UserReportBLL bll = new Business.UserReportBLL();
            var json = bll.GeneratePDF(34, "2013-07-15", "2013-07-19", Business.Utility.EnumReportType.Custom, true, "1,2,3,4", "", "");

            //ServiceHost serviceHost = new ServiceHost(typeof(SimplyReportService));
            //serviceHost.Open();
            //Console.WriteLine("服务已经启动");
            //Console.Read();




            //ThreadPool.SetMaxThreads(20, 20);
            //for (int i = 0; i < 1000; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(DoJob, i);
            //}
            //Console.WriteLine("working...");
            //Console.Read();
            //DrawReport dr = new DrawReport();
            ////dr.DrawChart();
            //dr.GeneratePDF(34, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1), DateTime.Now.AddDays(-1), EnumReportType.DayReport, true, "1,2,3,4", "8,9", "-1,0,1", true);
        }
        //private static void DoJob(Object threadContext)
        //{
        //    DrawReport drawReport = new DrawReport();
        //    Console.WriteLine("working on over at " + threadContext.ToString());
        //}
    }
}
