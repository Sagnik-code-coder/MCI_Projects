using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public class SubmissionReport_MOM
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<double> TrafficMOM { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgTrafficMOM { get; set; }
        [JsonProperty]
        public Nullable<double> TrafficWritesMOM { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgWritesMOM { get; set; }
        [JsonProperty]
        public Nullable<double> ClosingMOM { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgClosingMOM { get; set; }
        [JsonProperty]
        public Nullable<double> MonthlySalesForecast { get; set; }
        [JsonProperty]
        public Nullable<double> TrafficYOY { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgTrafficYOY { get; set; }
        [JsonProperty]
        public Nullable<double> WritesYOY { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgWritesYOY { get; set; }

        [JsonProperty]
        public Nullable<double> ClosingYOY { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgClosingYOY { get; set; }
    }
}
