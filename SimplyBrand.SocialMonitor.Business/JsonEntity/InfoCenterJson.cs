using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class InfoCenterJson : BaseJson
    {
        public int monitorsites { get; set; }
        public int total30day { get; set; }
        public int todaynew { get; set; }
        public int todaynewnegative { get; set; }
        public int keywordfamily { get; set; }
        public int historyreport { get; set; }
    }

    public class ResponseInfoCenterJson : ResponseJson
    {
        public InfoCenterJson data { get; set; }
    }
}
