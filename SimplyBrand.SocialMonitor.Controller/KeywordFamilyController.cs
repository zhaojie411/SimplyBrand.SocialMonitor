using SimplyBrand.SocialMonitor.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Controller
{

    public class KeywordFamilyController
    {

        KeywordFamilyBLL bll = new KeywordFamilyBLL();
        /// <summary>
        ///  查询sysuserid用户的关键词列表
        /// </summary>
        /// <param name="sysuserid"></param>
        /// <param name="managerId"></param>
        /// <param name="likeKeyName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string Find(string sysuserid, int managerId, string likeKeyName, int pageIndex, int pageSize)
        {

            return bll.Find(sysuserid, managerId, likeKeyName, pageIndex, pageSize);
        }
        /// <summary>
        /// 添加关键词家族
        /// </summary>
        /// <param name="keywordName"></param>
        /// <param name="keyWordStatus"></param>
        /// <param name="sysuserid"></param>
        /// <returns></returns>
        public string Add(string keywordName, string keyWordStatus, string sysuserid)
        {
            return bll.Add(keywordName, keyWordStatus, sysuserid);
        }

        /// <summary>
        /// 修改关键词家族
        /// </summary>
        /// <param name="keywordid"></param>
        /// <param name="keywordName"></param>
        /// <param name="keyWordStatus"></param>
        /// <param name="sysuserid"></param>
        /// <returns></returns>
        public string Update(string keywordid, string keywordName, string keyWordStatus, string sysuserid)
        {
            return bll.Update(keywordid, keywordName, keyWordStatus, sysuserid);
        }

        public string UpdateStatus(int keywordId, string sysuserid, string managerId)
        {

            return bll.UpdateStatus(keywordId, sysuserid, managerId);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="familyid"></param>
        /// <returns></returns>
        public string Delete(string familyid)
        {
            return bll.Delete(familyid);
        }

        /// <summary>
        /// 获取用户对应的品牌
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        public string GetKeywordFamily(int sysUserId)
        {
            return bll.GetKeywordFamily(sysUserId);
        }

    }
}
