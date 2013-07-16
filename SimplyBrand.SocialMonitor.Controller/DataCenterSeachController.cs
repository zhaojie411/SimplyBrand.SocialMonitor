using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.DB;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class DataCenterSeachController
    {
        private DBType dbType = DBType.SqlServer;
        Business.DB.SQLServerProvider bll = new Business.DB.SQLServerProvider();
        public string GetSeach(string sourceids, string familyids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex,int sysuserid)
        {
            return bll.GetSeach(sourceids, familyids, keyvalue, notkeyvalue, starttime, endtime, emotional, pageSize, pageindex, sysuserid);
        }


        public string GetSummaryData(int sysUserId, string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday)
        {
            IDBProvider dbProvider = DBProviderFactory.GetDBProvider(dbType);
            return dbProvider.GetSummaryData(sysUserId, keywordFamilyIDs, platforms, emotionvalues, isToday);
        }

        public string GetEmotionalData(int sysUserId, string keywordFamilyIDs, string platforms, bool isToday)
        {
            IDBProvider dbProvider = DBProviderFactory.GetDBProvider(dbType);
            return dbProvider.GetEmotionalData(sysUserId, keywordFamilyIDs, platforms, isToday);
        }
    }
}
