using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetUserReport 的摘要说明
    /// </summary>
    public class GetUserReport : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            UserReportController target = new UserReportController();
            int reporttype = context.Request.Form["reporttype"] == null ? 0 : int.Parse(context.Request.Form["reporttype"].ToString());
            string date = context.Request.Form["date"] == null ? "" : context.Request.Form["date"].ToString();
            context.Response.Write(target.GetUserReport(SysUserId, reporttype, date));
        }


    }
}