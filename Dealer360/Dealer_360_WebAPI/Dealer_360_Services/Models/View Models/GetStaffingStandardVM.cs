using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Dealer_360_Services.Models.View_Models
{
    [Keyless]
    public class GetStaffingStandardVM
    {
        public int StaffingStandardID { get; set; }
        public int NewUnitSl { get; set; }
        public string JobCls {  get; set; }
        public string Administrative {  get; set; }
        public int GenMngrOwnr { get; set; }
        public int Accountant {  get; set; }
        public int OfcStaff { get; set; }
        public int CustmrRelCood {  get; set; }
        public string VicSalesDept { get; set; }
        public int SalesMngr { get; set; }
        public int FinServcMngr { get; set; }
        public int FleetndserMngr {  get; set; }
        public int PreOwnMngr {  get; set; }
        public int SalesRprtv {  get; set; }
        public int ClericalStaff { get; set; }
        public string PartDept { get; set; }
        public int PartsMngr { get; set; }
        public int PartsAdvisr {  get; set; }
        public int PartsClrk {  get; set; }
        public string ServcDept { get;set; }
        public int ServcMngr { get; set; }
        public int ShopForePerson { get; set; }
        public int ServcAdvsr {  get; set; }
        public int ClericalStaff1 {  get; set; }
        public int Technician {  get; set; }
        public int Apprentices {  get; set; }
        public int GrdOther {  get; set; }
        public string TotalPrsn {  get; set; }
        public bool? IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public DateTime ModifiedOn { get; set; }

    }
}



      //,[IsDeleted],[IsActive],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn] 