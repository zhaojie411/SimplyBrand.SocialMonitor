using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class RoleJson : BaseCommonJson
    {
        public string desc { get; set; }
    }

    public class ResponseRoleListJson : ResponseJson
    {
        public List<RoleJson> data { get; set; }
    }
}
