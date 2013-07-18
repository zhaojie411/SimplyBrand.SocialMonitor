using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetInfoCenter 的摘要说明
    /// </summary>
    public class GetInfoCenter : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            UserReportController target = new UserReportController();
            context.Response.Write(target.GetInfoCenter(SysUserId));
        }

    }
}