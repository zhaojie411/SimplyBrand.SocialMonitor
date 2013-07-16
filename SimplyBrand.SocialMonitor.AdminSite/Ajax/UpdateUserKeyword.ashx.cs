using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// UpdateUserKeyword 的摘要说明
    /// </summary>
    public class UpdateUserKeyword : BaseAjax
    {
        public override void ProcessRequest(HttpContext context)
        {
           base.ProcessRequest(context);           
           Controller.UserKeywordController target = new Controller.UserKeywordController();
           string keyid = context.Request.Form["keyid"] == null ? "": context.Request.Form["keyid"].ToString().Trim();           
           string keyname = context.Request.Form["keyname"] == null ? "" : context.Request.Form["keyname"].ToString();
           string isforbid = context.Request.Form["isforbid"] == null ? "" : context.Request.Form["isforbid"].ToString();
           string result = target.Update(base.SysAdminId.ToString(), keyid, keyname, isforbid);
           context.Response.Write(result);
        }          
    }
}