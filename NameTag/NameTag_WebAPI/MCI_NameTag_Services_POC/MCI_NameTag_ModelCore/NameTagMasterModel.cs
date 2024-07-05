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
    public class NameTagMasterModel : BaseEntity
    {
        [JsonProperty]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        [JsonProperty]
        public int OrderStatusID { get; set; }
        [JsonProperty]
        public string DealerCode { get; set; }
        [JsonProperty]
        public string ConfirmationEmailID { get; set; }
        [JsonProperty]
        public string OrderDescription { get; set; }
        [JsonProperty]
        public string UserType { get; set; }
        [JsonProperty]
        public bool? IsActive { get; set; }
    }
}
