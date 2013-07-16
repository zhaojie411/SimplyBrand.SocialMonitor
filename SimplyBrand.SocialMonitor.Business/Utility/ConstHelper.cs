using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public class ConstHelper
    {
        /// <summary>
        /// 网络异常
        /// </summary>
        public const string NETWORK_ERROR = "网络异常";
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const string INNER_ERROR = "内部错误";

        /// <summary>
        /// 用户名或密码不正确
        /// </summary>
        public const string NAMEORPASSWORD_ERROR = "用户名或密码不正确";

        /// <summary>
        /// 用户已被停用
        /// </summary>
        public const string SYSUSERSTOP_INFO = "用户已被停用";

        /// <summary>
        /// 用户已经被删除
        /// </summary>
        public const string SYSUSERDELETE_INFO = "用户已被删除";
        /// <summary>
        /// 用户已经存在
        /// </summary>
        public const string SYSUSERHASEXISTS_INFO = "用户已经存在";
        /// <summary>
        /// 该用户使用期限已到，请联系管理员
        /// </summary>
        public const string SYSUSERENDTIME_INFO = "该用户使用期限已到，请联系管理员";
        /// <summary>
        /// 截止时间格式不正确
        /// </summary>
        public const string SYSUSERENDTIMEFORMAT_ERROR = "截止时间格式不正确";
        /// <summary>
        /// 参数错误
        /// </summary>
        public const string PARAMETER_ERROR = "参数错误";
    }
}
