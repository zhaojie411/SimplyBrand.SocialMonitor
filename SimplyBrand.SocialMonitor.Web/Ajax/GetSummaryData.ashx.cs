using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetSummaryData 的摘要说明
    /// </summary>
    public class GetSummaryData : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            DataCenterSeachController target = new DataCenterSeachController();
            string keywordfamily = context.Request.Form["keywordfamily"] == null ? "" : context.Request.Form["keywordfamily"].ToString();
            string platforms = context.Request.Form["platforms"] == null ? "" : context.Request.Form["platforms"].ToString();
            bool isToday = context.Request.Form["isToday"] == null ? true : bool.Parse(context.Request.Form["isToday"].ToString());
            string emotionvalues = context.Request.Form["emotionvalues"] == null ? "" : context.Request.Form["emotionvalues"].ToString();
            context.Response.Write(target.GetSummaryData(SysUserId, keywordfamily, platforms, emotionvalues, isToday));
        }


    }
}