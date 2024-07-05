using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [JsonObject]
    public partial class SummaryReport : DealerView
    {
        [JsonProperty]
        [NotMapped]
        public string Status { get; set; }
        [JsonProperty]
        [NotMapped]
        public DateTime? TimeStamp { get; set; }
    }
}
