using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// DeleteKeywordFamily 的摘要说明
    /// </summary>
    public class DeleteKeywordFamily : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {  
            base.ProcessRequest(context);         
            Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();
            string keyid = context.Request.Form["familyid"] == "" ? "" : context.Request.Form["familyid"].ToString();

           context.Response.Write(target.Delete(keyid));
        }
    }
}