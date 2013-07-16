using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// PostContactUser 的摘要说明
    /// </summary>
    public class PostContactUser : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.ContactUserController target = new Controller.ContactUserController();              
             string linkman = context.Request["linkman"] == null ? "" : context.Request["linkman"].ToString();
             int linkId = 0;// Convert.ToInt32(context.Request["linkId"]) == null ? 0 : Convert.ToInt32(context.Request["linkId"]);
             string linkTel = context.Request["linkTel"] == null ? "" : context.Request["linkTel"].ToString();
             string linkEmail = context.Request["linkEmail"] == null ? "" : context.Request["linkEmail"].ToString();
             int txtId = 0;// Convert.ToInt32(context.Request["txtId"]) == null ? 0 : Convert.ToInt32(context.Request["txtId"]);
             string txtman = context.Request["txtman"] == null ? "" : context.Request["txtman"].ToString();
             string txtTel = context.Request["txtTel"] == null ? "" : context.Request["txtTel"].ToString();
             string txtEmail = context.Request["txtEmail"] == null ? "" : context.Request["txtEmail"].ToString();
             string reslt = target.Update(base.SysUserId, linkId, linkman, linkTel, linkEmail, txtId, txtman, txtTel, txtEmail);
             context.Response.Write(reslt);
        
        } 
    }
}