using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.ReportService
{
    public class SimplyReportService : ISimplyReportService
    {
        public string GeneratePDF(int sysUserId, string reportStarttime, string reportEndTime, Business.Utility.EnumReportType type, bool isSysGen, string platforms, string keywordFamilyIDs, string emotionvalues)
        {
            UserReportController target = new UserReportController();
            return target.GeneratePDF(sysUserId, reportStarttime, reportEndTime, type, isSysGen, platforms, keywordFamilyIDs, emotionvalues);
        }
    }
}
