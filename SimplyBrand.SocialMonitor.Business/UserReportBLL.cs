using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.Validation;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.Business.Report;
namespace SimplyBrand.SocialMonitor.Business
{
    public class UserReportBLL
    {

        /// <summary>
        /// 前台用户下载报表
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="reportType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetUserReport(int sysUserId, int reportType, string date)
        {
            ResponseUserReportJson response = new ResponseUserReportJson();

            try
            {
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                DateTime currentTime = DateTime.MinValue;
                if (reportType == (int)EnumReportType.DayReport)
                {
                    DateTime.TryParse(date, out currentTime);
                    if (currentTime == DateTime.MinValue)
                    {
                        response.issucc = false;
                        response.errormsg = ConstHelper.PARAMETER_ERROR;
                        return JsonHelper.ToJson(response);
                    }
                    startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day);
                    endTime = startTime.AddDays(1);
                }
                else if (reportType == (int)EnumReportType.WeekDayReport)
                {
                    string[] strtime = date.Split('~');
                    startTime = DateTime.Parse(strtime[0].Trim());
                    endTime = DateTime.Parse(strtime[1]).AddDays(1);
                }
                else if (reportType == (int)EnumReportType.MonthReport)
                {
                    string[] strtime = date.Split('-');
                    startTime = DateTimeHelper.GetFirstDayOfMonth(int.Parse(strtime[0]), int.Parse(strtime[1]));
                    endTime = DateTimeHelper.GetLastDayOfMonth(int.Parse(strtime[0]), int.Parse(strtime[1])).AddDays(1);
                }

                UserReport entity = Find(sysUserId, reportType, startTime, endTime, (int)EnumReportStatus.ThroughAudit);
                if (entity != null)
                {
                    response.issucc = true;
                    response.data = new UserReportJson();
                    response.data = JsonEntityUtility.SetJsonEntity(response.data, entity) as UserReportJson;
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

        /// <summary>
        /// 更新报表的审核状态
        /// </summary>
        /// <param name="userReportId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateReportStatus(int userReportId, int reportStatus)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                if (reportStatus != (int)EnumReportStatus.ThroughAudit && reportStatus != (int)EnumReportStatus.UnAudited && reportStatus != (int)EnumReportStatus.UnauditedThrough)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.PARAMETER_ERROR;
                    return JsonHelper.ToJson(response);
                }
                UserReport entity = DataRepository.UserReportProvider.GetByUserReportId(userReportId);
                entity.ReportStatus = reportStatus;
                if (DataRepository.UserReportProvider.Update(entity))
                {
                    response.issucc = true;
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

        /// <summary>
        /// 后台查询用户报表
        /// </summary>
        /// <param name="sysAdminId"></param>
        /// <param name="reportType"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public string GetUserReport(int sysAdminId, int reportType, int pageindex, int pagesize)
        {
            ResponseUserReportPageJson response = new ResponseUserReportPageJson();
            response.issucc = true;
            try
            {
                response.data = new UserReportPageJson();
                int count = 0;
                TList<UserReport> tlist = Find(sysAdminId, reportType, pageindex, pagesize, out count);
                response.data.count = count;
                response.data.items = new List<UserReportJson>();
                foreach (UserReport item in tlist)
                {
                    UserReportJson jsonEntity = new UserReportJson();
                    jsonEntity = JsonEntityUtility.SetJsonEntity(jsonEntity, item) as UserReportJson;
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


        private TList<UserReport> Find(int sysAdminId, int reportType, int pageindex, int pagesize, out int count)
        {
            UserReportParameterBuilder builder = new UserReportParameterBuilder();
            builder.Clear();
            builder.Junction = string.Empty;
            builder.Append(UserReportColumn.ReportType, reportType.ToString());
            UserReportSortBuilder sort = new UserReportSortBuilder();
            sort.AppendDESC(UserReportColumn.UserReportId);
            return DataRepository.UserReportProvider.Find(builder.GetParameters(), sort, (pageindex - 1) * pagesize, pagesize, out count);
        }


        private UserReport Find(int sysUserId, int reportType, DateTime startTime, DateTime endTime, int reportStatus)
        {
            UserReportParameterBuilder builder = new UserReportParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.Append(UserReportColumn.SysUserId, sysUserId.ToString());
            builder.Append(UserReportColumn.ReportType, reportType.ToString());
            builder.Append(UserReportColumn.ReportStatus, reportStatus.ToString());
            builder.Append(UserReportColumn.IsSysGen, true.ToString());//这里查询系统生成的
            builder.AppendGreaterThanOrEqual(UserReportColumn.StartTime, startTime.ToString("yyyy-MM-dd"));
            builder.AppendLessThan(UserReportColumn.EndTime, endTime.ToString("yyyy-MM-dd"));
            TList<UserReport> tlist = DataRepository.UserReportProvider.Find(builder.GetParameters());
            if (tlist.Count > 0)
                return tlist[tlist.Count - 1];
            return null;
        }

        public string GeneratePDF(int sysUserId, string reportStarttime, string reportEndTime, EnumReportType type, bool isSysGen, string platforms, string keywordFamilyIDs, string emotionvalues)
        {
            ResponseUserReportJson response = new ResponseUserReportJson();
            try
            {
                DateTime start = DateTime.MaxValue;
                DateTime end = DateTime.MaxValue;

                DateTime.TryParse(reportStarttime, out start);
                DateTime.TryParse(reportEndTime, out end);
                if (start == DateTime.MaxValue || end == DateTime.MaxValue)
                {
                    response.issucc = false;
                    response.errormsg = ConstHelper.PARAMETER_ERROR;
                    return JsonHelper.ToJson(response);
                }

                SimplyReport simplyReport = new SimplyReport();
                UserReport entity = simplyReport.GeneratePDF(sysUserId, start, end, type, isSysGen, platforms, keywordFamilyIDs, emotionvalues);
                if (entity != null && entity.UserReportId > 0)
                {
                    response.issucc = true;
                    response.data = new UserReportJson();
                    response.data = JsonEntityUtility.SetJsonEntity(response.data, entity) as UserReportJson;
                }
                else
                {
                    response.issucc = false;
                }
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ex.Message + ex.StackTrace; //ConstHelper.INNER_ERROR;
                LogHelper.WriteLog(ex);
            }

            return JsonHelper.ToJson(response);
        }

        /// <summary>
        /// 获取我的信息 统计信息
        /// </summary>
        /// <returns></returns>
        public string GetInfoCenter(int sysUserId)
        {
            ResponseInfoCenterJson response = new ResponseInfoCenterJson();
            try
            {
                response.data = new InfoCenterJson();
                response.data.monitorsites = GetMonitorsites(sysUserId);
                response.data.keywordfamily = GetKeyWordfamily(sysUserId);
                response.data.historyreport = GetHistoryReport(sysUserId);
                response.data.todaynew = GetTodayNew(sysUserId);
                response.data.todaynewnegative = GetTodayNewNegative(sysUserId);
                response.data.total30day = GetTotal30day(sysUserId);
            }
            catch (Exception ex)
            {
                response.issucc = false;
                response.errormsg = ConstHelper.INNER_ERROR;
                LogHelper.WriteLog(ex);
            }
            return JsonHelper.ToJson(response);
        }

        /// <summary>
        /// 监测站点数
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetMonitorsites(int sysUserId)
        {
            return 0;
        }
        /// <summary>
        /// 近30天累计信息数
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetTotal30day(int sysUserId)
        {

            return GetViewDataCenterCount(sysUserId, DateTime.Now.AddDays(-30), DateTime.Now);
        }
        /// <summary>
        /// 今日新增信息数
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetTodayNew(int sysUserId)
        {
            return GetViewDataCenterCount(sysUserId, DateTime.Now, DateTime.Now);
        }
        /// <summary>
        /// 今日新增负面信息
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetTodayNewNegative(int sysUserId)
        {
            return GetViewDataCenterCount(sysUserId, DateTime.Now, DateTime.Now, ((int)EnumEmotionalValue.Negative).ToString());
        }
        /// <summary>
        /// 监测对象数
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetKeyWordfamily(int sysUserId)
        {
            return DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId).Count;
        }
        /// <summary>
        /// 历史报告数
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        private int GetHistoryReport(int sysUserId)
        {
            UserReportParameterBuilder builder = new UserReportParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.Append(UserReportColumn.SysUserId, sysUserId.ToString());
            builder.Append(UserReportColumn.IsSysGen, true.ToString());
            return DataRepository.UserReportProvider.Find(builder.GetParameters()).Count;
        }
        private int GetViewDataCenterCount(int sysUserId, DateTime start, DateTime end, string emotionalvalue = null)
        {
            ViewDataCenterParameterBuilder builder = new ViewDataCenterParameterBuilder();
            builder.Clear();
            builder.Junction = SqlUtil.AND;
            builder.Append(ViewDataCenterColumn.SysUserId, sysUserId.ToString());
            builder.AppendGreaterThanOrEqual(ViewDataCenterColumn.Datatime, start.ToString("yyyy-MM-dd"));
            builder.AppendLessThanOrEqual(ViewDataCenterColumn.Datatime, end.ToString("yyyy-MM-dd 23:59:59"));
            if (!string.IsNullOrEmpty(emotionalvalue))
            {
                builder.AppendIn(ViewDataCenterColumn.Emotionalvalue, emotionalvalue.Split(','));
            }
            int count = 0;
            DataRepository.ViewDataCenterProvider.Find(builder.GetParameters(), "", 1, 1, out count);
            return count;
        }
    }
}
