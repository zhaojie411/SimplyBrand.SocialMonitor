using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.Validation;
using System.IO;
namespace SimplyBrand.SocialMonitor.Business
{
    public class SysUserBLL
    {

        public bool CheckLogin(int sysUserId)
        {
            ResponseJson response = new ResponseJson();
            SysUser SysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
            if (SysUser == null || SysUser.SysUserStatus != (int)EnumSysUserStatus.Enable || SysUser.SysUserEndTime < DateTime.Now)
            {
                return false;
            }
            return true;

        }


        #region 前台用户登录

        /// <summary>
        /// 前台用户登录
        /// 返回ResponseSysUserJson
        /// </summary>
        /// <param name="SysUserName"></param>
        /// <param name="SysUserPassword"></param>
        /// <returns></returns>
        public string Login(string sysUserName = null, string sysUserPwd = null)
        {
            ResponseSysUserJson response = new ResponseSysUserJson();
            string msg = "";
            response.issucc = true;
            try
            {

                SysUser SysUser = new SysUser() { SysUserName = sysUserName, SysUserPwd = sysUserPwd };

                SysUserValidator validator = new SysUserValidator();
                if (!validator.ValidationLogin(SysUser, out msg))
                {
                    response.issucc = false;
                    response.errormsg = msg;
                    return JsonHelper.ToJson(response);
                }

                SysUser = Find(sysUserName, EncryHelper.MD5Encrypt(sysUserPwd));
                if (SysUser == null)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.NAMEORPASSWORD_ERROR;
                    return JsonHelper.ToJson(response);
                }
                if (SysUser.SysUserStatus == (short)EnumSysUserStatus.Stop)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERSTOP_INFO;
                    return JsonHelper.ToJson(response);
                }
                if (SysUser.SysUserStatus == (short)EnumSysUserStatus.Delete)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERDELETE_INFO;
                    return JsonHelper.ToJson(response);

                }
                if (SysUser.SysUserEndTime < DateTime.Now)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERENDTIME_INFO;
                    return JsonHelper.ToJson(response);
                }
                response.data = new SysUserJson();
                response.data = JsonEntityUtility.SetJsonEntity(response.data, SysUser) as SysUserJson;

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



        public SysUser Find(string sysUserName, string sysUserPwd)
        {
            SysUserParameterBuilder builder = new SysUserParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.Append(SysUserColumn.SysUserName, sysUserName);
            builder.Append(SysUserColumn.SysUserPwd, sysUserPwd);
            TList<SysUser> tlist = DataRepository.SysUserProvider.Find(builder.GetParameters());
            if (tlist != null && tlist.Count > 0)
                return tlist[tlist.Count - 1];
            return null;
        }

        #region StopOrEnableSysUser

        /// <summary>
        /// 停用/开启某个帐号
        /// 返回ResponseJson
        /// </summary>
        /// <param name="SysUserId"></param>
        /// <returns></returns>
        public string StopOrEnableSysUser(int sysUserId)
        {
            ResponseJson response = new ResponseJson();

            try
            {
                SysUser SysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
                if (SysUser != null)
                {
                    if (SysUser.SysUserStatus == (int)EnumSysUserStatus.Enable)
                    {
                        SysUser.SysUserStatus = (int)EnumSysUserStatus.Stop;
                    }
                    else
                    {
                        SysUser.SysUserStatus = (int)EnumSysUserStatus.Enable;
                    }
                    if (DataRepository.SysUserProvider.Update(SysUser))
                    {
                        response.issucc = true;
                    }
                    else
                    {
                        response.issucc = false;
                    }
                }
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

        #region Delete

        /// <summary>
        /// 删除某个帐号
        /// 返回ResponseJson
        /// </summary>
        /// <param name="SysUserId"></param>
        /// <returns></returns>
        public string DeleteSysUser(int sysUserId)
        {
            ResponseJson response = new ResponseJson();
            response.issucc = false;
            try
            {
                SysUser SysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
                if (SysUser != null)
                {
                    SysUser.SysUserStatus = (short)EnumSysUserStatus.Delete;
                    if (DataRepository.SysUserProvider.Update(SysUser))
                    {
                        response.issucc = true;
                    }
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
        #endregion

        #region SearchPage

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="likeSysUserName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string SearchPage(string likeSysUserName, int pageIndex, int pageSize)
        {
            ResponseSysUserPageJson response = new ResponseSysUserPageJson();
            response.issucc = true;
            int count = 0;
            try
            {
                SysUserParameterBuilder builder = new SysUserParameterBuilder();
                builder.Clear();
                builder.Junction = SqlUtil.AND;
                if (!string.IsNullOrEmpty(likeSysUserName))
                {
                    builder.AppendLike(SysUserColumn.SysUserName, string.Format("%{0}%", likeSysUserName));
                }
                builder.AppendNotEquals(SysUserColumn.SysUserStatus, ((int)EnumSysUserStatus.Delete).ToString());

                SysUserSortBuilder sortb = new SysUserSortBuilder();
                sortb.AppendDESC(SysUserColumn.SysUserId);

                TList<SysUser> tlist = DataRepository.SysUserProvider.Find(builder.GetParameters(), sortb, (pageIndex - 1) * pageSize, pageSize, out count);

                string[] sysUserIds = tlist.Select(p => p.SysUserId).ToList().ConvertAll(p => p.ToString()).ToArray();
                //获取角色
                //Dictionary<int, int> dicRole = FindRole(sysUserIds);
                Dictionary<int, List<int>> dicPermissin = FindPermission(sysUserIds);
                //获取平台
                Dictionary<int, List<int>> dicPlatform = FindPlatform(sysUserIds);
                Dictionary<int, List<ContactUserJson>> dicContact = FindContactUser(sysUserIds);
                response.data = new SysUserPageJson();
                response.data.count = count;
                response.data.items = new List<SysUserJson>();
                foreach (SysUser item in tlist)
                {
                    SysUserJson sysUserJson = new SysUserJson();
                    sysUserJson = JsonEntityUtility.SetJsonEntity(sysUserJson, item) as SysUserJson;
                    //sysUserJson.roleid = dicRole[item.SysUserId];
                    sysUserJson.permissions = dicPermissin[item.SysUserId];
                    sysUserJson.platformids = dicPlatform[item.SysUserId];
                    sysUserJson.contacts = dicContact[item.SysUserId];
                    response.data.items.Add(sysUserJson);
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

        #endregion


        #region CreateSysUser
        /// <summary>
        /// 创建用户
        /// 返回ResponseSysUserJson
        /// </summary>
        /// <param name="sysUserName">用户名</param>
        /// <param name="sysUserPwd">用户密码</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="platforms">多个数据源平台采用逗号分隔</param>
        /// <returns></returns>
        public string CreateSysUser(string sysUserName, string sysUserPwd, int roleId, string platforms, string endDateStr, string sysUserRealName = null)
        {
            ResponseSysUserJson response = new ResponseSysUserJson();
            response.issucc = true;
            string msg = "";
            TransactionManager mgr = DataRepository.Provider.CreateTransaction();
            mgr.BeginTransaction();
            try
            {
                DateTime endTime = DateTime.MinValue;
                DateTime.TryParse(endDateStr, out endTime);
                //验证截止时间
                if (endTime == DateTime.MinValue || endTime < DateTime.Now)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERENDTIMEFORMAT_ERROR;
                    return JsonHelper.ToJson(response);
                }
                //验证选择的平台是否正确
                CommonValidator cvalidator = new CommonValidator();
                try
                {
                    cvalidator.ValidateIntList(platforms);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }

                SysUserValidator validator = new SysUserValidator();

                SysUser entity = new SysUser()
                {
                    SysUserName = sysUserName,
                    SysUserPwd = sysUserPwd,
                    SysUserLogo = string.Empty,
                    SysUserStatus = (short)EnumSysUserStatus.Enable,
                    SysUserAddDate = DateTime.Now,
                    SysUserEndTime = endTime,
                    SysUserRealName = sysUserRealName
                };
                if (!validator.ValidationSysUser(entity, out msg))
                {
                    response.issucc = false;
                    response.errormsg = msg;
                    return JsonHelper.ToJson(response);
                }

                entity.SysUserPwd = EncryHelper.MD5Encrypt(entity.SysUserPwd);

                DataRepository.SysUserProvider.Insert(mgr, entity);

                SysUserRole sysUserRole = new SysUserRole() { RoleId = roleId, SysUserId = entity.SysUserId };

                DataRepository.SysUserRoleProvider.Insert(mgr, sysUserRole);

                List<string> platformList = platforms.Split(',').ToList();
                TList<SysUserPlatform> tlistPlatform = new TList<SysUserPlatform>();
                foreach (string item in platformList)
                {
                    tlistPlatform.Add(new SysUserPlatform() { PlatformId = short.Parse(item), SysUserId = entity.SysUserId });
                }

                if (DataRepository.SysUserPlatformProvider.Insert(mgr, tlistPlatform) == platformList.Count)
                {
                    mgr.Commit();
                    response.data = new SysUserJson();
                    response.data = JsonEntityUtility.SetJsonEntity(response.data, entity) as SysUserJson;
                }
                else
                {
                    mgr.Rollback();
                    response.issucc = false;
                    response.errormsg = ConstHelper.NETWORK_ERROR;
                }

            }
            catch (Exception ex)
            {
                mgr.Rollback();
                LogHelper.WriteLog(ex);
                response.issucc = false;
                if (ex.Message.Contains("UQ_SysUser_SysUserName"))
                {
                    response.errormsg = ConstHelper.SYSUSERHASEXISTS_INFO;
                }
                else
                {
                    response.errormsg = ConstHelper.INNER_ERROR;
                }
            }
            return JsonHelper.ToJson(response);
        }

        public string CreateSysUser(string sysUserName, string sysUserPwd, string sysUserRealName, string endDateStr, string permissions, string platforms, string sysUserEmail = null)
        {
            ResponseSysUserJson response = new ResponseSysUserJson();
            response.issucc = true;
            string msg = "";
            TransactionManager mgr = DataRepository.Provider.CreateTransaction();
            mgr.BeginTransaction();

            try
            {

                DateTime endTime = DateTime.MinValue;
                DateTime.TryParse(endDateStr, out endTime);
                //验证截止时间
                if (endTime == DateTime.MinValue || endTime < DateTime.Now)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERENDTIMEFORMAT_ERROR;
                    return JsonHelper.ToJson(response);
                }
                //验证选择的平台是否正确
                CommonValidator cvalidator = new CommonValidator();
                try
                {
                    cvalidator.ValidateIntList(platforms);
                    cvalidator.ValidateIntList(permissions);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }

                SysUser entity = new SysUser()
                {
                    SysUserName = sysUserName,
                    SysUserPwd = sysUserPwd,
                    SysUserLogo = string.Empty,
                    SysUserStatus = (short)EnumSysUserStatus.Enable,
                    SysUserAddDate = DateTime.Now,
                    SysUserEndTime = endTime,
                    SysUserRealName = sysUserRealName,
                    SysUserEmail = sysUserEmail
                };
                SysUserValidator validator = new SysUserValidator();
                if (!validator.ValidationSysUser(entity, out msg))
                {
                    response.issucc = false;
                    response.errormsg = msg;
                    return JsonHelper.ToJson(response);
                }
                entity.SysUserPwd = EncryHelper.MD5Encrypt(entity.SysUserPwd);
                //插入用户数据
                DataRepository.SysUserProvider.Insert(mgr, entity);
                //角色数据
                Role role = new Role() { RoleName = sysUserName, RoleDescription = sysUserRealName };
                DataRepository.RoleProvider.Insert(mgr, role);
                //角色对应的用户
                SysUserRole sysUserRole = new SysUserRole() { RoleId = role.RoleId, SysUserId = entity.SysUserId };
                DataRepository.SysUserRoleProvider.Insert(mgr, sysUserRole);
                //用户对应的平台数据
                List<string> platformList = platforms.Split(',').ToList();
                TList<SysUserPlatform> tlistPlatform = new TList<SysUserPlatform>();
                foreach (string item in platformList)
                {
                    tlistPlatform.Add(new SysUserPlatform() { PlatformId = short.Parse(item), SysUserId = entity.SysUserId });
                }
                int platformsucc = DataRepository.SysUserPlatformProvider.Insert(mgr, tlistPlatform);

                List<string> permissionsList = permissions.Split(',').ToList();
                TList<RolePermission> rolePermissionTList = new TList<RolePermission>();
                foreach (string item in permissionsList)
                {
                    rolePermissionTList.Add(new RolePermission() { RoleId = role.RoleId, PermissionId = int.Parse(item) });
                }
                int rolePermissionsucc = DataRepository.RolePermissionProvider.Insert(mgr, rolePermissionTList);
                if (platformsucc == tlistPlatform.Count && rolePermissionsucc == rolePermissionTList.Count)
                {
                    mgr.Commit();
                    response.data = new SysUserJson();
                    response.data = JsonEntityUtility.SetJsonEntity(response.data, entity) as SysUserJson;
                }
                else
                {
                    mgr.Rollback();
                    response.issucc = false;
                    response.errormsg = ConstHelper.NETWORK_ERROR;
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

        #endregion


        #region UpdateSysUser

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="sysUserPwd"></param>
        /// <param name="roleId"></param>
        /// <param name="platforms"></param>
        /// <returns></returns>
        public string UpdateSysUser(int sysUserId, string sysUserPwd, int roleId, string platforms, string endDateStr = null)
        {
            ResponseJson response = new ResponseJson();
            response.issucc = false;
            TransactionManager mgr = DataRepository.Provider.CreateTransaction();
            mgr.BeginTransaction();
            string msg = "";
            try
            {
                DateTime endTime = DateTime.MinValue;
                DateTime.TryParse(endDateStr, out endTime);
                //验证截止时间
                if (endTime == DateTime.MinValue || endTime < DateTime.Now)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERENDTIMEFORMAT_ERROR;
                    return JsonHelper.ToJson(response);
                }
                CommonValidator cvalidator = new CommonValidator();
                try
                {
                    cvalidator.ValidateIntList(platforms);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }

                SysUser sysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
                if (sysUser != null)
                {
                    if (!string.IsNullOrEmpty(sysUserPwd))
                    {
                        if (sysUser.SysUserPwd != sysUserPwd && sysUser.SysUserPwd != EncryHelper.MD5Encrypt(sysUserPwd))
                        {
                            sysUser.SysUserPwd = EncryHelper.MD5Encrypt(sysUserPwd);
                        }
                    }
                    sysUser.SysUserEndTime = endTime;

                    SysUserValidator validator = new SysUserValidator();
                    if (!validator.ValidationSysUser(sysUser, out msg))
                    {
                        response.errormsg = msg;
                        return JsonHelper.ToJson(response);
                    }


                    if (DataRepository.SysUserProvider.Update(mgr, sysUser))
                    {
                        TList<SysUserRole> sysUserRoleList = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
                        if (sysUserRoleList.Count > 0)
                        {
                            SysUserRole sysUserRole = sysUserRoleList[sysUserRoleList.Count - 1];
                            if (sysUserRole.RoleId != roleId)
                            {
                                sysUserRole.RoleId = roleId;
                                DataRepository.SysUserRoleProvider.Update(mgr, sysUserRole);
                            }

                        }

                        List<int> platformIdList = platforms.Split(',').ToList().ConvertAll(p => int.Parse(p)).Distinct().ToList();
                        TList<SysUserPlatform> sysUserPlatformTlist = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysUserId);
                        List<int> currentPlatformIdList = sysUserPlatformTlist.Select(p => p.PlatformId).ToList().ConvertAll(p => int.Parse(p.ToString()));
                        //删除
                        List<int> deletePlatformIdList = currentPlatformIdList.Except(platformIdList).ToList();
                        var query = from t in sysUserPlatformTlist
                                    where deletePlatformIdList.Contains(int.Parse(t.PlatformId.ToString()))
                                    select t;
                        TList<SysUserPlatform> sysUserPlatformDelete = new TList<SysUserPlatform>();
                        foreach (var item in query)
                        {
                            sysUserPlatformDelete.Add(item);
                        }
                        int deletecount = DataRepository.SysUserPlatformProvider.Delete(mgr, sysUserPlatformDelete);
                        //新增
                        List<int> addPlatformIdList = platformIdList.Except(currentPlatformIdList).ToList();

                        TList<SysUserPlatform> sysUserPlatformAdd = new TList<SysUserPlatform>();
                        foreach (int item in addPlatformIdList)
                        {
                            sysUserPlatformAdd.Add(new SysUserPlatform() { SysUserId = sysUserId, PlatformId = (short)item });
                        }
                        int addcount = DataRepository.SysUserPlatformProvider.Insert(mgr, sysUserPlatformAdd);
                        if (deletecount == sysUserPlatformDelete.Count && addcount == sysUserPlatformAdd.Count)
                        {
                            mgr.Commit();
                            response.issucc = true;
                            return JsonHelper.ToJson(response);
                        }
                        else
                        {
                            mgr.Rollback();
                            response.issucc = false;
                            response.errormsg = ConstHelper.NETWORK_ERROR;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mgr.Rollback();
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
            }

            return JsonHelper.ToJson(response);
        }
        public string UpdateSysUser(int sysUserId, string sysUserPwd, string sysUserRealName, string permissions, string platforms, string endDateStr, string sysUserEmail = null)
        {
            ResponseJson response = new ResponseJson();
            response.issucc = false;
            TransactionManager mgr = DataRepository.Provider.CreateTransaction();
            mgr.BeginTransaction();
            string msg = "";
            try
            {
                DateTime endTime = DateTime.MinValue;
                DateTime.TryParse(endDateStr, out endTime);
                //验证截止时间
                if (endTime == DateTime.MinValue || endTime < DateTime.Now)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.SYSUSERENDTIMEFORMAT_ERROR;
                    return JsonHelper.ToJson(response);
                }
                CommonValidator cvalidator = new CommonValidator();
                try
                {
                    cvalidator.ValidateIntList(platforms);
                    cvalidator.ValidateIntList(permissions);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }

                SysUser sysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
                if (sysUser != null)
                {
                    if (!string.IsNullOrEmpty(sysUserPwd))
                    {
                        if (sysUser.SysUserPwd != sysUserPwd && sysUser.SysUserPwd != EncryHelper.MD5Encrypt(sysUserPwd))
                        {
                            sysUser.SysUserPwd = EncryHelper.MD5Encrypt(sysUserPwd);
                        }
                    }
                    sysUser.SysUserEndTime = endTime;
                    sysUser.SysUserRealName = sysUserRealName;
                    sysUser.SysUserEmail = sysUserEmail;
                    SysUserValidator validator = new SysUserValidator();
                    if (!validator.ValidationSysUser(sysUser, out msg))
                    {
                        response.errormsg = msg;
                        return JsonHelper.ToJson(response);
                    }


                    if (DataRepository.SysUserProvider.Update(mgr, sysUser))
                    {
                        TList<SysUserRole> sysUserRoleList = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
                        SysUserRole sysUserRole = null;
                        if (sysUserRoleList.Count > 0)
                            sysUserRole = sysUserRoleList[0];//每个用户对应一个角色
                        List<int> permissionIdList = permissions.Split(',').ToList().ConvertAll(p => int.Parse(p)).Distinct().ToList();
                        TList<RolePermission> rolePermissionTList = DataRepository.RolePermissionProvider.GetByRoleId(sysUserRole.RoleId);
                        List<int> currentPermissionIdList = rolePermissionTList.Select(p => p.PermissionId).ToList().ConvertAll(p => int.Parse(p.ToString()));
                        //删除权限
                        List<int> deletePermissionIdList = currentPermissionIdList.Except(permissionIdList).ToList();
                        var querypermission = from t in rolePermissionTList
                                              where deletePermissionIdList.Contains(int.Parse(t.PermissionId.ToString()))
                                              select t;
                        TList<RolePermission> rolePermissionDelete = new TList<RolePermission>();
                        foreach (var item in querypermission)
                        {
                            rolePermissionDelete.Add(item);
                        }
                        int deletepermissioncount = DataRepository.RolePermissionProvider.Delete(mgr, rolePermissionDelete);
                        //新增权限
                        List<int> addPermissionList = permissionIdList.Except(currentPermissionIdList).ToList();

                        TList<RolePermission> rolePermissionAdd = new TList<RolePermission>();
                        foreach (int item in addPermissionList)
                        {
                            rolePermissionAdd.Add(new RolePermission() { RoleId = sysUserRole.RoleId, PermissionId = item });
                        }
                        int addpermissioncount = DataRepository.RolePermissionProvider.Insert(mgr, rolePermissionAdd);
                        ///平台
                        List<int> platformIdList = platforms.Split(',').ToList().ConvertAll(p => int.Parse(p)).Distinct().ToList();
                        TList<SysUserPlatform> sysUserPlatformTlist = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysUserId);
                        List<int> currentPlatformIdList = sysUserPlatformTlist.Select(p => p.PlatformId).ToList().ConvertAll(p => int.Parse(p.ToString()));
                        //删除平台
                        List<int> deletePlatformIdList = currentPlatformIdList.Except(platformIdList).ToList();
                        var query = from t in sysUserPlatformTlist
                                    where deletePlatformIdList.Contains(int.Parse(t.PlatformId.ToString()))
                                    select t;
                        TList<SysUserPlatform> sysUserPlatformDelete = new TList<SysUserPlatform>();
                        foreach (var item in query)
                        {
                            sysUserPlatformDelete.Add(item);
                        }
                        int deletecount = DataRepository.SysUserPlatformProvider.Delete(mgr, sysUserPlatformDelete);
                        //新增平台
                        List<int> addPlatformIdList = platformIdList.Except(currentPlatformIdList).ToList();

                        TList<SysUserPlatform> sysUserPlatformAdd = new TList<SysUserPlatform>();
                        foreach (int item in addPlatformIdList)
                        {
                            sysUserPlatformAdd.Add(new SysUserPlatform() { SysUserId = sysUserId, PlatformId = (short)item });
                        }
                        int addcount = DataRepository.SysUserPlatformProvider.Insert(mgr, sysUserPlatformAdd);
                        if (deletecount == sysUserPlatformDelete.Count && addcount == sysUserPlatformAdd.Count && deletepermissioncount == rolePermissionDelete.Count && addPermissionList.Count == addpermissioncount)
                        {
                            mgr.Commit();
                            response.issucc = true;
                            return JsonHelper.ToJson(response);
                        }
                        else
                        {
                            mgr.Rollback();
                            response.issucc = false;
                            response.errormsg = ConstHelper.NETWORK_ERROR;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mgr.Rollback();
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
            }


            return JsonHelper.ToJson(response);
        }

        #endregion


        #region UpdateSysUserPwd
        public string UpdateSysUserPwd(int sysUserId, string sysUserPwd, string newsUserPwd)
        {
            SysUserValidator validator = new SysUserValidator();
            ResponseSysUserJson response = new ResponseSysUserJson();
            response.issucc = true;
            var msg = "";
            try
            {
                validator.ValidationSysUserPwd(sysUserPwd, out msg);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }
            SysUser entity = new SysUser();
            try
            {
                entity = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }
            if (entity == null)
            {
                response.issucc = false;
                response.errormsg = "没有找到该用户！";
                return JsonHelper.ToJson(response);
            }
            string newspwd = EncryHelper.MD5Encrypt(newsUserPwd);

            if (entity.SysUserPwd != EncryHelper.MD5Encrypt(sysUserPwd))
            {
                response.issucc = false;
                response.errormsg = "原密码不正确";
                return JsonHelper.ToJson(response);
            }

            if (entity.SysUserPwd != newspwd)
            {
                entity.SysUserPwd = newspwd;
                try
                {
                    DataRepository.SysUserProvider.Update(entity);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }

            }
            return JsonHelper.ToJson(response);
        }
        #endregion

        #region UpdateLogo

        /// <summary>
        /// 更新logo
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="logoName"></param>
        /// <returns></returns>
        public string UpdateLogo(int sysUserId, string logoName)
        {
            ResponseSysUserJson response = new ResponseSysUserJson();
            response.issucc = false;
            try
            {
                SysUser sysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
                sysUser.SysUserLogo = logoName;
                if (DataRepository.SysUserProvider.Update(sysUser))
                {

                    response.issucc = true;
                    response.data = new SysUserJson();
                    response.data.logo = logoName;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.errormsg = ConstHelper.INNER_ERROR;

            }
            return JsonHelper.ToJson(response);
        }
        #endregion


        #region 私有方法


        //private Dictionary<int, int> FindRole(string[] sysUserIds)
        //{
        //    SysUserRoleParameterBuilder builder = new SysUserRoleParameterBuilder();
        //    builder.Clear();
        //    builder.Junction = string.Empty;
        //    builder.AppendIn(SysUserRoleColumn.SysUserId, sysUserIds);
        //    TList<SysUserRole> tlist = DataRepository.SysUserRoleProvider.Find(builder.GetParameters());
        //    Dictionary<int, int> dic = new Dictionary<int, int>();
        //    foreach (string item in sysUserIds)
        //    {
        //        if (!dic.ContainsKey(int.Parse(item)))
        //        {
        //            dic.Add(int.Parse(item), new List<int>());
        //        }
        //    }
        //    foreach (SysUserRole item in tlist)
        //    {
        //        if (!dic.ContainsKey(item.SysUserId))
        //        {
        //            dic.Add(item.SysUserId, item.RoleId);
        //        }
        //    }
        //    return dic;
        //}
        private Dictionary<int, List<int>> FindPermission(string[] sysUserIds)
        {
            ViewSysUserRolePermissionParameterBuilder builder = new ViewSysUserRolePermissionParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.AppendIn(ViewSysUserRolePermissionColumn.SysUserId, sysUserIds);
            VList<ViewSysUserRolePermission> vList = DataRepository.ViewSysUserRolePermissionProvider.Find(builder.GetParameters());
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            foreach (string item in sysUserIds)
            {
                if (!dic.ContainsKey(int.Parse(item)))
                {
                    dic.Add(int.Parse(item), new List<int>());
                }
            }
            foreach (ViewSysUserRolePermission item in vList)
            {
                dic[item.SysUserId].Add(item.PermissionId);
            }
            return dic;
        }

        private Dictionary<int, List<ContactUserJson>> FindContactUser(string[] sysUserIds)
        {
            ContactUserParameterBuilder builder = new ContactUserParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.AppendIn(ContactUserColumn.SysUserId, sysUserIds);
            TList<ContactUser> tList = DataRepository.ContactUserProvider.Find(builder.GetParameters());

            Dictionary<int, List<ContactUserJson>> dic = new Dictionary<int, List<ContactUserJson>>();
            foreach (string item in sysUserIds)
            {
                if (!dic.ContainsKey(int.Parse(item)))
                {
                    dic.Add(int.Parse(item), new List<ContactUserJson>());
                }
            }
            foreach (ContactUser item in tList)
            {
                ContactUserJson jsonEntity = new ContactUserJson();
                jsonEntity = JsonEntityUtility.SetJsonEntity(jsonEntity, item) as ContactUserJson;
                dic[(int)item.SysUserId].Add(jsonEntity);
            }
            return dic;
        }


        private Dictionary<int, List<int>> FindPlatform(string[] sysUserIds)
        {
            SysUserPlatformParameterBuilder builder = new SysUserPlatformParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.AppendIn(SysUserPlatformColumn.SysUserId, sysUserIds);
            TList<SysUserPlatform> tlist = DataRepository.SysUserPlatformProvider.Find(builder.GetParameters());
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            foreach (string item in sysUserIds)
            {
                if (!dic.ContainsKey(int.Parse(item)))
                {
                    dic.Add(int.Parse(item), new List<int>());
                }
            }
            foreach (SysUserPlatform item in tlist)
            {
                dic[item.SysUserId].Add(item.PlatformId);
            }
            return dic;
        }


        #endregion

    }
}
