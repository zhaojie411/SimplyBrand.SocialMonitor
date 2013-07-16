using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.Business.Validation;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
namespace SimplyBrand.SocialMonitor.Business
{
    public class SysAdminBLL
    {
        #region 后台管理员登录

        /// <summary>
        /// 后台管理员登录
        /// 返回ResponseSysAdminJson
        /// </summary>
        /// <param name="sysAdminName"></param>
        /// <param name="sysAdminPwd"></param>
        /// <returns></returns>
        public string Login(string sysAdminName = null, string sysAdminPwd = null)
        {
            ResponseSysAdminJson response = new ResponseSysAdminJson();
            string msg = "";
            response.issucc = true;
            try
            {

                SysAdmin sysAdmin = new SysAdmin() { SysAdminName = sysAdminName, SysAdminPwd = sysAdminPwd };

                SysAdminValidator validator = new SysAdminValidator();
                if (!validator.ValidateSysAdmin(sysAdmin, out msg))
                {
                    response.issucc = false;
                    response.errormsg = msg;
                    return JsonHelper.ToJson(response);
                }

                sysAdmin = Find(sysAdminName, EncryHelper.MD5Encrypt(sysAdminPwd));
                if (sysAdmin == null)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.NAMEORPASSWORD_ERROR;
                    return JsonHelper.ToJson(response);
                }
                response.data = new SysAdminJson();
                response.data = JsonEntityUtility.SetJsonEntity(response.data, sysAdmin) as SysAdminJson;

            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
                LogHelper.WriteLog(ex);
            }
            return JsonHelper.ToJson(response);
        } 
        #endregion



        


        public SysAdmin Find(string sysAdminName, string sysAdminPwd)
        {
            SysAdminParameterBuilder builder = new SysAdminParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.Append(SysAdminColumn.SysAdminName, sysAdminName);
            builder.Append(SysAdminColumn.SysAdminPwd, sysAdminPwd);
            TList<SysAdmin> tlist = DataRepository.SysAdminProvider.Find(builder.GetParameters());
            if (tlist != null && tlist.Count > 0)
                return tlist[tlist.Count - 1];
            return null;
        }
    }
}
