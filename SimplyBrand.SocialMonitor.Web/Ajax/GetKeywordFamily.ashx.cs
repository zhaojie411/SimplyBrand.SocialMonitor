using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetKeywordFamily 的摘要说明
    /// </summary>
    public class GetKeywordFamily : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            KeywordFamilyController target = new KeywordFamilyController();
            context.Response.Write(target.GetKeywordFamily(SysUserId));

        }


    }
}