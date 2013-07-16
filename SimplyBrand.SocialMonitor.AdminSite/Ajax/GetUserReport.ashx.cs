using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
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
            int pagesize = context.Request.Form["pagesize"] == null ? 10 : int.Parse(context.Request.Form["pagesize"].ToString());
            int pageindex = context.Request.Form["pageindex"] == null ? 1 : int.Parse(context.Request.Form["pageindex"].ToString());
            context.Response.Write(target.GetUserReport(SysAdminId, reporttype, pageindex, pagesize));
        }


    }
}