using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class SysUserController
    {
        SysUserBLL bll = new SysUserBLL();

        public bool CheckLogin(int sysUserId)
        {
            return bll.CheckLogin(sysUserId);
        }

        public string Login(string sysUserName = null, string sysUserPwd = null)
        {
            return bll.Login(sysUserName, sysUserPwd);
        }

        public string StopOrEnableSysUser(int sysUserId)
        {
            return bll.StopOrEnableSysUser(sysUserId);
        }

        public string DeleteSysUser(int sysUserId)
        {
            return bll.DeleteSysUser(sysUserId);
        }

        public string SearchPage(string likeSysUserName, int pageIndex, int pageSize)
        {
            return bll.SearchPage(likeSysUserName, pageIndex, pageSize);
        }

        public string CreateSysUser(string sysUserName, string sysUserPwd, string sysUserRealName, string endDateStr, string permissions, string platforms, string sysUserEmail = null)
        {
            return bll.CreateSysUser(sysUserName, sysUserPwd, sysUserRealName, endDateStr, permissions, platforms, sysUserEmail);
        }
        public string UpdateSysUser(int sysUserId, string sysUserPwd, string sysUserRealName, string permissions, string platforms, string endDateStr, string sysUserEmail = null)
        {
            return bll.UpdateSysUser(sysUserId, sysUserPwd, sysUserRealName, permissions, platforms, endDateStr, sysUserEmail);
        }
        public string UpdateSysUserPwd(int sysUserId, string sysUserPwd, string newsUserPwd)
        {
            return bll.UpdateSysUserPwd(sysUserId, sysUserPwd, newsUserPwd);
        }

        public string UpdateLogo(int sysUserId, string logoName)
        {
            return bll.UpdateLogo(sysUserId, logoName);
        }

    }
}
