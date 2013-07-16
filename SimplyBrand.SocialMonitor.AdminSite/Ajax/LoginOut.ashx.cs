using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// LoginOut 的摘要说明
    /// </summary>
    public class LoginOut : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            if (context.Request.Cookies[sysUserName] != null)
            {
                context.Response.Cookies[sysUserName].Expires = DateTime.Now.AddDays(-1);
            }
            if (context.Request.Cookies[sysUserUnique] != null)
            {
                context.Response.Cookies[sysUserUnique].Expires = DateTime.Now.AddDays(-1);
            }

            ResponseJson response = new ResponseJson();
            response.issucc = true;
            context.Response.Write(JsonHelper.ToJson(response));
        }


    }
}