using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SummaryReport_MTDTrend
    {
        [JsonProperty]
        public string DealerNumber4 { get; set; }
        [JsonProperty]
        public string DealerName4 { get; set; }
        //[JsonProperty]
        //public string DealerArea4 { get; set; }
        //[JsonProperty]
        //public string DealerRegion4 { get; set; }
        //[JsonProperty]
        //public string DealerDistrict4 { get; set; }

        [JsonProperty]
        public Nullable<int> DealerTraffic4 { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites4 { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing4 { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast4 { get; set; }
        [JsonProperty]
        public string DealerNumber3 { get; set; }
        [JsonProperty]
        public string DealerName3 { get; set; }
        //[JsonProperty]
        //public string DealerArea3 { get; set; }
        //[JsonProperty]
        //public string DealerRegion3 { get; set; }
        //[JsonProperty]
        //public string DealerDistrict3 { get; set; }

        [JsonProperty]
        public Nullable<int> DealerTraffic3 { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites3 { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing3 { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast3 { get; set; }
        [JsonProperty]
        public string DealerNumber2 { get; set; }
        [JsonProperty]
        public string DealerName2 { get; set; }
        //[JsonProperty]
        //public string DealerArea2 { get; set; }
        //[JsonProperty]
        //public string DealerRegion2 { get; set; }
        //[JsonProperty]
        //public string DealerDistrict2 { get; set; }

        [JsonProperty]
        public Nullable<int> DealerTraffic2 { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites2 { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing2 { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast2 { get; set; }
        [JsonProperty]
        public string DealerNumber1 { get; set; }
        [JsonProperty]
        public string DealerName1 { get; set; }
        //[JsonProperty]
        //public string DealerArea1 { get; set; }
        //[JsonProperty]
        //public string DealerRegion1 { get; set; }
        //[JsonProperty]
        //public string DealerDistrict1 { get; set; }

        [JsonProperty]
        public Nullable<int> DealerTraffic1 { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites1 { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing1 { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast1 { get; set; }
        [JsonProperty]
        public string DealerNumber { get; set; }
        [JsonProperty]
        public string DealerName { get; set; }
        //[JsonProperty]
        //public string DealerArea { get; set; }
        //[JsonProperty]
        //public string DealerRegion { get; set; }
        //[JsonProperty]
        //public string DealerDistrict { get; set; }

        [JsonProperty]
        public Nullable<int> DealerTraffic { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast { get; set; }
        [JsonProperty]
        public string OrderKey { get; set; }
    }
}
