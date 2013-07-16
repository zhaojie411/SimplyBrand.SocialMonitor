using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.Business.Utility;
namespace SimplyBrand.SocialMonitor.Business
{
    public class RoleBLL
    {
        public string GetRole()
        {
            ResponseRoleListJson response = new ResponseRoleListJson();
            response.issucc = true;
            response.data = new List<RoleJson>();
            try
            {
                TList<Role> tlist = DataRepository.RoleProvider.GetAll();
                foreach (Role item in tlist)
                {
                    RoleJson roleJson = new RoleJson();
                    roleJson = JsonEntityUtility.SetJsonEntity(roleJson, item) as RoleJson;
                    response.data.Add(roleJson);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = "inner error";
            }

            return JsonHelper.ToJson(response);
        }
    }
}
