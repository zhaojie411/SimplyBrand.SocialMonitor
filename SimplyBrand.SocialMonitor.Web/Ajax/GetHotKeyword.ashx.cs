using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetHotKeyword 的摘要说明
    /// </summary>
    public class GetHotKeyword : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            string keywordfamily = context.Request["keywordfamily"] == null ? "" : context.Request["keywordfamily"].ToString();
            HotKeywordController target = new HotKeywordController();
            context.Response.Write(target.GetHotKeyword(SysUserId, keywordfamily));

        }


    }
}