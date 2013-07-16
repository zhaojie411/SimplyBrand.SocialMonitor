using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetPlatform 的摘要说明
    /// </summary>
    public class GetPlatform : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            PlatformController target = new PlatformController();
            context.Response.Write(target.GetPlatform());
        }


    }
}