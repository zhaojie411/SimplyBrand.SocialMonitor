using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// DeleteDataInfo 的摘要说明
    /// </summary>
    public class DeleteDataInfo : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            long dataid = context.Request.Form["dataid"] == null ? 0 : Convert.ToInt64(context.Request.Form["dataid"].ToString()); 
            Controller.DataCenterSeachController target = new Controller.DataCenterSeachController();
            context.Response.Write(target.Delete(dataid));
            
        }

      
    }
}