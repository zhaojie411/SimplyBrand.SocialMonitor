using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// CreateUserKeyword 的摘要说明
    /// </summary>
    public class CreateUserKeyword : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.UserKeywordController target = new Controller.UserKeywordController();
            string familyid = context.Request.Form["familyid"] == null ? "" : context.Request.Form["familyid"].ToString();
            string keyname = context.Request.Form["keyname"] == null ? "" : context.Request.Form["keyname"].ToString();
            string isforbid = context.Request.Form["isforbid"] == null ? "" : context.Request.Form["isforbid"].ToString();
            string result=target.Add(base.SysAdminId.ToString(), familyid,keyname,"1",isforbid);
            context.Response.Write(result);
        }
    }
}