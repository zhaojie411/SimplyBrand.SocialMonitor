using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// UpdateDataEmotional 的摘要说明
    /// </summary>
    public class UpdateDataEmotional : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            //dataid: objid, motionalvalue: value}
            long dataid = context.Request.Form["dataid"] == null ? 0 :Convert.ToInt64(context.Request.Form["dataid"].ToString());
            string motionalvalue=context.Request.Form["motionalvalue"] == null ? "":context.Request.Form["motionalvalue"].ToString();
            Controller.DataCenterSeachController target = new Controller.DataCenterSeachController();
            context.Response.Write(target.UpdateEmotional(dataid, motionalvalue));
            
        } 
       
    }
}