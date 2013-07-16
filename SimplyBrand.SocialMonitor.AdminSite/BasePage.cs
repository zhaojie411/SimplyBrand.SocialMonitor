using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (Request.Cookies["adminunique"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}