﻿using System;
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
                if (reportType == (int)EnumReportType.DailyReport)
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
                else if (reportType == (int)EnumReportType.WeeklyReport)
                {
                    string[] strtime = date.Split('~');
                    startTime = DateTime.Parse(strtime[0].Trim());
                    endTime = DateTime.Parse(strtime[1]).AddDays(1);
                }
                else if (reportType == (int)EnumReportType.MonthlyReport)
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

    }
}