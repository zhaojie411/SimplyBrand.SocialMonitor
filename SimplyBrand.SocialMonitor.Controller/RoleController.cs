using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;
namespace SimplyBrand.SocialMonitor.Controller
{
    public class RoleController
    {
        RoleBLL bll = new RoleBLL();

        public string GetRole()
        {
            return bll.GetRole();
        }
    }
}
