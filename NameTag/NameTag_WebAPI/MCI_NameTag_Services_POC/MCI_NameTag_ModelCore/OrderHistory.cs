using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MCI_NameTag_ModelCore
{
    public class OrderHistory:BaseEntity
    {
        [JsonProperty]
        [Key]
        public int OrderProcessID { get; set; }
        [JsonProperty]
        public string ProcessedBy { get; set; }
        [JsonProperty]
        public DateTime ProcessedOn { get; set; }
    }
}
