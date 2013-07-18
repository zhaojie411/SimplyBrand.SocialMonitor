using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using System.Threading;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
namespace SimplyBrand.SocialMonitor.ReportService
{
    /// <summary>
    /// 自动生成报告
    /// </summary>
    public class AutoGeneratePDF
    {
        System.Timers.Timer timer = null;
        public void Init()
        {
            timer = new System.Timers.Timer(3600000);//每个小时轮询
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            ThreadPool.SetMaxThreads(20, 20);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == 0)
            {
                //生成日报
                TList<SysUser> tlist = DataRepository.SysUserProvider.GetAll();
                foreach (SysUser entity in tlist)
                {
                    ThreadPool.QueueUserWorkItem(GenerateDayReport, entity.SysUserId);
                }
                //生成周报
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    foreach (SysUser entity in tlist)
                    {
                        ThreadPool.QueueUserWorkItem(GenerateWeekdayReport, entity.SysUserId);
                    }
                }
                //生成月报
                if (DateTime.Now.Day == 1)
                {
                    foreach (SysUser entity in tlist)
                    {
                        ThreadPool.QueueUserWorkItem(GenerateMonthReport, entity.SysUserId);
                    }
                }
            }
        }

        public void GenerateDayReport(object obj)
        {
            UserReportBLL bll = new UserReportBLL();
            string json = bll.GeneratePDF(int.Parse(obj.ToString()),
                                          DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                                          DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"),
                                          Business.Utility.EnumReportType.DayReport, true, "", "", "");
            LogHelper.WriteLog(json);
        }
        public void GenerateWeekdayReport(object obj)
        {
            UserReportBLL bll = new UserReportBLL();
            string json = bll.GeneratePDF(int.Parse(obj.ToString()),
                                          DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                                          DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"),
                                          Business.Utility.EnumReportType.WeekDayReport, true, "", "", "");
            LogHelper.WriteLog(json);
        }
        public void GenerateMonthReport(object obj)
        {
            UserReportBLL bll = new UserReportBLL();
            DateTime dt = DateTime.Now.AddDays(-1);

            string json = bll.GeneratePDF(int.Parse(obj.ToString()),
                                          DateTimeHelper.GetFirstDayOfMonth(dt.Year, dt.Month).ToString("yyyy-MM-dd"),
                                          DateTimeHelper.GetLastDayOfMonth(dt.Year, dt.Month).ToString("yyyy-MM-dd 23:59:59"),
                                          Business.Utility.EnumReportType.MonthReport, true, "", "", "");
            LogHelper.WriteLog(json);
        }
    }
}
