using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetSettingInfo 的摘要说明
    /// </summary>
    public class GetSettingInfo : BaseAjax
    {    
        public override  void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            SimplyBrand.SocialMonitor.Controller.SysUserController target = new Controller.SysUserController();

            string oldpwd = context.Request["oldpwd"] == null ? "": context.Request["oldpwd"].ToString();
            string newpwd = context.Request["newpwd"] == null ? "" : context.Request["newpwd"].ToString();

            context.Response.Write(target.UpdateSysUserPwd(base.SysUserId, oldpwd, newpwd));
        }  
        
    }
}