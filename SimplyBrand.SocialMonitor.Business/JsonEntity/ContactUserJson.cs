using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class ContactUserJson : BaseCommonJson
    { 
        public int ContactUserId { get; set; }
        public string ContactUserName { get; set; }
        public string ContactUserTel { get; set; }
        public string ContactUserEmail { get; set; }       
        public bool ContactUserIsprimary { get; set; }       
        public int SysUserId { get; set; }
    }
    public class ResponseContactUserJson : ResponseJson
    {
        public int count { get; set; }
        public List<ContactUserJson> item { get; set; }
    }
}
