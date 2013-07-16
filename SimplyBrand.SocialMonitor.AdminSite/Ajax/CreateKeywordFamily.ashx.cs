using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// CreateKeywordFamily 的摘要说明
    /// </summary>
    public class CreateKeywordFamily : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
          
            base.ProcessRequest(context);
            string username = context.Request.Form["keyname"] == null ? "" : context.Request.Form["keyname"].ToString();
            string uid = context.Request.Form["userid"] == null ? "" : context.Request.Form["userid"].ToString(); 
            Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();
           string result= target.Add(username, "1", uid);
           context.Response.Write(result);
        } 
    }
}