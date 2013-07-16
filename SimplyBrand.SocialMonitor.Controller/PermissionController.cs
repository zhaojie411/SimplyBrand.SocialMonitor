using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;

namespace SimplyBrand.SocialMonitor.Controller
{
    public class PermissionController
    {
        PermissionBLL bll = new PermissionBLL();
        public string GetPermission()
        {
            return bll.GetPermission();
        }
    }
}
