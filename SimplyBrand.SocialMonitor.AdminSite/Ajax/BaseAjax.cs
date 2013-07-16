using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    public class BaseAjax : IRequiresSessionState, IHttpHandler
    {
        protected int vCodeLength = 5;
        protected string Identify = "Identify";
        protected string sysUserUnique = "adminunique";
        protected string sysUserName = "adminname";
        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (SysAdminId <= 0)
            {
                ResponseJson response = new ResponseJson();
                response.errormsg = "请先登录";
                response.issucc = false;
                context.Response.Write(JsonHelper.ToJson(response));
                context.Response.End();
            }

        }
        public int SysAdminId
        {
            get
            {
                try
                {
                    string cookie = EncryHelper.DencryCurrentInfo(HttpContext.Current.Request.Cookies[sysUserUnique].Value);
                    int SysAdminId = int.Parse(cookie.Split('|')[0]);
                    return SysAdminId;
                }
                catch (Exception ex)
                {

                    LogHelper.WriteLog(ex);
                    return 0;
                }
                return 0;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}