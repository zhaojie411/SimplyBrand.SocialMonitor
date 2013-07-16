using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class BaseJson
    {

    }

    public class BaseCommonJson : BaseJson
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class BasePageJson : BaseJson
    {
        public int count { get; set; }
    }


    public class ResponseJson : BaseJson
    {
        public string errormsg { get; set; }
        public bool issucc { get; set; }
    }
}
