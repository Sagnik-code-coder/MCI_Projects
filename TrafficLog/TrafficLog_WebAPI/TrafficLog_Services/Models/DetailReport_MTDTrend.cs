using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class DetailReport_MTDTrend
    {
        [JsonProperty]
        public string CarLineName { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week5Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week5Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week5Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week5MSF { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week4Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week4Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week4Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week4MSF { get; set; }
        public Nullable<decimal> Week3Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week3Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week3Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week3MSF { get; set; }
        public Nullable<decimal> Week2Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week2Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week2Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week2MSF { get; set; }
        public Nullable<decimal> Week1Traffic { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week1Writes { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week1Closing { get; set; }
        [JsonProperty]
        public Nullable<decimal> Week1MSF { get; set; }

    }
}
