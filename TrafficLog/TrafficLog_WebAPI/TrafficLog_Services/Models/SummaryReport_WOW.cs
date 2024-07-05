using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SummaryReport_WOW
    {
        [JsonProperty]
        public string DealerNumber { get; set; }
        [JsonProperty]
        public string DealerName { get; set; }
        //[JsonProperty]
        //public string DealerArea { get; set; }
        //[JsonProperty]
        //public string DealerDistrict { get; set; }

        [JsonProperty]
        public Nullable<decimal> DealerTrafficWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerWritesWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosingWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> MonthlySalesForecastWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTrafficWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWritesWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingWOW { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerTrafficYOY { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerWritesYOY { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosingYOY { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTrafficYOY { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWritesYOY { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingYOY { get; set; }
        [JsonProperty]
        public string OrderKey { get; set; }

    }
}
