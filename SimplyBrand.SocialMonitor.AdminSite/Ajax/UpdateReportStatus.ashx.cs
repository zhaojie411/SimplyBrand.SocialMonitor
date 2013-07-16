using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// UpdateReportStatus 的摘要说明
    /// </summary>
    public class UpdateReportStatus : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            UserReportController target = new UserReportController();
            int userreportid = context.Request.Form["userreportid"] == null ? 0 : int.Parse(context.Request.Form["userreportid"].ToString());
            int reportstatus = context.Request.Form["reportstatus"] == null ? 0 : int.Parse(context.Request.Form["reportstatus"].ToString());
            context.Response.Write(target.UpdateReportStatus(userreportid, reportstatus));
        }

    }
}