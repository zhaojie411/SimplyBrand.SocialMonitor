using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.DB
{
    public enum DBType
    {
        SqlServer = 1,
        Hadoop = 2
    }
    public interface IDBProvider
    {
        string GetSeach(string sourceids, string familyids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex, int sysuserid);

        string GetSummaryData(int sysUserId, string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday);

        string GetEmotionalData(int sysUserId, string keywordFamilyIDs, string platforms, bool isToday);
    }
}
