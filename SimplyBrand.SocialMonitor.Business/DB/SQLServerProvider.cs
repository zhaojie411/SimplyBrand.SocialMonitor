using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;

namespace SimplyBrand.SocialMonitor.Business.DB
{
    public class SQLServerProvider : IDBProvider
    {
        public string GetSeach(string sourceids, string familyids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex,int sysuserid)
        {
            ResponseDataCenterJson response = new ResponseDataCenterJson();
            response.issucc = true;
          
            response.data = new DataCenterPageJson();
            try
            {
                DataCenterParameterBuilder builder = new DataCenterParameterBuilder();
                builder.Clear();
                builder.Junction = string.Empty;
                int count = 0;
                if (!string.IsNullOrEmpty(sourceids))               
                    builder.AppendIn(DataCenterColumn.Datasourceid, sourceids.Split(','));
           
              

                TList<KeywordFamily> keywordfamily = new TList<KeywordFamily>();
                KeywordFamilyParameterBuilder familybulider = new KeywordFamilyParameterBuilder();
                if (!string.IsNullOrEmpty(familyids))
                {
                    familybulider.AppendIn(KeywordFamilyColumn.KeywordFamilyId, familyids.Split(','));  
                }
                else
                {
                    familybulider.Append(KeywordFamilyColumn.SysUserId, sysuserid.ToString()); 
                }          
                keywordfamily = DataRepository.KeywordFamilyProvider.Find(familybulider.GetParameters()); 

                List<int> familyidlist = (from p in keywordfamily select p.KeywordFamilyId).ToList();
                StringBuilder sb = new StringBuilder();
                foreach (int item in familyidlist)
                {
                    sb.Append(item);
                    sb.Append(",");
                }
                TList<UserKeyword> Tuserkeyword = new TList<UserKeyword>();
                Tuserkeyword.Clear();
                UserKeywordParameterBuilder userkeywordbuilder = new UserKeywordParameterBuilder();
                userkeywordbuilder.AppendIn(UserKeywordColumn.KeywordFamilyId, sb.ToString().Split(','));
                Tuserkeyword = DataRepository.UserKeywordProvider.Find(userkeywordbuilder.GetParameters());



                List<UserKeyword> keyword = (from p in Tuserkeyword
                                             where p.IsForbid == true
                                             select p).ToList();
                //品牌名称对应的关键词名称列表   用于标红 统计关键词列表；
                List<keywordModel> _keywordmodel = new List<keywordModel>();
                foreach (var item in keywordfamily)
                {      
                     List<UserKeyword> _list=new List<UserKeyword>();
                       _list.Clear();
                          _list= (from p in keyword
                                  where p.KeywordFamilyId == item.KeywordFamilyId
                                             select p).ToList();
                          foreach (var list in _list)
                          {
                                    keywordModel keymodel=new keywordModel();
                              keymodel.keyFamilyName=item.KeywordFamilyName;
                              keymodel.keyValue=list.KeywordContent;
                              _keywordmodel.Add(keymodel);
                          }
                }

                  //like
                if (keyword.Count > 0)
                {
                    builder.BeginGroup(SqlUtil.AND);
                    for (int i = 0; i < keyword.Count; i++)
                    {
                        if (i == 0)
                            builder.AppendLike(DataCenterColumn.Datatitle, "%" + keyword[i].KeywordContent + "%");
                        else
                            builder.AppendLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + keyword[i].KeywordContent + "%");
                    }                     
                    builder.EndGroup();
                }  
                
                if (!string.IsNullOrEmpty(keyvalue))
                {
                    builder.AppendLike(SqlUtil.AND,DataCenterColumn.Datatitle, string.Format("%{0}%", keyvalue.Trim()));
                }
               
                List<UserKeyword> notkeyword = (from p in Tuserkeyword
                                                where p.IsForbid == false
                                                select p).ToList();

                if (notkeyword.Count > 0)
                {
                    builder.BeginGroup(SqlUtil.AND);
                    for (int i = 0; i < notkeyword.Count; i++)
                    {
                        if (i == 0)
                            builder.AppendNotLike(DataCenterColumn.Datatitle, "%" + notkeyword[i].KeywordContent + "%");
                        else
                            builder.AppendNotLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + notkeyword[i].KeywordContent + "%");
                    }
                    builder.EndGroup();
                }  
              
                if (!string.IsNullOrEmpty(notkeyvalue))
                {
                    builder.AppendNotLike(SqlUtil.AND,DataCenterColumn.Datatitle, string.Format("%{0}%", notkeyvalue.Trim()));
                }                
                if (!string.IsNullOrEmpty(emotional))
                {
                    builder.Append(DataCenterColumn.Emotionalvalue, emotional);
                }
                builder.AppendRange(DataCenterColumn.Datatime, starttime, endtime);
                DataCenterSortBuilder sortb = new DataCenterSortBuilder();
                sortb.AppendDESC(DataCenterColumn.Dataid);
                TList<DataCenter> tdatalist = DataRepository.DataCenterProvider.Find(builder.GetParameters(), sortb, (pageindex - 1) * pageSize, pageSize, out count);
                response.data.items = new List<DataCenterJson>();
                foreach (DataCenter item in tdatalist)
                {
                    DataCenterJson _model = new DataCenterJson();
                    _model = JsonEntity.JsonEntityUtility.SetJsonEntity(_model, item) as DataCenterJson;
                    bool IsHit = false;
                    foreach (keywordModel keywordsList in _keywordmodel)
                    {
                        if (_model.datatitle.Contains(keywordsList.keyValue))
	                    {
                            _model.datatitle = _model.datatitle.Replace(keywordsList.keyValue, "<font style=\"color:red;\">"+keywordsList.keyValue+"</font>");

                            if (_model.databody.Contains(keywordsList.keyValue))
                            {
                                _model.databody = _model.databody.Replace(keywordsList.keyValue, "<font style=\"color:red;\">" + keywordsList.keyValue + "</font>");
                            }
                            if (!IsHit)
                            {
                                _model.dataKey = keywordsList.keyFamilyName;
                                IsHit = true;
                                
                            }
	                    }
                        else
                        {
                            if (_model.databody.Contains(keywordsList.keyValue))
                            {
                                _model.databody = _model.databody.Replace(keywordsList.keyValue, "<font style=\"color:red;\">" + keywordsList.keyValue + "</font>");
                                if (!IsHit)
                                {
                                    _model.dataKey = keywordsList.keyFamilyName;
                                    IsHit = true;                                 
                                }
                            }
                        }
                     
                    }  
                    response.data.items.Add(_model);
                }
                response.data.count = count;
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message;
                return JsonHelper.ToJson(response);
            }


            return JsonHelper.ToJson(response);
        }



        public string GetSummaryData(int sysUserId, string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday)
        {
            DataCenterBLL bll = new DataCenterBLL();
            return bll.GetSummaryData(sysUserId, keywordFamilyIDs, platforms, emotionvalues, isToday);
        }

        public string GetEmotionalData(int sysUserId, string keywordFamilyIDs, string platforms, bool isToday)
        {
            DataCenterBLL bll = new DataCenterBLL();
            return bll.GetEmotionalData(sysUserId, keywordFamilyIDs, platforms, isToday);
        }
    }
    public class keywordModel
        {
        public   string keyFamilyName { get; set; }
        public string keyValue { get; set; }

        }
}
