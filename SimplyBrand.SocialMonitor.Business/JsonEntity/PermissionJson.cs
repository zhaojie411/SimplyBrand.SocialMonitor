using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class PermissionJson : BaseCommonJson
    {
        public string desc { get; set; }
    }
    public class ResponsePermissionListJson : ResponseJson
    {
        public List<PermissionJson> data { get; set; }
    }
}
