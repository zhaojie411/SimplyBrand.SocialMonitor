using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using SimplyBrand.SocialMonitor.Business.Utility;
namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// UserReportBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserReportBLLTest
    {
        public UserReportBLLTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestMethod]
        public void UpdateReportStatusTest()
        {

            DateTime dt = new DateTime(2013, 7, 14);
            for (int i = 0; i <= 30; i++)
            {
                UserReport entity = new UserReport()
                {

                    FileName = Guid.NewGuid().ToString() + ".pdf",
                    FilePath = "/reports",
                    Link = "",
                    CreateDate = DateTime.Now,
                    Description = "",
                    ReportType = (int)EnumReportType.WeeklyReport,
                    SysUserId = 34,
                    IsSysGen = true,
                    //StartTime = DateTimeHelper.GetFirstDayOfMonth(2012, i),
                    //EndTime = DateTimeHelper.GetLastDayOfMonth(2012, i),
                    StartTime = dt.AddDays(-i * 7 + 1),
                    EndTime = dt.AddDays(-i * 7 + 7),
                    ReportStatus = (int)EnumReportStatus.UnAudited

                };
                DataRepository.UserReportProvider.Insert(entity);
            }





            //UserReportBLL bll = new UserReportBLL();
            //bll.UpdateReportStatus(entity.UserReportId, (int)EnumReportStatus.ThroughAudit);
            //Assert.IsTrue(DataRepository.UserReportProvider.GetByUserReportId(entity.UserReportId).ReportStatus == (int)EnumReportStatus.ThroughAudit);
        }





    }
}
