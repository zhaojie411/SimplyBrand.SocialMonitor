using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
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

            string sourceids = context.Request["sourceids"] == "0" ? "" : context.Request["sourceids"].ToString();             
            string keyvalue = context.Request["keyvalue"] == null ? "" : context.Request["keyvalue"].ToString();

            string notkeyvalue = context.Request["notkeyvalue"] == null ? "" : context.Request["notkeyvalue"].ToString();

            string starttime = context.Request["starttime"] == null ? "" : context.Request["starttime"].ToString();

            string endtime = context.Request["endtime"] == null ? "" : context.Request["endtime"].ToString();

            string emotional = context.Request["emotional"] == null ? "" : context.Request["emotional"].ToString();

            int pageindex = context.Request["pageindex"] == "" ? 0 : Convert.ToInt32(context.Request["pageindex"].ToString());  

            int pagesize = 30;
            string htmljson = target.GetSeach(sourceids, keyvalue, notkeyvalue, starttime, endtime, emotional, pagesize, pageindex);

            context.Response.Write(htmljson);
        }
    }
}