using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Entities;

namespace SimplyBrand.SocialMonitor.Business
{
    public class PermissionBLL
    {
        public string GetPermission()
        {
            ResponsePermissionListJson response = new ResponsePermissionListJson();
            response.issucc = true;
            response.data = new List<PermissionJson>();
            try
            {
                TList<Permission> tlist = DataRepository.PermissionProvider.GetAll();
                foreach (Permission item in tlist)
                {
                    PermissionJson permissionJson = new PermissionJson();
                    permissionJson = JsonEntityUtility.SetJsonEntity(permissionJson, item) as PermissionJson;
                    response.data.Add(permissionJson);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
            }
            return JsonHelper.ToJson(response);
        }
    }
}
