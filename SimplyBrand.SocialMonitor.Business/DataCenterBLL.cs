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
using System.Data.SqlTypes;
namespace SimplyBrand.SocialMonitor.Business
{

    public class DataCenterBLL
    {
        /// <summary>
        /// 获取折线图的数据
        /// </summary>
        /// <param name="keywordFamilyIDs"></param>
        /// <param name="platforms"></param>
        /// <param name="emotionvalues"></param>
        /// <param name="isToday"></param>
        /// <returns></returns>
        public string GetSummaryData(int sysUserId, string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday)
        {
            ResponseDataCenterSummaryJson response = new ResponseDataCenterSummaryJson();
            response.issucc = true;
            try
            {

                TList<KeywordFamily> tlist = DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId);
                if (string.IsNullOrEmpty(keywordFamilyIDs))
                {
                    keywordFamilyIDs = string.Join(",", tlist.Select(p => p.KeywordFamilyId).ToList());
                }
                if (string.IsNullOrEmpty(platforms))
                {
                    platforms = (int)EnumPlatform.Weibo + "," + (int)EnumPlatform.News + "," + (int)EnumPlatform.Blog + "," + (int)EnumPlatform.BBS;
                }
                if (string.IsNullOrEmpty(emotionvalues))
                {
                    emotionvalues = (int)EnumEmotionalValue.Positive + "," + (int)EnumEmotionalValue.Negative + "," + (int)EnumEmotionalValue.Neutral;
                }
                DateTime starttime = DateTime.Now;
                DateTime endtime = DateTime.Now;
                if (isToday)
                {
                    starttime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    endtime = DateTime.Now.AddDays(1);
                }
                else
                {
                    starttime = DateTime.Parse(SqlDateTime.MinValue.ToString());
                    endtime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                }
                //可以加校验 
                Dictionary<string, List<DataCenterSummaryItemJson>> dic = GetSummaryData(keywordFamilyIDs, platforms, emotionvalues, isToday, starttime.ToString(), endtime.ToString());

                response.data = new DataCenterSummaryJson();
                response.data.items = dic;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
            }
            return JsonHelper.ToJson(response);
        }

        public string GetEmotionalData(int sysUserId, string keywordFamilyIDs, string platforms, bool isToday)
        {
            ResponseDataEmotionalJson response = new ResponseDataEmotionalJson();
            response.issucc = true;
            try
            {
                TList<KeywordFamily> tlist = DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId);
                if (string.IsNullOrEmpty(keywordFamilyIDs))
                {
                    keywordFamilyIDs = string.Join(",", tlist.Select(p => p.KeywordFamilyId).ToList());
                }
                if (string.IsNullOrEmpty(platforms))
                {
                    platforms = (int)EnumPlatform.Weibo + "," + (int)EnumPlatform.News + "," + (int)EnumPlatform.Blog + "," + (int)EnumPlatform.BBS;
                }
                DateTime starttime = DateTime.Now;
                DateTime endtime = DateTime.Now;
                if (isToday)
                {
                    starttime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    endtime = DateTime.Now.AddDays(1);
                }
                else
                {
                    starttime = DateTime.Parse(SqlDateTime.MinValue.ToString());
                    endtime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                }
                List<DataCenterSummaryItemJson> list = GetEmotionalData(keywordFamilyIDs, platforms, starttime.ToString(), endtime.ToString());
                response.data = list;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
            }
            return JsonHelper.ToJson(response);
        }


        private string getEmtionalValue(int type)
        {
            string value = "";
            switch (type)
            {
                case (int)EnumEmotionalValue.Positive:
                    value = "正面";
                    break;

                case (int)EnumEmotionalValue.Negative:
                    value = "负面";
                    break;
                case (int)EnumEmotionalValue.Neutral:
                    value = "中性";
                    break;

            }
            return value;
        }

        public List<DataCenterSummaryItemJson> GetEmotionalData(string keywordFamilyIDs, string platforms, string starttime, string endtime)
        {
            //TList<DataCenter> tlist = FindDataCenter(keywordFamilyIDs, platforms, isToday, emotionvalues);
            VList<ViewDataCenter> vlist = FindViewDataCenter(keywordFamilyIDs, platforms, starttime, endtime, string.Empty);
            var query = from t in vlist
                        group t by (int)t.Emotionalvalue
                            into g
                            select new
                            {
                                g.Key,
                                Count = g.Count()
                            };
            List<DataCenterSummaryItemJson> list = new List<DataCenterSummaryItemJson>();
            foreach (var item in query)
            {
                list.Add(new DataCenterSummaryItemJson() { key = item.Key, value = item.Count, title = getEmtionalValue(item.Key) });
            }


            return list.OrderByDescending(p => p.key).ToList();
        }




        public Dictionary<string, List<DataCenterSummaryItemJson>> GetSummaryData(string keywordFamilyIDs, string platforms, string emotionvalues, bool isToday, string starttime, string endtime)
        {
            //TList<DataCenter> tlist = FindDataCenter(keywordFamilyIDs, platforms, isToday, emotionvalues);
            VList<ViewDataCenter> vlist = FindViewDataCenter(keywordFamilyIDs, platforms, starttime, endtime, emotionvalues);

            if (isToday)
            {
                var query = from t in vlist
                            group t by new
                                {

                                    Hour = ((DateTime)t.Datatime).Hour,
                                    Name = t.KeywordFamilyName  //Sitename
                                }
                                into g
                                select new
                                {
                                    g.Key,
                                    Count = g.Count()
                                };



                Dictionary<string, List<DataCenterSummaryItemJson>> dic = new Dictionary<string, List<DataCenterSummaryItemJson>>();
                Dictionary<string, List<DataCenterSummaryItemJson>> dicNew = new Dictionary<string, List<DataCenterSummaryItemJson>>();
                List<long> listhours = new List<long>();
                for (int i = 0; i < 24; i++)
                {
                    listhours.Add(i);
                }
                foreach (var item in query)
                {
                    if (!dic.ContainsKey(item.Key.Name))
                    {
                        dic.Add(item.Key.Name, new List<DataCenterSummaryItemJson>());
                    }
                    dic[item.Key.Name].Add(new DataCenterSummaryItemJson() { key = item.Key.Hour, value = item.Count });
                }
                foreach (string key in dic.Keys)
                {
                    List<DataCenterSummaryItemJson> listitem = dic[key];
                    listhours = listhours.Except(listitem.Select(p => p.key).ToList()).ToList();
                    foreach (long longkey in listhours)
                    {
                        dic[key].Add(new DataCenterSummaryItemJson() { key = longkey, value = 0 });
                    }
                }
                foreach (string item in dic.Keys)
                {
                    dicNew.Add(item, dic[item].OrderBy(p => p.key).ToList());
                }
                return dicNew;
            }
            else
            {
                var query = from t in vlist
                            group t by new
                            {

                                Date = ((DateTime)t.Datatime).ToString("yyyy-MM-dd"),
                                Name = t.KeywordFamilyName
                            }
                                into g
                                select new
                                {
                                    g.Key,
                                    Count = g.Count()
                                };

                Dictionary<string, List<DataCenterSummaryItemJson>> dic = new Dictionary<string, List<DataCenterSummaryItemJson>>();
                Dictionary<string, List<DataCenterSummaryItemJson>> dicNew = new Dictionary<string, List<DataCenterSummaryItemJson>>();
                foreach (var item in query)
                {
                    if (!dic.ContainsKey(item.Key.Name))
                    {
                        dic.Add(item.Key.Name, new List<DataCenterSummaryItemJson>());
                    }
                    dic[item.Key.Name].Add(new DataCenterSummaryItemJson() { key = Decimal.ToInt64(Decimal.Divide(DateTime.Parse(item.Key.Date).Ticks - 621355968000000000, 10000)), value = item.Count, title = DateTime.Parse(item.Key.Date).ToString("yyyy-MM-dd") });

                }
                var nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                foreach (string key in dic.Keys)
                {
                    List<DateTime> dtDate = dic[key].Select(p => p.title).ToList().ConvertAll(p => DateTime.Parse(p)).ToList();
                    for (int i = 1; i <= 30; i++)
                    {
                        if (!dtDate.Contains(nowDate.AddDays(-i)))
                        {
                            dic[key].Add(new DataCenterSummaryItemJson() { key = Decimal.ToInt64(Decimal.Divide(nowDate.AddDays(-i).Ticks - 621355968000000000, 10000)), value = 0, title = nowDate.AddDays(-i).ToString("yyyy-MM-dd") });
                        }
                    }
                }


                foreach (string item in dic.Keys)
                {
                    dicNew.Add(item, dic[item].OrderBy(p => p.key).ToList());
                }
                return dicNew;

            }
        }


        public VList<ViewDataCenter> FindViewDataCenter(string keywordFamilyIDs, string platforms, string starttime, string endtime, string emotionvalues)
        {
            if (string.IsNullOrEmpty(keywordFamilyIDs))
            {
                return new VList<ViewDataCenter>();
            }
            ViewDataCenterParameterBuilder builder = new ViewDataCenterParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.AppendIn(ViewDataCenterColumn.KeywordFamilyId, keywordFamilyIDs.Split(','));
            builder.AppendIn(ViewDataCenterColumn.Datasourceid, platforms.Split(','));
            if (!string.IsNullOrEmpty(starttime))
            {
                builder.AppendGreaterThanOrEqual(ViewDataCenterColumn.Datatime, starttime);
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                builder.AppendLessThanOrEqual(ViewDataCenterColumn.Datatime, endtime);
            }
            if (!string.IsNullOrEmpty(emotionvalues))
            {
                builder.AppendIn(ViewDataCenterColumn.Emotionalvalue, emotionvalues.Split(','));
            }
            return DataRepository.ViewDataCenterProvider.Find(builder.GetParameters());
        }

        public VList<ViewDataCenter> FindViewDataCenter(string keywordFamilyIDs, string platforms, bool isToday, string emotionvalues = null)
        {
            if (string.IsNullOrEmpty(keywordFamilyIDs))
            {
                return new VList<ViewDataCenter>();
            }
            ViewDataCenterParameterBuilder builder = new ViewDataCenterParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.AppendIn(ViewDataCenterColumn.KeywordFamilyId, keywordFamilyIDs.Split(','));
            builder.AppendIn(ViewDataCenterColumn.Datasourceid, platforms.Split(','));
            if (isToday)
            {
                builder.AppendGreaterThanOrEqual(ViewDataCenterColumn.Datatime, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                builder.AppendLessThan(ViewDataCenterColumn.Datatime, DateTime.Now.ToString("yyyy-MM-dd"));
            }

            if (!string.IsNullOrEmpty(emotionvalues))
            {
                builder.AppendIn(ViewDataCenterColumn.Emotionalvalue, emotionvalues.Split(','));
            }
            return DataRepository.ViewDataCenterProvider.Find(builder.GetParameters());

        }

        public TList<DataCenter> FindDataCenter(string keywordFamilyIDs, string platforms, bool isToday, string emotionvalues = null)
        {
            if (string.IsNullOrEmpty(keywordFamilyIDs))
            {
                return new TList<DataCenter>();
            }
            TList<UserKeyword> userKeywordTList = FindKeywordFamily(keywordFamilyIDs.Split(','));
            List<UserKeyword> likeList = userKeywordTList.Where(p => p.IsForbid == true).ToList();
            List<UserKeyword> notLikeList = userKeywordTList.Where(p => p.IsForbid == false).ToList();

            DataCenterParameterBuilder builder = new DataCenterParameterBuilder();
            builder.Junction = string.Empty; ;
            if (isToday)
            {
                builder.AppendGreaterThanOrEqual(SqlUtil.AND, DataCenterColumn.Datatime, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                builder.AppendLessThan(SqlUtil.AND, DataCenterColumn.Datatime, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            builder.AppendIn(SqlUtil.AND, DataCenterColumn.Datasourceid, platforms.Split(','));

            if (!string.IsNullOrEmpty(emotionvalues))
            {
                builder.AppendIn(SqlUtil.AND, DataCenterColumn.Emotionalvalue, emotionvalues.Split(','));
            }

            //like
            if (likeList.Count > 0)
            {
                builder.BeginGroup(SqlUtil.AND);

                for (int i = 0; i < likeList.Count; i++)
                {
                    if (i == 0)
                        builder.AppendLike(DataCenterColumn.Datatitle, "%" + likeList[i].KeywordContent + "%");
                    else
                        builder.AppendLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + likeList[i].KeywordContent + "%");
                }
                builder.EndGroup();
            }
            //not like
            if (notLikeList.Count > 0)
            {
                builder.BeginGroup(SqlUtil.AND);
                for (int i = 0; i < notLikeList.Count; i++)
                {
                    if (i == 0)
                        builder.AppendNotLike(DataCenterColumn.Datatitle, "%" + notLikeList[i].KeywordContent + "%");
                    else
                        builder.AppendNotLike(SqlUtil.OR, DataCenterColumn.Datatitle, "%" + notLikeList[i].KeywordContent + "%");
                }
                builder.EndGroup();
            }
            return DataRepository.DataCenterProvider.Find(builder.GetParameters());
        }

        public TList<UserKeyword> FindKeywordFamily(string[] keywordFamilyIDs)
        {
            UserKeywordParameterBuilder builder = new UserKeywordParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.AppendIn(UserKeywordColumn.KeywordFamilyId, keywordFamilyIDs);
            return DataRepository.UserKeywordProvider.Find(builder.GetParameters());
        }
    }
}
