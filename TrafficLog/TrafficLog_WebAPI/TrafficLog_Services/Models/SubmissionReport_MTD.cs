using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SubmissionReport_MTD
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<double> Traffic { get; set; }
        [JsonProperty]
        public Nullable<double> Writes { get; set; }
        [JsonProperty]
        public Nullable<double> Closing { get; set; }
        [JsonProperty]
        public Nullable<double> MonthlySalesForecast { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgTraffic { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgWrites { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgClosing { get; set; }
        [JsonProperty]
        public Nullable<double> MOMTraffic { get; set; }
        [JsonProperty]
        public Nullable<double> MOMWrites { get; set; }
        [JsonProperty]
        public Nullable<double> ClosingMOM { get; set; }
        [JsonProperty]
        public Nullable<double> MonthlySalesTarget { get; set; }

        [JsonProperty]
        public Nullable<double> Achievement { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgTrafficMOM { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgWritesMOM { get; set; }
        [JsonProperty]
        public Nullable<double> AreaAvgClosingMOM { get; set; }
    }
}
