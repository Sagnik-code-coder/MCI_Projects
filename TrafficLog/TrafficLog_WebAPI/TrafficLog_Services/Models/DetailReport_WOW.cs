using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public class DetailReport_WOW
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWAreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWAreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWClosing { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWAreaAvgClosing { get; set; }
        [JsonProperty]
        public Nullable<decimal> WOWMonthlySalesForecast { get; set; }
        [JsonProperty]
        public Nullable<decimal> YOYTraffic { get; set; }
        public Nullable<decimal> YOYAreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> YOYWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> YOYAreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> YOYClosing { get; set; }
        public Nullable<decimal> YOYAreaAvgClosing { get; set; }
    }
}
