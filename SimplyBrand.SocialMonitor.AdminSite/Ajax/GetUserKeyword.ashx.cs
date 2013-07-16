using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetUserKeyword 的摘要说明
    /// </summary>
    public class GetUserKeyword : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.UserKeywordController target = new Controller.UserKeywordController();
            int familyid = context.Request.Form["familyid"] == null ? 0 : Convert.ToInt32(context.Request.Form["familyid"].ToString().Trim());
           string keyname = context.Request.Form["keyname"] == null ? "" : context.Request.Form["keyname"].ToString();
           string result=target.Find(base.SysAdminId.ToString(),1,familyid,keyname);
           context.Response.Write(result);
        }      
    }
}