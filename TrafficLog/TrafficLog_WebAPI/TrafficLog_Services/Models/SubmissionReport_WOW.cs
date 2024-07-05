using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SubmissionReport_WOW
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<double> TrafficWOW { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgTrafficWOW { get; set; }
        [JsonProperty]
        public Nullable<double> TrafficWritesWOW { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgWritesWOW { get; set; }
        [JsonProperty]
        public Nullable<double> ClosingWOW { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgClosingWOW { get; set; }
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
