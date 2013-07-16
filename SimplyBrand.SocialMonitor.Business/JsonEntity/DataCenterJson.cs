using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class DataCenterJson : BaseCommonJson
    {
        public long dataid { get; set; }

        public int datasourceid { get; set; }

        public string dataurl { get; set; }

        public string dataKey { get; set; }

        public string datatitle { get; set; }

        public string datatime { get; set; }

        public string datavalue { get; set; }

        public string createtime { get; set; }

        public string databody { get; set; }

        public string dataauthor { get; set; }

        public string newsplatform { get; set; }

        public string sitename { get; set; }

        public string dataforward { get; set; }

        public string datacomment { get; set; }

        public string bbsclick { get; set; }

        public string datasource { get; set; }

        public string weiboid { get; set; }

        public int emotionalvalue { get; set; }

        public string uid { get; set; }
    }

    public class DataCenterPageJson : BasePageJson
    {
        public List<DataCenterJson> items { get; set; }
    }

    public class ResponseDataCenterJson : ResponseJson
    {
        //public int count { get; set; }
        public DataCenterPageJson data { get; set; }
    }
    public class DataCenterSummaryItemJson : BaseJson
    {
        public long key { get; set; }
        public int value { get; set; }
        public string title { get; set; }
    }

    public class DataCenterSummaryJson : BaseJson
    {
        public Dictionary<string, List<DataCenterSummaryItemJson>> items { get; set; }
    }
    public class ResponseDataEmotionalJson : ResponseJson
    {
        public List<DataCenterSummaryItemJson> data { get; set; }
    }

    public class ResponseDataCenterSummaryJson : ResponseJson
    {
        public DataCenterSummaryJson data { get; set; }
    }

}
