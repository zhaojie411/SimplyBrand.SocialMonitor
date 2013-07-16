using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class SysAdminJson : BaseCommonJson
    {
        public bool issuper { get; set; }
    }

    public class ResponseSysAdminJson : ResponseJson
    {
        public SysAdminJson data { get; set; }
    }
}
