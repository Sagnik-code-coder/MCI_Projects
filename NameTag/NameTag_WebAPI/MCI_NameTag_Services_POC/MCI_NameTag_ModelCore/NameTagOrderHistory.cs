using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCI_NameTag_ModelCore
{
    public class NameTagOrderHistory : BaseEntity
    {
        [Key]
        [JsonProperty]
        public Int32 OrderID { get; set; }
        [JsonProperty]
        public string OrderDescription { get; set; }
        [JsonProperty]
        public Int32 Quantity { get; set; }
        [JsonProperty]
        public Decimal TotalCost { get; set; }
        [JsonProperty]
        public string OrderStatusName { get; set; }
        [NotMapped]
        [JsonProperty]
        public string Name { get; set;}
        [JsonProperty]
        public string DealerName { get; set; }
        [Column("CreatedOn")]
        [JsonProperty]
        public DateTime? CreatedOn { get; set; }
        [MaxLength(50), Column("CreatedBy")]
        [JsonProperty]
        public string? CreatedBy { get; set; }


    }
}
