using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// GetFamily 的摘要说明
    /// </summary>
    public class GetFamily : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            Controller.KeywordFamilyController target = new Controller.KeywordFamilyController();
          //  target.Find(base.SysUserId.ToString());
        }

     
    }
}