using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;

namespace SimplyBrand.SocialMonitor.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (Request.Cookies["sysuserunique"] != null)
                {
                    string cookie = EncryHelper.DencryCurrentInfo(HttpContext.Current.Request.Cookies["sysuserunique"].Value);
                    int sysUserId = int.Parse(cookie.Split('|')[0]);
                    if (!new SysUserController().CheckLogin(sysUserId))
                    {
                        Response.Redirect("/Login.html");
                    }
                }
                else
                {
                    Response.Redirect("/Login.html");
                }
            }
            catch (Exception)
            {

                Response.Redirect("/Login.html");
            }
        }
    }
}