using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// GetPermission 的摘要说明
    /// </summary>
    public class GetPermission : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            context.Response.Write(new PermissionController().GetPermission());

        }

    }
}