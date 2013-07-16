using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// UpdateSysUser 的摘要说明
    /// </summary>
    public class UpdateSysUser : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            int sysuserid = context.Request.Form["sysuserid"] == null ? 0 : int.Parse(context.Request.Form["sysuserid"].ToString());
            string username = context.Request.Form["username"] == null ? "" : context.Request.Form["username"].ToString();
            string userpassword = context.Request.Form["userpassword"] == null ? "" : context.Request.Form["userpassword"].ToString();
            //int roleid = context.Request.Form["roleid"] == null ? 0 : int.Parse(context.Request.Form["roleid"].ToString());
            string permission = context.Request.Form["permission"] == null ? "" : context.Request.Form["permission"].ToString();
            string realname = Microsoft.JScript.GlobalObject.decodeURIComponent(context.Request.Form["realname"] == null ? "" : context.Request.Form["realname"].ToString());
            string platform = context.Request.Form["platform"] == null ? "" : context.Request.Form["platform"].ToString();
            string endtime = context.Request.Form["endtime"] == null ? "" : context.Request.Form["endtime"].ToString();
            SysUserController target = new SysUserController();
            context.Response.Write(target.UpdateSysUser(sysuserid, userpassword, realname, permission, platform, endtime));
        }


    }
}