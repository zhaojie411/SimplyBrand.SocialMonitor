using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            SysAdminController target = new SysAdminController();
            string username = context.Request.Form["username"] == null ? string.Empty : context.Request.Form["username"].ToString();
            string password = context.Request.Form["password"] == null ? string.Empty : context.Request.Form["password"].ToString();
            string remember = context.Request.Form["remember"] == null ? "false" : context.Request.Form["remember"].ToString();
            bool isremember = bool.Parse(remember);
            string json = target.Login(username, password);
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);

            if (result.issucc)
            {
                string adminunique = result.data.id + "|" + result.data.name + "|" + DateTime.Now.Ticks;
                HttpCookie adminNameCookie = new HttpCookie(sysUserName, username);
                HttpCookie adminUnique = new HttpCookie(sysUserUnique, EncryHelper.EncryCurrentInfo(adminunique));


                if (isremember)
                {
                    adminNameCookie.Expires = DateTime.Now.AddDays(15);
                    adminUnique.Expires = DateTime.Now.AddDays(15);
                }
                context.Response.SetCookie(adminNameCookie);
                context.Response.SetCookie(adminUnique);
            }
            context.Response.Write(json);

        }


    }
}