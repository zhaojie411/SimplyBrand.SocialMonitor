using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.DB
{
    public class HadoopProvider : IDBProvider
    {
        public string GetSeach(string sourceids, string familyids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex, int sysuserid)
        {
            return "";
        }



        public string GetSummaryData(int sysUserId, string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday)
        {
            throw new NotImplementedException();
        }

        public string GetEmotionalData(int sysUserId, string keywordFamilyIDs, string platforms, bool isToday)
        {
            throw new NotImplementedException();
        }
    }
}
