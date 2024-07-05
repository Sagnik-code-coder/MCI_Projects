using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class DetailReport_MTD
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<decimal> Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> MonthlySalesForecast { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosing { get; set; }
        [JsonProperty]
        public Nullable<decimal> TrafficMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> WritesMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> ClosingMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> MontlySalesTarget_MTD { get; set; }

        [JsonProperty]
        public Nullable<decimal> Achievement { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTrafficMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWritesMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingMTD { get; set; }
    }
}
