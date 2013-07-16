using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// DeleteUserKeyWord 的摘要说明
    /// </summary>
    public class DeleteUserKeyWord : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.UserKeywordController target = new Controller.UserKeywordController(); 
            string keyid = context.Request.Form["keyid"] == "" ? "" : context.Request.Form["keyid"].ToString();
            context.Response.Write(target.Delete(keyid));
        }
                
    }
}