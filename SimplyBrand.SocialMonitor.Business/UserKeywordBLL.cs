using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business
{
   public class UserKeywordBLL
    {
       /// <summary>
       /// 添加数据
       /// </summary>
       /// <param name="sysadminid"></param>
       /// <param name="familyid"></param>
       /// <param name="keyname"></param>
       /// <param name="isprimary"></param>
       /// <param name="isforbid"></param>
       /// <returns></returns>
       public string Add(string sysadminid,string familyid,string keyname,string isprimary,string isforbid)
       {

           ResponseUserKeywordJson response=new ResponseUserKeywordJson();         
           response.issucc = true;
           response.data = new List<UserKeywordJson>();
           if (string.IsNullOrEmpty(keyname))
           {
               response.issucc = false;
               response.errormsg = "您输入的关键词为空！";
               return JsonHelper.ToJson(response);
           }
           try
           {
               UserKeywordParameterBuilder  builder=new UserKeywordParameterBuilder();            
               builder.Clear();
               builder.Append(  UserKeywordColumn.KeywordFamilyId, familyid);
               builder.Append(UserKeywordColumn.KeywordContent, keyname.Trim());
               TList<UserKeyword> _modelList = DataRepository.UserKeywordProvider.Find(builder.GetParameters());
               if (_modelList.Count>0)
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
 
          UserKeyword entity=new UserKeyword();         
           entity.KeywordContent = keyname;
           entity.IsPrimary =true;
           entity.KeywordFamilyId = Convert.ToInt32(familyid);
           entity.CreateTime=DateTime.Now;
           if (isforbid == "1")
	        {
                entity.IsForbid = true;
	        }
           else
	        {
                entity.IsForbid = false;
	        }
        
           try
           {
               DataRepository.UserKeywordProvider.Insert(entity);
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
       /// 查询数据
       /// </summary>
       /// <param name="sysuserid"></param>
       /// <param name="managerId"></param>
       /// <param name="familyid"></param>
       /// <param name="likeKeyName"></param>
       /// <returns></returns>
       public string Find(string sysuserid, int managerId,int familyid, string likeKeyName)
       {
           ResponseUserKeywordJson response = new ResponseUserKeywordJson();
           response.issucc = true;
           response.data = new List<UserKeywordJson>();      
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

               UserKeywordParameterBuilder builder = new UserKeywordParameterBuilder();
               builder.Clear();
               builder.Junction = SqlUtil.AND;
               if (!string.IsNullOrEmpty(likeKeyName))
               {
                   builder.AppendLike(UserKeywordColumn.KeywordContent, string.Format("%{0}%", likeKeyName));
               }
               builder.Append(UserKeywordColumn.KeywordFamilyId,familyid.ToString());
         

               KeywordFamilySortBuilder sortb = new KeywordFamilySortBuilder();
               sortb.AppendDESC(KeywordFamilyColumn.KeywordFamilyId);

               TList<UserKeyword> userkeyword = DataRepository.UserKeywordProvider.Find(builder.GetParameters(), sortb);

               foreach (UserKeyword item in userkeyword)
               {
                   UserKeywordJson keywordFamilyJson = new UserKeywordJson();
                   keywordFamilyJson = JsonEntityUtility.SetJsonEntity(keywordFamilyJson, item) as UserKeywordJson;
                   response.data.Add(keywordFamilyJson);
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
       /// 更新 用户关键词数据
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="keyid"></param>
       /// <param name="keyname"></param>
       /// <param name="isforbid"></param>
       /// <returns></returns>
       public string Update(string userid,string keyid,string keyname,string isforbid)
       {
           ResponseUserKeywordJson response = new ResponseUserKeywordJson();
           response.issucc = true;
           response.data = new List<UserKeywordJson>();
           if (string.IsNullOrEmpty(keyname))
           {
               response.issucc = false;
               response.errormsg = "您输入的关键词为空！";
               return JsonHelper.ToJson(response);
           }
           try
           {              
               UserKeyword _model = DataRepository.UserKeywordProvider.GetByKeywordId(Convert.ToInt32(keyid));
               if (_model == null)
               {
                   response.issucc = false;
                   response.errormsg = "您修改的关键词不存在！";
                   return JsonHelper.ToJson(response);
               }
               else
               {
                   _model.KeywordContent = keyname;
                   if (isforbid=="1")
                   {
                       _model.IsForbid = true;  
                   }
                   else
                   {
                       _model.IsForbid = false;
                   }           
                   try
                   {
                       DataRepository.UserKeywordProvider.Update(_model);
                       UserKeywordJson keywordFamilyJson = new UserKeywordJson();
                       keywordFamilyJson = JsonEntityUtility.SetJsonEntity(keywordFamilyJson, _model) as UserKeywordJson;
                       response.data.Add(keywordFamilyJson);
                       

                   }
                   catch (Exception ex)
                   {
                       response.issucc = false;
                       response.errormsg = ex.Message;
                       return JsonHelper.ToJson(response);
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
        /// 删除用户关键词
        /// </summary>
        /// <param name="keyid"></param>
        /// <returns></returns>
       public string Delete(string keyid)
       {
           ResponseUserKeywordJson response = new ResponseUserKeywordJson();
           response.issucc = true;
           response.data = new List<UserKeywordJson>();  
           try
           {
               response.issucc = DataRepository.UserKeywordProvider.Delete(Convert.ToInt32(keyid));
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
