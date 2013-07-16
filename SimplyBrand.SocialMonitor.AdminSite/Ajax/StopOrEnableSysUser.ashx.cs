using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// StopOrEnableSysUser 的摘要说明
    /// </summary>
    public class StopOrEnableSysUser : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            int sysUserId = context.Request.Form["sysUserId"] == null ? 0 : int.Parse(context.Request.Form["sysUserId"].ToString());
            SysUserController target = new SysUserController();
            context.Response.Write(target.StopOrEnableSysUser(sysUserId));
        }


    }
}