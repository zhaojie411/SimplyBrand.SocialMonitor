using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;

namespace SimplyBrand.SocialMonitor.Business
{
    public class PlatformBLL
    {
        public string GetPlatform()
        {
            ResponsePlatformListJson response = new ResponsePlatformListJson();
            response.issucc = true;
            response.data = new List<PlatformJson>();
            try
            {
                TList<Platform> tlist = DataRepository.PlatformProvider.GetAll();
                foreach (Platform item in tlist)
                {
                    PlatformJson platformJson = new PlatformJson();
                    platformJson = JsonEntityUtility.SetJsonEntity(platformJson, item) as PlatformJson;
                    response.data.Add(platformJson);
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
        public string Find(int sysuserid)
        {
            ResponsePlatformListJson response = new ResponsePlatformListJson();
            response.issucc = true;
            response.data = new List<PlatformJson>();
            try
            {
                TList<SysUserPlatform> sysuseplatlist = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysuserid);
                List<short> arrids = (from p in sysuseplatlist
                                 select p.PlatformId).ToList();
                StringBuilder sb = new StringBuilder();
                foreach (short item in arrids)
                {
                    sb.Append(item);
                    sb.Append(",");
                    
                }

                PlatformParameterBuilder builder = new PlatformParameterBuilder();
                builder.AppendIn(PlatformColumn.PlatformId, sb.ToString().Split(','));
                TList<Platform> tlist = DataRepository.PlatformProvider.Find(builder.GetParameters());

                foreach (Platform item in tlist)
                {
                    PlatformJson platformJson = new PlatformJson();
                    platformJson = JsonEntityUtility.SetJsonEntity(platformJson, item) as PlatformJson;
                    response.data.Add(platformJson);
                }
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
               return  JsonHelper.ToJson(response); 
            }


            return JsonHelper.ToJson(response); ;
        }        
    }
}
