using SimplyBrand.SocialMonitor.Business.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SimplyBrand.SocialMonitor.ReportService
{
    [ServiceContract]
    public interface ISimplyReportService
    {
        [OperationContract]
        string GeneratePDF(int sysUserId, string reportStarttime, string reportEndTime, EnumReportType type, bool isSysGen, string platforms, string keywordFamilyIDs, string emotionvalues);
    }
}
