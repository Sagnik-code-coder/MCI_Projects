using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class DetailReport_MOM
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMAreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMAreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMClosing { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMAreaAvgClosing { get; set; }
        [JsonProperty]
        public Nullable<decimal> MOMMonthlySalesForecast { get; set; }
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
