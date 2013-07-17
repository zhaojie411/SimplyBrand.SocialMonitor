using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetDataPageListInfo 的摘要说明
    /// </summary>
    public class GetDataPageListInfo : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            SimplyBrand.SocialMonitor.Controller.DataCenterSeachController target = new Controller.DataCenterSeachController();

            string sourceids= context.Request["sourceids"] == "0" ? "": context.Request["sourceids"].ToString();

            string familyids = context.Request["familyids"] == null ? "" : context.Request["familyids"].ToString();
            string keyvalue = context.Request["keyvalue"] == null ? "" : context.Request["keyvalue"].ToString();

            string notkeyvalue = context.Request["notkeyvalue"] == null ? "" : context.Request["notkeyvalue"].ToString();

            string starttime = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");

            string endtime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss" );

            string emotional = context.Request["emotional"] == null ? "" : context.Request["emotional"].ToString();

            int pageindex = context.Request["pageindex"] == "" ? 0 :Convert.ToInt32(context.Request["pageindex"].ToString());  
          
            int pagesize = 30;
            string htmljson = target.GetSeach(sourceids, familyids, keyvalue, notkeyvalue, starttime, endtime, emotional, pagesize, pageindex, base.SysUserId);

           context.Response.Write(htmljson);
        }
    }
}