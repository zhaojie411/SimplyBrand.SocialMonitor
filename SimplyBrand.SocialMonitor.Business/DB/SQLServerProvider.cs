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
        public string GetSeach(string sourceids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex)
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
                if (!string.IsNullOrEmpty(keyvalue))
                    builder.AppendLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", keyvalue.Trim()));

                if (!string.IsNullOrEmpty(notkeyvalue))
                    builder.AppendNotLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", notkeyvalue.Trim()));
                if (!string.IsNullOrEmpty(emotional))
                    builder.AppendIn(SqlUtil.AND, DataCenterColumn.Emotionalvalue, emotional.Split(','));

                builder.AppendRange(DataCenterColumn.Datatime, starttime, endtime);
                DataCenterSortBuilder sortb = new DataCenterSortBuilder();
                sortb.AppendDESC(DataCenterColumn.Dataid);
                TList<DataCenter> tdatalist = DataRepository.DataCenterProvider.Find(builder.GetParameters(), sortb, (pageindex - 1) * pageSize, pageSize, out count);
                response.data.count = count;

                List<UserKeyword> keyword = new List<UserKeyword>();
                List<UserKeyword> notkeyword = new List<UserKeyword>();

                DataCenterParameterBuilder Weibobuilder = new DataCenterParameterBuilder();
                Weibobuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "1");
                response.Weibocount = DataRepository.DataCenterProvider.Find(Weibobuilder.GetParameters()).Count;
                DataCenterParameterBuilder Forumbuilder = new DataCenterParameterBuilder();
                Forumbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "2");
                response.Forumcount = DataRepository.DataCenterProvider.Find(Forumbuilder.GetParameters()).Count;
                DataCenterParameterBuilder Blogbuilder = new DataCenterParameterBuilder();

                Blogbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "3");
                response.Blogcount = DataRepository.DataCenterProvider.Find(Blogbuilder.GetParameters()).Count;
                DataCenterParameterBuilder Newsbuilder = new DataCenterParameterBuilder();
                Newsbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "4");
                response.Newscount = DataRepository.DataCenterProvider.Find(Newsbuilder.GetParameters()).Count;


                response.data.items = new List<DataCenterJson>();
                foreach (DataCenter item in tdatalist)
                {
                    DataCenterJson _model = new DataCenterJson();
                    _model = JsonEntity.JsonEntityUtility.SetJsonEntity(_model, item) as DataCenterJson;
                    response.data.items.Add(_model);

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
        public string GetSeach(string sourceids, string familyids, string keyvalue, string notkeyvalue, string starttime, string endtime, string emotional, int pageSize, int pageindex, int sysuserid)
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
                //foreach (int item in familyidlist)
                //{
                //    sb.Append(item);
                //    sb.Append(",");
                //}
                sb.Append(string.Join(",", familyidlist.ConvertAll(p => p.ToString()).ToList()));
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
                    List<UserKeyword> _list = new List<UserKeyword>();
                    _list.Clear();
                    _list = (from p in keyword
                             where p.KeywordFamilyId == item.KeywordFamilyId
                             select p).ToList();
                    foreach (var list in _list)
                    {
                        keywordModel keymodel = new keywordModel();
                        keymodel.keyFamilyName = item.KeywordFamilyName;
                        keymodel.keyValue = list.KeywordContent;
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
                    builder.AppendLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", keyvalue.Trim()));
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
                    builder.AppendNotLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", notkeyvalue.Trim()));
                }
                if (!string.IsNullOrEmpty(emotional))
                {
                    builder.AppendIn(SqlUtil.AND, DataCenterColumn.Emotionalvalue, emotional.Split(','));
                }
                builder.AppendRange(SqlUtil.AND, DataCenterColumn.Datatime, starttime, endtime);
                DataCenterSortBuilder sortb = new DataCenterSortBuilder();
                sortb.AppendDESC(DataCenterColumn.Dataid);
                TList<DataCenter> tdatalist = DataRepository.DataCenterProvider.Find(builder.GetParameters(), sortb, (pageindex - 1) * pageSize, pageSize, out count);

                DataCenterParameterBuilder Weibobuilder = new DataCenterParameterBuilder();
                Weibobuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "1");
                response.Weibocount = DataRepository.DataCenterProvider.Find(Weibobuilder.GetParameters()).Count;
                DataCenterParameterBuilder Forumbuilder = new DataCenterParameterBuilder();
                Forumbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "2");
                response.Forumcount = DataRepository.DataCenterProvider.Find(Forumbuilder.GetParameters()).Count;
                DataCenterParameterBuilder Blogbuilder = new DataCenterParameterBuilder();

                Blogbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "3");
                response.Blogcount = DataRepository.DataCenterProvider.Find(Blogbuilder.GetParameters()).Count;
                DataCenterParameterBuilder Newsbuilder = new DataCenterParameterBuilder();
                Newsbuilder = Createbuider(keyword, notkeyword, keyvalue, notkeyvalue, starttime, endtime, emotional, "4");
                response.Newscount = DataRepository.DataCenterProvider.Find(Newsbuilder.GetParameters()).Count;



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
                            // _model.datatitle = _model.datatitle.Replace(keywordsList.keyValue, "<font style=\"color:red;\">"+keywordsList.keyValue+"</font>");

                            if (_model.databody.Contains(keywordsList.keyValue))
                            {
                                // _model.databody = _model.databody.Replace(keywordsList.keyValue, "<font style=\"color:red;\">" + keywordsList.keyValue + "</font>");
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
                                // _model.databody = _model.databody.Replace(keywordsList.keyValue, "<font style=\"color:red;\">" + keywordsList.keyValue + "</font>");
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
        public DataCenterParameterBuilder Createbuider(List<UserKeyword> keywordlist, List<UserKeyword> notkeywordlist, string keyvalue, string notkeyvalue, string startime, string endtime, string emotional, string datasourceid)
        {
            DataCenterParameterBuilder builder = new DataCenterParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty; ;
            builder.Append(DataCenterColumn.Datasourceid, datasourceid);
            //like
            if (keywordlist.Count > 0)
            {
                builder.BeginGroup(SqlUtil.AND);
                for (int i = 0; i < keywordlist.Count; i++)
                {
                    if (i == 0)
                        builder.AppendLike(DataCenterColumn.Datatitle, "%" + keywordlist[i].KeywordContent + "%");
                    else
                        builder.AppendLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + keywordlist[i].KeywordContent + "%");
                }
                builder.EndGroup();
            }

            if (!string.IsNullOrEmpty(keyvalue))
            {
                builder.AppendLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", keyvalue.Trim()));
            }

            if (notkeywordlist.Count > 0)
            {
                builder.BeginGroup(SqlUtil.AND);
                for (int i = 0; i < notkeywordlist.Count; i++)
                {
                    if (i == 0)
                        builder.AppendNotLike(DataCenterColumn.Datatitle, "%" + notkeywordlist[i].KeywordContent + "%");
                    else
                        builder.AppendNotLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + notkeywordlist[i].KeywordContent + "%");
                }
                builder.EndGroup();
            }

            if (!string.IsNullOrEmpty(notkeyvalue))
            {
                builder.AppendNotLike(SqlUtil.AND, DataCenterColumn.Datatitle, string.Format("%{0}%", notkeyvalue.Trim()));
            }
            if (!string.IsNullOrEmpty(emotional))
            {
                builder.AppendIn(SqlUtil.AND, DataCenterColumn.Emotionalvalue, emotional.Split(','));
            }
            builder.AppendRange(SqlUtil.AND, DataCenterColumn.Datatime, startime, endtime);

            return builder;

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

        public string UpdateEmotional(long dataid, string emotionalvalue)
        {
            ResponseDataCenterJson response = new ResponseDataCenterJson();
            response.issucc = true;
            response.data = new DataCenterPageJson();
            DataCenter _model = new DataCenter();
            _model = DataRepository.DataCenterProvider.GetByDataid(dataid);
            if (_model != null)
            {
                try
                {
                    _model.Emotionalvalue = Convert.ToInt16(emotionalvalue);
                    DataRepository.DataCenterProvider.Update(_model);
                }
                catch (Exception ex)
                {
                    response.issucc = false;
                    response.errormsg = ex.Message;
                    return JsonHelper.ToJson(response);
                }
            }
            else
            {
                response.issucc = false;
                response.errormsg = "没有找该条数据";
                return JsonHelper.ToJson(response);
            }
            return JsonHelper.ToJson(response);
        }

        public string Delete(long dataid)
        {
            ResponseDataCenterJson response = new ResponseDataCenterJson();
            response.issucc = true;
            response.data = new DataCenterPageJson();
            try
            {

                DataRepository.DataCenterProvider.Delete(dataid);
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
    public class keywordModel
    {
        public string keyFamilyName { get; set; }
        public string keyValue { get; set; }

    }
}
