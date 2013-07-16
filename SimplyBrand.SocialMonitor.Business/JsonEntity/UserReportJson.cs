using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class UserReportJson : BaseCommonJson
    {
        public string filename { get; set; }
        public string filepath { get; set; }
        public string link { get; set; }
        public string createdate { get; set; }
        public string desc { get; set; }
        public int reporttype { get; set; }
        public bool issysgen { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public int reportstatus { get; set; }
        public SysUserJson sysuser { get; set; }

    }

    public class UserReportPageJson : BasePageJson
    {
        public List<UserReportJson> items { get; set; }
    }

    public class ResponseUserReportJson : ResponseJson
    {
        public UserReportJson data { get; set; }
    }

    public class ResponseUserReportPageJson : ResponseJson
    {
        public UserReportPageJson data { get; set; }
    }
}
