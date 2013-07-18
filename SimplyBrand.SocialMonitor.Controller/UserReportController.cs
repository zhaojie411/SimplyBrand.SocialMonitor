using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
namespace SimplyBrand.SocialMonitor.Controller
{
    public class UserReportController
    {
        UserReportBLL bll = new UserReportBLL();
        /// <summary>
        /// 前台用户下载报表
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="reportType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetUserReport(int sysUserId, int reportType, string date)
        {
            return bll.GetUserReport(sysUserId, reportType, date);
        }

        /// <summary>
        /// 更新报表的审核状态
        /// </summary>
        /// <param name="userReportId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateReportStatus(int userReportId, int reportStatus)
        {
            return bll.UpdateReportStatus(userReportId, reportStatus);
        }
        /// <summary>
        /// 后台查询用户报表
        /// </summary>
        /// <param name="sysAdminId"></param>
        /// <param name="reportType"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public string GetUserReport(int sysAdminId, int reportType, int pageindex, int pagesize)
        {
            return bll.GetUserReport(sysAdminId, reportType, pageindex, pagesize);
        }


        public string GeneratePDF(int sysUserId, string reportStarttime, string reportEndTime, EnumReportType type, bool isSysGen, string platforms, string keywordFamilyIDs, string emotionvalues)
        {
            return bll.GeneratePDF(sysUserId, reportStarttime, reportEndTime, type, isSysGen, platforms, keywordFamilyIDs, emotionvalues);
        }


        public string GetInfoCenter(int sysUserId)
        {
            return bll.GetInfoCenter(sysUserId);
        }
    }
}
