using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    public class BaseAjax : IRequiresSessionState, IHttpHandler
    {
        protected int vCodeLength = 5;
        protected string Identify = "Identify";
        protected string sysUserUnique = "sysuserunique";
        protected string sysUserName = "sysusername";
        protected string sysUserLogo = "sysuserlogo";
        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (SysUserId <= 0)
            {
                ResponseJson response = new ResponseJson();
                response.errormsg = "请先登录";
                response.issucc = false;
                context.Response.Write(JsonHelper.ToJson(response));
                context.Response.End();
            }
        }

        public int SysUserId
        {
            get
            {
                try
                {
                    string cookie = EncryHelper.DencryCurrentInfo(HttpContext.Current.Request.Cookies[sysUserUnique].Value);
                    int sysUserId = int.Parse(cookie.Split('|')[0]);
                    if (new SysUserController().CheckLogin(sysUserId))
                    {
                        return sysUserId;
                    }
                }
                catch (Exception ex)
                {

                    LogHelper.WriteLog(ex);
                    return 0;
                }
                return 0;
            }
        }
        public string UserName = "";

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}