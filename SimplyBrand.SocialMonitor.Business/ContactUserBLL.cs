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
   public class ContactUserBLL
    {
       public string Find(int sysuserid)
       {
           ResponseContactUserJson   response=new ResponseContactUserJson();       
           response.issucc = true;
           response.item = new List<ContactUserJson>();
         
           
            List<ContactUser> contactuserlist = new List<ContactUser>();
                try 
	            {
                    contactuserlist = FindList(sysuserid);
	            }
	            catch (Exception ex)
	            {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
	            }
                foreach (var item in contactuserlist)
                {

                    ContactUserJson contactUserJson = new ContactUserJson();
                    contactUserJson = JsonEntityUtility.SetJsonEntity(contactUserJson, item) as ContactUserJson;
                    response.item.Add(contactUserJson);
                }             
            return JsonHelper.ToJson(response);
       }
       public string Update(int sysuserid,int  linkId,string linkman, string linkTel, string linkEmail,int txtId,string linkmans, string linkTels, string linkEmails)
       {
           ResponseContactUserJson response = new ResponseContactUserJson();
           response.issucc = true;            
           response.count = 0;
           List<ContactUser> contactuser = new List<ContactUser>();
           try
           {
               contactuser = FindList(sysuserid);
           }
           catch (Exception ex)
           {
               response.issucc = false;
               response.errormsg = ex.Message;
               return JsonHelper.ToJson(response);
           }
           Validation.CommonValidator commonvalidator = new Validation.CommonValidator();
           if (linkman.Length==0 ||linkTel.Length==0||linkEmail.Length==0)
           {
               response.issucc = false;
               response.errormsg = "第一联系人资料不全,请填写";
               return JsonHelper.ToJson(response);
           }
           else
           {                
               if (!commonvalidator.ValidatePhone(linkTel))
               {
                 response.issucc = false;
               response.errormsg = "第一联系人电话号码不正确";
               return JsonHelper.ToJson(response);  
               }
               if (!commonvalidator.ValidateEmail(linkEmail))
               {
                   response.issucc = false;
                   response.errormsg = "第一联系人邮箱格式不正确";
                   return JsonHelper.ToJson(response);
               }

               ContactUser _model = new ContactUser();
               _model = (from p in contactuser
                         where p.SysUserId ==sysuserid
                         where p.IsPrimary == true
                         select p).FirstOrDefault();
               if (_model!=null )
               {
                   if (_model.ContactUserName != linkman || _model.ContactUserTel != linkTel || _model.ContactUserEmail != linkEmail)
                   {
                       _model.ContactUserName = linkman;
                       _model.ContactUserTel = linkTel;
                       _model.ContactUserEmail = linkEmail;
                       _model.SysUserId = sysuserid;
                       _model.IsPrimary = true;
                       try
                       {
                           DataRepository.ContactUserProvider.Update(_model);
                       }
                       catch (Exception ex)
                       {                        
                           response.errormsg = ex.Message;                          
                       }   
                   }
                                  
               }
               else
               {
                   ContactUser _modelContactUser = new ContactUser();
                   _modelContactUser.ContactUserName = linkman;
                   _modelContactUser.ContactUserTel = linkTel;
                   _modelContactUser.ContactUserEmail = linkEmail;
                   _modelContactUser.SysUserId = sysuserid;
                   _modelContactUser.IsPrimary = true; 
                   try
                   {
                       DataRepository.ContactUserProvider.Insert(_modelContactUser);
                   }
                   catch (Exception ex)
                   {                    
                       response.errormsg =ex.Message;                      
                   }
               }
           }

           if (linkmans.Length != 0 && linkTels.Length != 0 && linkEmails.Length != 0)
           {
               if (!commonvalidator.ValidatePhone(linkTels))
               {
                   response.issucc = false;
                   response.errormsg = "第二联系人电话号码不正确";
                   return JsonHelper.ToJson(response);
               }
               if (!commonvalidator.ValidateEmail(linkEmails))
               {
                   response.issucc = false;
                   response.errormsg = "第二联系人邮箱格式不正确";
                   return JsonHelper.ToJson(response);
               }    

               ContactUser _model = new ContactUser();
               _model = (from p in contactuser
                         where p.SysUserId == sysuserid
                         where p.IsPrimary == false
                         select p).FirstOrDefault();
               if (_model != null)
               {
                   if (_model.ContactUserName != linkmans || _model.ContactUserTel != linkTels || _model.ContactUserEmail != linkEmails)
                   {
                       _model.ContactUserName = linkmans;
                       _model.ContactUserTel = linkTels;
                       _model.ContactUserEmail = linkEmails;
                       _model.SysUserId = sysuserid;
                       _model.IsPrimary = false;
                       try
                       {
                           DataRepository.ContactUserProvider.Update(_model);
                       }
                       catch (Exception ex)
                       {                       
                           response.errormsg = response.errormsg.Length == 0 ? " 第二联系人更新失败！" + ex.Message : response.errormsg + "  第二联系人更新失败！" + ex.Message;
                       }
                   }
               }
               else
               {
                   ContactUser _modelContactUser = new ContactUser();
                   _modelContactUser.ContactUserName = linkmans;
                   _modelContactUser.ContactUserTel = linkTels;
                   _modelContactUser.ContactUserEmail = linkEmails;
                   _modelContactUser.SysUserId = sysuserid;
                   _modelContactUser.IsPrimary = false;
                   try
                   {
                       DataRepository.ContactUserProvider.Insert(_modelContactUser);
                   }
                   catch (Exception ex)
                   {
                      // response.issucc = false;
                       response.errormsg = response.errormsg.Length == 0 ? " 第二联系人添加失败！" + ex.Message : response.errormsg + "  第二联系人添加失败！" + ex.Message;
                      // return JsonHelper.ToJson(response);
                   }
               }
           }
 
           
          
           return JsonHelper.ToJson(response);       
       }
       public List<ContactUser> FindList(int sysuserid)
       {
           ContactUserParameterBuilder builder = new ContactUserParameterBuilder();
           builder.Clear();
           builder.Junction = SqlUtil.AND;
           builder.Append(ContactUserColumn.SysUserId, sysuserid.ToString());
           TList<ContactUser> tlist = new TList<ContactUser>();
           try
           {
               tlist = DataRepository.ContactUserProvider.Find(builder.GetParameters());
               tlist.Sort("IsPrimary desc");
               return tlist.ToList() ;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
         
       }
    }
}
