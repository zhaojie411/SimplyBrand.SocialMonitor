using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;

namespace SimplyBrand.SocialMonitor.Business
{
    public class KeywordFamilyBLL
    {
        /// <summary>
        /// 根据用户获取所有的
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        public string GetKeywordFamily(int sysUserId)
        {
            ResponseKeywordFamilyPageJson response = new ResponseKeywordFamilyPageJson();
            response.issucc = true;
            try
            {
                TList<KeywordFamily> tlist = DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId);
                List<KeywordFamily> list = tlist.Where(p => p.KeywordStatus == (int)EnumKeywordStatus.Enable).ToList();
                response.data = new KeywordFamilyPageJson();
                response.data.count = list.Count;
                response.data.items = new List<KeywordFamilyJson>();
                foreach (KeywordFamily item in list)
                {
                    KeywordFamilyJson json = new KeywordFamilyJson();
                    json = JsonEntityUtility.SetJsonEntity(json, item) as KeywordFamilyJson;
                    response.data.items.Add(json);
                }
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
                LogHelper.WriteLog("", ex);
            }
            return JsonHelper.ToJson(response);
        }

        /// <summary>
        ///  查询sysuserid用户的关键词列表
        /// </summary>
        /// <param name="sysuserid"></param>
        /// <param name="managerId"></param>
        /// <param name="likeKeyName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string Find(string sysuserid, int managerId, string likeKeyName, int pageIndex, int pageSize)
        {
            ResponseKeywordFamilyPageJson response = new ResponseKeywordFamilyPageJson();
            response.issucc = true;
            response.data = new KeywordFamilyPageJson();
            response.data.count = 0;
            response.data.items = new List<KeywordFamilyJson>();
            int count = 0;
            Validation.CommonValidator invalidint = new Validation.CommonValidator();
            try
            {
                invalidint.ValidateIntList(sysuserid);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }
            try
            {
                SysAdminSysUserParameterBuilder builder = new SysAdminSysUserParameterBuilder();
                builder.Clear();
                builder.Append(SysAdminSysUserColumn.SysAdminId, managerId.ToString());
                builder.Append(SysAdminSysUserColumn.SysUserId, sysuserid);
                TList<SysAdminSysUser> sysadminuser = DataRepository.SysAdminSysUserProvider.Find(builder.GetParameters());

                //if (sysadminuser.Count<1)
                //{        
                //  response.issucc = false;
                //  response.errormsg = " 该客户您没有操作权限";
                //  return JsonHelper.ToJson(response);  
                //}                    
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            try
            {
                KeywordFamilyParameterBuilder builder = new KeywordFamilyParameterBuilder();
                builder.Clear();
                builder.Junction = SqlUtil.AND;
                if (!string.IsNullOrEmpty(likeKeyName))
                {
                    builder.AppendLike(KeywordFamilyColumn.KeywordFamilyName, string.Format("%{0}%", likeKeyName));
                }
                builder.Append(KeywordFamilyColumn.SysUserId, sysuserid);
                builder.AppendNotEquals(KeywordFamilyColumn.KeywordStatus, ((int)EnumKeywordStatus.Delete).ToString());
                KeywordFamilySortBuilder sortb = new KeywordFamilySortBuilder();
                sortb.AppendDESC(KeywordFamilyColumn.KeywordFamilyId);

                TList<KeywordFamily> keywordfamily = DataRepository.KeywordFamilyProvider.Find(builder.GetParameters(), sortb, (pageIndex - 1) * pageSize, pageSize, out count);

                foreach (KeywordFamily item in keywordfamily)
                {
                    KeywordFamilyJson keywordFamilyJson = new KeywordFamilyJson();
                    keywordFamilyJson = JsonEntityUtility.SetJsonEntity(keywordFamilyJson, item) as KeywordFamilyJson;
                    response.data.items.Add(keywordFamilyJson);
                }
                response.data.count = response.data.items.Count;

            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }
            return JsonHelper.ToJson(response);
        }
        /// <summary>
        /// 添加关键词家族
        /// </summary>
        /// <param name="keywordName"></param>
        /// <param name="keyWordStatus"></param>
        /// <param name="sysuserid"></param>
        /// <returns></returns>
        public string Add(string keywordName, string keyWordStatus, string sysuserid)
        {
            ResponseKeywordFamilyJson response = new ResponseKeywordFamilyJson();
            response.issucc = true;
            response.data = new List<KeywordFamilyJson>();
            if (string.IsNullOrEmpty(keywordName))
            {
                response.issucc = false;
                response.errormsg = "您输入的关键词为空！";
                return JsonHelper.ToJson(response);
            }
            try
            {

                KeywordFamilyParameterBuilder builder = new KeywordFamilyParameterBuilder();
                builder.Clear();
                builder.Append(KeywordFamilyColumn.SysUserId, sysuserid);
                builder.Append(KeywordFamilyColumn.KeywordFamilyName, keywordName.Trim());
                TList<KeywordFamily> _model = DataRepository.KeywordFamilyProvider.Find(builder.GetParameters());
                if (_model.Count > 0)
                {
                    response.issucc = false;
                    response.errormsg = "该用户已添加该关键词！";
                    return JsonHelper.ToJson(response);
                }

            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            KeywordFamily entity = new KeywordFamily();
            entity.KeywordFamilyName = keywordName;
            entity.KeywordStatus = Convert.ToInt16(keyWordStatus);
            entity.SysUserId = Convert.ToInt32(sysuserid);
            try
            {
                DataRepository.KeywordFamilyProvider.Insert(entity);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            return JsonHelper.ToJson(response);
        }
        /// <summary>
        /// 修改状态数据
        /// </summary>
        /// <param name="keywordId"></param>
        /// <param name="sysuserid"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public string UpdateStatus(int keywordId, string sysuserid, string managerId)
        {
            ResponseKeywordFamilyJson response = new ResponseKeywordFamilyJson();
            response.issucc = true;
            response.data = new List<KeywordFamilyJson>();
            try
            {
                SysAdminSysUserParameterBuilder builder = new SysAdminSysUserParameterBuilder();
                builder.Clear();
                builder.Append(SysAdminSysUserColumn.SysAdminId, managerId.ToString());
                builder.Append(SysAdminSysUserColumn.SysUserId, sysuserid);
                TList<SysAdminSysUser> sysadminuser = DataRepository.SysAdminSysUserProvider.Find(builder.GetParameters());
                //if (sysadminuser.Count<1)
                //{        
                //  response.issucc = false;
                //  response.errormsg = " 该客户您没有操作权限";
                //  return JsonHelper.ToJson(response);  
                //}                    
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            try
            {

                KeywordFamily _model = DataRepository.KeywordFamilyProvider.GetByKeywordFamilyId(keywordId);
                if (_model == null)
                {
                    response.issucc = false;
                    response.errormsg = "您修改的关键词不存在";
                    return JsonHelper.ToJson(response);
                }
                else
                {
                    if (_model.KeywordStatus == (int)EnumSysUserStatus.Enable)
                    {
                        _model.KeywordStatus = (int)EnumSysUserStatus.Stop;
                    }
                    else
                    {
                        _model.KeywordStatus = (int)EnumSysUserStatus.Enable;
                    }
                    if (DataRepository.KeywordFamilyProvider.Update(_model))
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
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }



            return JsonHelper.ToJson(response);
        }

        /// <summary>
        /// 修改单挑关键词族数据
        /// </summary>
        /// <param name="keywordid"></param>
        /// <param name="keywordName"></param>
        /// <param name="keyWordStatus"></param>
        /// <param name="sysuserid"></param>
        /// <returns></returns>
        public string Update(string keywordid, string keywordName, string keyWordStatus, string sysuserid)
        {
            ResponseKeywordFamilyJson response = new ResponseKeywordFamilyJson();
            response.issucc = true;
            response.data = new List<KeywordFamilyJson>();
            if (string.IsNullOrEmpty(keywordName))
            {
                response.issucc = false;
                response.errormsg = "您输入的关键词为空！";
                return JsonHelper.ToJson(response);
            }
            try
            {
                KeywordFamily _model = DataRepository.KeywordFamilyProvider.GetByKeywordFamilyId(Convert.ToInt32(keywordid));
                if (_model == null)
                {
                    response.issucc = false;
                    response.errormsg = "您修改的关键词不存在！";
                    return JsonHelper.ToJson(response);
                }
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }


            KeywordFamily entity = new KeywordFamily();
            entity.KeywordFamilyId = Convert.ToInt32(keywordid);
            entity.KeywordFamilyName = keywordName;
            entity.KeywordStatus = Convert.ToInt16(keyWordStatus);
            entity.SysUserId = Convert.ToInt32(sysuserid);
            try
            {
                DataRepository.KeywordFamilyProvider.Update(entity);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            return JsonHelper.ToJson(response);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="familyid"></param>
        /// <returns></returns>
        public string Delete(string familyid)
        {
            ResponseKeywordFamilyJson response = new ResponseKeywordFamilyJson();
            response.issucc = true;
            response.data = new List<KeywordFamilyJson>();
            // KeywordFamilyParameterBuilder buider = new KeywordFamilyParameterBuilder();
            try
            {
                TList<UserKeyword> userkeywordlist = DataRepository.UserKeywordProvider.GetByKeywordFamilyId(Convert.ToInt32(familyid));
                if (userkeywordlist.Count > 0)
                {
                    response.issucc = false;
                    response.errormsg = "该品牌下面有关键词...请手动删除";
                    return JsonHelper.ToJson(response);
                }

            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }



            try
            {
                response.issucc = DataRepository.KeywordFamilyProvider.Delete(Convert.ToInt32(familyid));
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }

            return JsonHelper.ToJson(response);
        }
    }
}
