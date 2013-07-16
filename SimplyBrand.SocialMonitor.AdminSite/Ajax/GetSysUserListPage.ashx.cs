using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetSysUserListPage 的摘要说明
    /// </summary>
    public class GetSysUserListPage : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);   
            SysUserController target = new SysUserController();
            string searchname = context.Request["name"];
            int pageindex = context.Request["pageindex"] == null ? 0 : int.Parse(context.Request["pageindex"].ToString());
            int pagesize = context.Request["pagesize"] == null ? 0 : int.Parse(context.Request["pagesize"].ToString());
            if (pagesize > 50)
                pagesize = 50;    
            context.Response.Write(target.SearchPage(searchname, pageindex, pagesize));
        }

    }
}