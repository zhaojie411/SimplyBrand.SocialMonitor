using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetKeywordList 的摘要说明
    /// </summary>
    public class GetKeywordList : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);  
            Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();                    
            string username = context.Request.Form["q"] == null ? "" : context.Request.Form["q"].ToString();
            int pageindex = Convert.ToInt32(context.Request.Form["pageindex"]) == null ? 1 : Convert.ToInt32(context.Request.Form["pageindex"]);
            int pagesize = Convert.ToInt32(context.Request.Form["pagesize"]) == null ? 1 : Convert.ToInt32(context.Request.Form["pagesize"]);
            string name = context.Request.Form["name"] == null ? "" : context.Request.Form["name"].ToString();
            if (pagesize > 50)
                pagesize = 50;
            int sysadminid = base.SysAdminId;   
            string regult = target.Find(username, sysadminid,name,pageindex,pagesize);
            context.Response.Write(regult);
                      
        }   
    }
}