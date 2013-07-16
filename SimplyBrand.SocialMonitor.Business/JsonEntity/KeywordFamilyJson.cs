using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class KeywordFamilyJson : BaseCommonJson
    {
        public int SysUserId { get; set; }
        public int KeywordStatus { get; set; }
    }
    public class KeywordFamilyPageJson : BasePageJson
    {
        public List<KeywordFamilyJson> items { get; set; }
    }

    public class ResponseKeywordFamilyJson : ResponseJson
    {
        public List<KeywordFamilyJson> data { get; set; }
    }
    public class ResponseKeywordFamilyPageJson : ResponseJson
    {
        public KeywordFamilyPageJson data { get; set; }
    }
}
