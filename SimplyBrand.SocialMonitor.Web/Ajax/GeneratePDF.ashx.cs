using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GeneratePDF 的摘要说明
    /// </summary>
    public class GeneratePDF : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            UserReportController target = new UserReportController();
            string from = context.Request.Form["from"] == null ? "" : context.Request.Form["from"].ToString();
            string to = context.Request.Form["to"] == null ? "" : context.Request.Form["to"].ToString();
            string keywordfamily = context.Request.Form["keywordfamily"] == null ? "" : context.Request.Form["keywordfamily"].ToString();
            string platforms = context.Request.Form["platforms"] == null ? "" : context.Request.Form["platforms"].ToString();
            string emotionvalues = context.Request.Form["emotionvalues"] == null ? "" : context.Request.Form["emotionvalues"].ToString();
            context.Response.Write(target.GeneratePDF(SysUserId, from, to, Business.Utility.EnumReportType.Custom, false, platforms, keywordfamily, emotionvalues));


        }


    }
}