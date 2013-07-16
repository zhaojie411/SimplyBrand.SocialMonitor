using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// UpdateKeywordFamily 的摘要说明
    /// </summary>
    public class UpdateKeywordFamily : BaseAjax
    {  
        public override void ProcessRequest(HttpContext context)
        {                 
            base.ProcessRequest(context);   
            Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();
            string username = context.Request.Form["keyname"] == null ? "" : context.Request.Form["keyname"].ToString();
            string uid = context.Request.Form["userid"] == null ? "" : context.Request.Form["userid"].ToString(); 
             string  kid= context.Request.Form["keyid"] == null ? "" : context.Request.Form["keyid"].ToString();
             string keystatus = context.Request.Form["keystatus"] == null ? "" : context.Request.Form["keystatus"].ToString();
            string result=target.Update(kid, username, keystatus, uid);
            context.Response.Write(result);

        }
    }
}