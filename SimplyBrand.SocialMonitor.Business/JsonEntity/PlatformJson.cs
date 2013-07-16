using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class PlatformJson : BaseCommonJson
    {
    }

    public class ResponsePlatformListJson : ResponseJson
    {
        public List<PlatformJson> data { get; set; }
    }
}
