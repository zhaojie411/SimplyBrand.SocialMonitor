using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetRole 的摘要说明
    /// </summary>
    public class GetRole : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            RoleController target = new RoleController();
            context.Response.Write(target.GetRole());
        }


    }
}