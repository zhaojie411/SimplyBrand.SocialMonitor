using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetContactUser 的摘要说明
    /// </summary>
    public class GetContactUser : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            SimplyBrand.SocialMonitor.Controller.ContactUserController target = new Controller.ContactUserController();
            string regult=target.Find(base.SysUserId); 
            context.Response.Write(regult);
        }
       
    }
}