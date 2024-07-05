using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCI_NameTag_ModelCore
{
    public class NameTagDetails : BaseEntity
    {
        [JsonProperty]
        public Int32 OrderDetailID { get; set; }
        [JsonProperty]
        public Int32 OrderID { get; set; }
        [JsonProperty]
        public string EmployeeID { get; set; }
        [JsonProperty]
        public string EmployeeType { get; set; }
        [JsonProperty]
        public string EmployeeName { get; set; }
        [JsonProperty]
        public string NameTagDisplayName { get; set; }
        [JsonProperty]
        public string JobTitle { get; set; }
        [JsonProperty]
        public string DealershipName { get; set; }
        [JsonProperty]
        public string ShiptToAddress { get; set; }
        [JsonProperty]
        public Int32 NameTagTypeID { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public string Tennure { get; set; }
        [JsonProperty]
        public Nullable<decimal> UnitCost { get; set; }
        [JsonProperty]
        public Nullable<decimal> Cost { get; set; }
        [JsonProperty]
        public Nullable<decimal> Tax { get; set; }
        [JsonProperty]
        public Nullable<decimal> TotalCost { get; set; }
        [JsonProperty]
        public bool? IsActive { get; set; }
        [JsonProperty]
        public string DealerCode { get; set; }

        [JsonProperty]
        public string OrderStatus { get; set; }
        [JsonProperty]
        public string ConfirmationEmailID { get; set; }
        [JsonProperty]
        public string OrderDescription { get; set; }
    }
}
