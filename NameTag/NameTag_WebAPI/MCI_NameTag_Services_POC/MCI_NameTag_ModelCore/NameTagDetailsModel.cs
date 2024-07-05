using Microsoft.EntityFrameworkCore;
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
    [Keyless]
    [Table("NameTagDetails")]
    public class NameTagDetailsModel : BaseEntity
    {
        //public string SiteUrl;

        [JsonProperty]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        [JsonProperty]
        public int OrderID { get; set; }
        [NotMapped]
        [JsonProperty]
        public string OrderStatusID { get; set; }
        [NotMapped]
        [JsonProperty]
        public string DealerCode { get; set; }
        [NotMapped]
        [JsonProperty]
        public string ConfirmationEmailID { get; set; }
        [NotMapped]
        [JsonProperty]
        public string OrderDescription { get; set; }
        [NotMapped]
        [JsonProperty]
        public string UserType { get; set; }
        [NotMapped]
        [JsonProperty]
        public DateTime LastOrderDate { get; set; }
        [JsonProperty]
        public string EmployeeID { get; set; }
        [JsonProperty]
        public string EmployeeType { get; set; }
        [NotMapped]
        [JsonProperty]
        public string EmployeeName { get; set; }
        [NotMapped]
        [JsonProperty]
        public string EmployeeFirstName { get; set; }
        [JsonProperty]
        public string NameTagDisplayName { get; set; }
        [JsonProperty]
        public string JobTitle { get; set; }
        [JsonProperty]
        public string ShipToAddress { get; set; }
        [NotMapped]
        [JsonProperty]
        public string NameTagType { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public string Tennure { get; set; }
        [JsonProperty]
        public decimal UnitCost { get; set; }
        [JsonProperty]
        public decimal Cost { get; set; }
        [JsonProperty]
        public decimal Tax { get; set; }
        [JsonProperty]
        public decimal TotalCost { get; set; }
        [JsonProperty]
        public bool? IsActive { get; set; }
        [JsonProperty]
        public string CreatedBy { get; set; }
        [NotMapped]
        [JsonProperty]
        public string SiteUrl { get; set; }
        [NotMapped]
        [JsonProperty]
        public string OrderStatus { get; set; }
        [NotMapped]
        [JsonProperty]
        public string DealershipName { get; set; }
    }

    [Keyless]
    public class NameTagMasterDetailsModel : BaseEntity
    {
        //public string SiteUrl;

        [JsonProperty]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        [JsonProperty]
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
        public DateTime LastOrderDate { get; set; }
        [JsonProperty]
        public string EmployeeID { get; set; }
        [JsonProperty]
        public string EmployeeType { get; set; }
        [JsonProperty]
        public string EmployeeName { get; set; }
        [NotMapped]
        [JsonProperty]
        public string EmployeeFirstName { get; set; }
        [JsonProperty]
        public string NameTagDisplayName { get; set; }
        [JsonProperty]
        public string JobTitle { get; set; }
        [JsonProperty]
        public string ShipToAddress { get; set; }
        [JsonProperty]
        public string NameTagType { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public string Tennure { get; set; }
        [JsonProperty]
        public decimal UnitCost { get; set; }
        [JsonProperty]
        public decimal Cost { get; set; }
        [JsonProperty]
        public decimal Tax { get; set; }
        [JsonProperty]
        public decimal TotalCost { get; set; }
        [JsonProperty]
        public bool? IsActive { get; set; }
        [JsonProperty]
        public string CreatedBy { get; set; }
        [NotMapped]
        [JsonProperty]
        public string SiteUrl { get; set; }
        [JsonProperty]
        public string OrderStatus { get; set; }
        [NotMapped]
        [JsonProperty]
        public string DealershipName { get; set; }
    }

    [Keyless]
    public class GetDealerName : BaseEntity
    {
        [JsonProperty]
        public string DealershipName { get; set; }

        
    }
    [Keyless]
    public class GetDealerNameAndOrderStatus : BaseEntity
    {
        [NotMapped]
        [JsonProperty]
        public string DealershipName { get; set; }
        [JsonProperty]
        public string OrderStatus { get; set; }
        [NotMapped]
        [JsonProperty]
        public string DealerCode { get; set; }
        [JsonProperty]
        public string EmployeeID { get; set; }
        [JsonProperty]
        public string EmployeeName { get; set; }
        [JsonProperty]
        public string OrderID { get; set; }
        
        [JsonProperty]
        public string OrderStatusID { get; set; }
    }
    [Keyless]
    public class GetDealerNameAndOrderStatuses : BaseEntity
    {
        [JsonProperty]
        public string DealershipName { get; set; }
        [JsonProperty]
        public string OrderStatus { get; set; }
        [JsonProperty]
        public string DealerCode { get; set; }
        [JsonProperty]
        public string EmployeeID { get; set; }
        [JsonProperty]
        public string EmployeeName { get; set; }
        [JsonProperty]
        public string OrderID { get; set; }
        [JsonProperty]
        public string OrderStatusID { get; set; }
        [JsonProperty]
        [Column("CreatedOn")]
        public DateTime? CreatedOn { get; set; }
    }
    [Keyless]
    public class GetConfirmationEmailID:BaseEntity
    {
        [JsonProperty]
        public string ConfirmationEmailID { get; set; }
    }
}
