using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCI_NameTag_ModelCore
{
    [Table("OrderProcessInfo")]
    public class OrderProcessInfo : BaseEntity
    {
        [JsonProperty]
        [Key, Column(Order = 0)]
        public int OrderProcessID { get; set; }
        [JsonProperty]
        //[Key, Column(Order = 1)]
        public int OrderID { get; set; }
        [JsonProperty]
        public string ProcessType { get; set; }
    }
}
