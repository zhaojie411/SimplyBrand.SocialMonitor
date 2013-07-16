using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetPlatFrom 的摘要说明
    /// </summary>
    public class GetPlatFrom : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.PlatformController target = new Controller.PlatformController();
            string html=target.Find(base.SysUserId);
            context.Response.Write(html);
        }       
    }
}