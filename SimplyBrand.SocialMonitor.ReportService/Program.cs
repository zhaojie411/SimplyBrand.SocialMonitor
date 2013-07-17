using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimplyBrand.SocialMonitor.ReportService
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ThreadPool.SetMaxThreads(20, 20);
            //for (int i = 0; i < 1000; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(DoJob, i);
            //}
            //Console.WriteLine("working...");
            //Console.Read();
            DrawReport dr = new DrawReport();
            //dr.DrawChart();
            dr.GeneratePDF(34, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1), DateTime.Now.AddDays(-1), EnumReportType.DayReport, true, "1,2,3,4", "8,9", "-1,0,1", true);
        }
        private static void DoJob(Object threadContext)
        {
            DrawReport drawReport = new DrawReport();
            Console.WriteLine("working on over at " + threadContext.ToString());
        }
    }
}
