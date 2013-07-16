using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class SysUserJson : BaseCommonJson
    {
        public short status { get; set; }
        public string createdate { get; set; }
        public string enddate { get; set; }
        public string logo { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public string realname { get; set; }
        public string email { get; set; }
        public List<int> platformids { get; set; }
        public List<int> permissions { get; set; }
        public List<ContactUserJson> contacts { get; set; }

    }

    public class SysUserPageJson : BasePageJson
    {
        public List<SysUserJson> items { get; set; }
    }

    public class ResponseSysUserJson : ResponseJson
    {
        public SysUserJson data { get; set; }
    }
    public class ResponseSysUserPageJson : ResponseJson
    {
        public SysUserPageJson data { get; set; }
    }

}
