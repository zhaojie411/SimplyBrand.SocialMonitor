using SimplyBrand.SocialMonitor.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class UserKeywordController
    {

        UserKeywordBLL bll = new UserKeywordBLL();
        public string Add(string sysadminid, string familyid, string keyname, string isprimary, string isforbid)
        {
            return bll.Add(sysadminid,familyid,keyname,isprimary,isforbid);    

        }
        public string Find(string sysuserid, int managerId, int familyid, string likeKeyName)
        {
            return bll.Find(sysuserid,managerId,familyid,likeKeyName);
        }
        public string Update(string userid, string keyid, string keyname, string isforbid)
        {
            return bll.Update(userid, keyid, keyname, isforbid);
        }
        /// <summary>
        /// 删除用户关键词
        /// </summary>
        /// <param name="keyid"></param>
        /// <returns></returns>
        public string Delete(string keyid)
        {
            return bll.Delete(keyid);
        }
    }
}
