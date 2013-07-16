using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business;
namespace SimplyBrand.SocialMonitor.Controller
{
    public class PlatformController
    {
        PlatformBLL bll = new PlatformBLL();

        public string GetPlatform()
        {
            return bll.GetPlatform();
        }
        public string Find(int sysuserid)
        {
            return bll.Find(sysuserid);
        }
    }
}
