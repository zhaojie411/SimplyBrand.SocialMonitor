using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (Request.Cookies["sysuserunique"] == null)
            {

                Response.Redirect("/Login.html");
            }
        }
    }
}