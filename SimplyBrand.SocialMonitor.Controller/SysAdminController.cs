using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class SysAdminController
    {
        SysAdminBLL bll = new SysAdminBLL();
        public string Login(string sysAdminName = null, string sysAdminPwd = null)
        {
            return bll.Login(sysAdminName, sysAdminPwd);
        }
    }
}
