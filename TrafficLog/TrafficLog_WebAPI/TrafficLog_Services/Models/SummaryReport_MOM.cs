using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SummaryReport_MOM
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
        public Nullable<decimal> DealerTrafficMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerWritesMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosingMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> MonthlySalesForecastMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTrafficMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWritesMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingMOM { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerTrafficYOYMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerWritesYOYMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> DealerClosingYOYMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgTrafficYOYMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgWritesYOYMTD { get; set; }
        [JsonProperty]
        public Nullable<decimal> AreaAvgClosingYOYMTD { get; set; }
        [JsonProperty]
        public string OrderKey { get; set; }
    }
}
