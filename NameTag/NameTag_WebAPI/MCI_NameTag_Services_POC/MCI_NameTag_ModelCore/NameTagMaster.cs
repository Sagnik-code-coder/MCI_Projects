using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_ModelCore
{
    [Table("NameTagMaster")]
    public class NameTagMaster: BaseEntity
    {
        [JsonProperty]
        [Key]   
        public Int32 OrderID { get; set; }
        [JsonProperty]
        public Int32 OrderStatusID { get; set; }
        [JsonProperty]
        public string DealerCode { get; set; }
        [JsonProperty]
        public string ConfirmationEmailID { get; set; }
        [JsonProperty]
        public string OrderDescription { get; set; }
        [JsonProperty]
        public string UserType { get; set; }
        [JsonProperty]
        public bool IsActive { get; set; }
    }
}
