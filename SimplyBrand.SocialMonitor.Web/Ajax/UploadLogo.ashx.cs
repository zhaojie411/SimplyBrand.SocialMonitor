using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Controller;
namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// UploadLogo 的摘要说明
    /// </summary>
    public class UploadLogo : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["uploadLogoPath"].ToString());
            Guid guid = Guid.NewGuid();
            string name = guid.ToString() + "_" + file.FileName;
            string logoName = @"/" + ConfigurationManager.AppSettings["uploadLogoPath"].ToString() + @"/" + name;
            string path = uploadPath + "\\" + name;
            SysUserController target = new SysUserController();
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(path);
                context.Response.Write(target.UpdateLogo(SysUserId, logoName));
            }
        }


    }
}