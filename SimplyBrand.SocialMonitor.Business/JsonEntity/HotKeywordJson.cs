using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class HotKeywordJson : BaseCommonJson
    {
        public int weight { get; set; }
        public int keywordFamilyID { get; set; }
    }

    public class HotKeywordPageJson : BasePageJson
    {
        public List<HotKeywordJson> items { get; set; }
    }

    public class ResponseHotKeywordPageJson : ResponseJson
    {
        public HotKeywordPageJson data { get; set; }
    }
}