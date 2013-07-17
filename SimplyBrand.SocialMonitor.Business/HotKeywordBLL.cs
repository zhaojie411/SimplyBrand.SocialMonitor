using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
namespace SimplyBrand.SocialMonitor.Business
{
    public class HotKeywordBLL
    {

        public string GetHotKeyword(int sysUserId, string keywordFamilyIDs)
        {
            ResponseHotKeywordPageJson response = new ResponseHotKeywordPageJson();
            try
            {
                TList<KeywordFamily> tlistKeywordFamily = DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId);
                if (string.IsNullOrEmpty(keywordFamilyIDs))
                {
                    keywordFamilyIDs = string.Join(",", tlistKeywordFamily.Select(p => p.KeywordFamilyId).ToList());
                }
                else
                {
                    keywordFamilyIDs = string.Join(",", (from t in tlistKeywordFamily
                                                         where keywordFamilyIDs.Contains(t.KeywordFamilyId.ToString())
                                                         select t.KeywordFamilyId).ToList());

                }
                TList<HotKeyword> tlist = Find(keywordFamilyIDs.Split(','));
                response.issucc = true;
                response.data = new HotKeywordPageJson();
                response.data.count = tlist.Count;
                response.data.items = new List<HotKeywordJson>();
                foreach (HotKeyword item in tlist)
                {
                    HotKeywordJson jsonEntity = new HotKeywordJson();
                    jsonEntity = JsonEntityUtility.SetJsonEntity(jsonEntity, item) as HotKeywordJson;
                    response.data.items.Add(jsonEntity);
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

        public TList<HotKeyword> Find(string[] keywordFamilyIDs)
        {
            HotKeywordParameterBuilder builder = new HotKeywordParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.AppendIn(HotKeywordColumn.KeywordFamilyId, keywordFamilyIDs);
            return DataRepository.HotKeywordProvider.Find(builder.GetParameters());
        }
    }
}