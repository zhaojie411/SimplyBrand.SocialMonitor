using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            SysUserController target = new SysUserController();
            string username = context.Request.Form["username"] == null ? string.Empty : context.Request.Form["username"].ToString();
            string password = context.Request.Form["password"] == null ? string.Empty : context.Request.Form["password"].ToString();
            string remember = context.Request.Form["remember"] == null ? "false" : context.Request.Form["remember"].ToString();
            //string vcode = context.Request.Form["vcode"] == null ? string.Empty : context.Request.Form["vcode"].ToString();
            //if (context.Session[Identify] == null || vcode.ToLower() != context.Session[Identify].ToString().ToLower())
            //{
            //    ResponseJson response = new ResponseJson() { errormsg = "验证码不正确", issucc = false };
            //    context.Response.Write(JsonHelper.ToJson(response));
            //    context.Response.End();
            //}
            bool isremember = bool.Parse(remember);
            string json = target.Login(username, password);
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);

            if (result.issucc)
            {
                string adminunique = result.data.id + "|" + result.data.name + "|" + DateTime.Now.Ticks;
                HttpCookie sysUserNameCookie = new HttpCookie(sysUserName, username);
                HttpCookie sysUserLogoCookie = new HttpCookie(sysUserLogo, result.data.logo);
                HttpCookie sysUserUniqueCookie = new HttpCookie(sysUserUnique, EncryHelper.EncryCurrentInfo(adminunique));
                if (isremember)
                {
                    sysUserNameCookie.Expires = DateTime.Now.AddDays(15);
                    sysUserLogoCookie.Expires = DateTime.Now.AddDays(15);
                    sysUserUniqueCookie.Expires = DateTime.Now.AddDays(15);
                }
                context.Response.SetCookie(sysUserNameCookie);
                context.Response.SetCookie(sysUserLogoCookie);
                context.Response.SetCookie(sysUserUniqueCookie);
            }
            context.Response.Write(json);
        }


    }
}