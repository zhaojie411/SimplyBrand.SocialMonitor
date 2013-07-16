using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class UserKeywordJson : BaseCommonJson
    {
        public bool IsPrimary { get; set; }
        public int FamilyId { get; set; }
        public string CreateTime  { get; set; }
        public bool IsForbid { get; set; }
    }
    public class ResponseUserKeywordJson : ResponseJson
    {
        public List<UserKeywordJson> data { get; set; }
    }
}
