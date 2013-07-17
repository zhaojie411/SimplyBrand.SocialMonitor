using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public enum EnumLoginStatus
    {
        Success = 1,
        NameOrPasswordFail = 2,
        MemberStop = 3,
        Error = 4,
        MemberDelete = 5
    }

    public enum EnumPlatform
    {

        /// <summary>
        /// 微博
        /// </summary>
        Weibo = 1,
        /// <summary>
        /// 博客
        /// </summary>
        Blog = 2,
        /// <summary>
        /// 论坛
        /// </summary>
        BBS = 3,
        /// <summary>
        /// 新闻
        /// </summary>
        News = 4
    }
    public enum EnumSysUserStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 停用
        /// </summary>
        Stop = 2,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete = 3
    }
    public enum EnumKeywordStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 停用
        /// </summary>
        Stop = 2,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete = 3
    }


    public enum EnumSimuDmTaskStatus
    {
        /// <summary>
        /// 未开启
        /// </summary>
        NotOpen = 1,
        /// <summary>
        /// 等待运行
        /// </summary>
        WaitFor = 2,
        /// <summary>
        /// 开启
        /// </summary>
        Start = 3,
        /// <summary>
        /// 暂停
        /// </summary>
        Suspended = 4,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 5,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 6
    }

    public enum EnumSimuDMTargetLogStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 2
    }
    public enum EnumIsForbid
    {
        /// <summary>
        /// 静止关键词
        /// </summary>
        Success = 0,
        /// <summary>
        /// 关键词
        /// </summary>
        Fail = 1
    }

    public enum EnumEmotionalValue
    {
        /// <summary>
        /// 正面
        /// </summary>
        Positive = 1,
        /// <summary>
        /// 负面
        /// </summary>
        Negative = -1,
        /// <summary>
        /// 中性
        /// </summary>
        Neutral = 0

    }
    /// <summary>
    /// 报表审核状态
    /// </summary>
    public enum EnumReportStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        UnAudited = 1,
        /// <summary>
        /// 审核通过
        /// </summary>
        ThroughAudit = 2,
        /// <summary>
        /// 未审核通过
        /// </summary>
        UnauditedThrough = 3

    }

    public enum EnumReportType
    {
        /// <summary>
        /// 日报
        /// </summary>
        DayReport = 1,
        /// <summary>
        /// 周报
        /// </summary>
        WeekDayReport = 2,
        /// <summary>
        /// 月报
        /// </summary>
        MonthReport = 3,
        /// <summary>
        /// 用户自定义
        /// </summary>
        Custom = 4,
    }

}
