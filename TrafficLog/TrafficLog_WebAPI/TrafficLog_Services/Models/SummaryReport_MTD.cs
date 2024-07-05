using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SummaryReport_MTD
    {
        [JsonProperty]
        public int DealerNumber { get; set; }
        [JsonProperty]
        public string DealerName { get; set; }
        //[JsonProperty]
        //public string DealerArea { get; set; }
        //[JsonProperty]
        //public string DealerDistrict { get; set; }

        [JsonProperty]

        //public int ReportingWeekID { get; set; }
        public Nullable<int> DealerTraffic { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosing { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesForecast { get; set; }
        [JsonProperty]
        public Nullable<int> AreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<int> AreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosing { get; set; }
        [JsonProperty]
        public int DealerNumberMTD { get; set; }
        [JsonProperty]
        public string DealerNameMTD { get; set; }
        //[JsonProperty]
        //public string DealerAreaMTD { get; set; }
        //[JsonProperty]
        //public string DealerDistrictMTD { get; set; }
        [JsonProperty]
        public Nullable<int> DealerTrafficMTD { get; set; }
        [JsonProperty]
        public Nullable<int> DealerWritesMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosingMTD { get; set; }
        [JsonProperty]
        public Nullable<int> MonthlySalesTarget { get; set; }

        [JsonProperty]
        public Nullable<decimal> Achievement { get; set; }
        [JsonProperty]
        public Nullable<int> AreaAvgTrafficMTD { get; set; }
        [JsonProperty]
        public Nullable<int> AreaAvgWritesMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingMTD { get; set; }
        [JsonProperty]
        public string OrderKey { get; set; }
    }
}
