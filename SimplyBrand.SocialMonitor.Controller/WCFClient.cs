using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class WCFClient
    {
        public string GeneratePDF(int sysUserId, string reportStarttime, string reportEndTime, SimplyBrand.SocialMonitor.Business.Utility.EnumReportType type, bool isSysGen, string platforms, string keywordFamilyIDs, string emotionvalues)
        {
            string result = "";
            using (ReportService.SimplyReportServiceClient client = new ReportService.SimplyReportServiceClient())
            {
                try
                {
                    result = client.GeneratePDF(sysUserId, reportStarttime, reportEndTime, type, isSysGen, platforms, keywordFamilyIDs, emotionvalues);
                }
                catch (Exception)
                {

                }
                finally
                {
                    try
                    {
                        client.Close();
                    }
                    catch (Exception)
                    {

                        client.Abort();
                    }
                }
            }
            return result;
        }

    }
}
