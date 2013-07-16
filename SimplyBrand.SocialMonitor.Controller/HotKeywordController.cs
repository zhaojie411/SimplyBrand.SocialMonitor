using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;
namespace SimplyBrand.SocialMonitor.Controller
{
    public class HotKeywordController
    {
        HotKeywordBLL bll = new HotKeywordBLL();
        public string GetHotKeyword(int sysUserId, string keywordFamilyIDs)
        {
            return bll.GetHotKeyword(sysUserId, keywordFamilyIDs);
        }
    }
}
