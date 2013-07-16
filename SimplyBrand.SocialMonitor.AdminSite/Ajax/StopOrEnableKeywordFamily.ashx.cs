using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.AdminSite.Ajax
{
    /// <summary>
    /// StopOrEnableKeywordFamily 的摘要说明
    /// </summary>
    public class StopOrEnableKeywordFamily : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            try
            {
                int keyId = context.Request.Form["sysUserId"] == null ? 0 : int.Parse(context.Request.Form["sysUserId"].ToString());
                int sysUserId = context.Request.Form["q"] == null ? 0 : int.Parse(context.Request.Form["q"].ToString());
                Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();
                string result = target.UpdateStatus(keyId, sysUserId.ToString(), base.SysAdminId.ToString());
                context.Response.Write(result);
            }
            catch (Exception ex)
            {             
            }                                            
        }         
    }
}