using Microsoft.EntityFrameworkCore;

namespace Dealer_360_Services.Models.View_Models
{
    [Keyless]
    public class GetFacilityStandardVM
    {
        public int FacilityStandardId { get; set; }

        public int? NewUnitSalesVol { get; set; }

        public int? MinNoVehInShowroom { get; set; }

        public int? ShowroomSqft { get; set; }

        public int? SalesOfficeSqft { get; set; }

        public int? TotalSalesArea { get; set; }

        public int? NoOfSrvcBays { get; set; }

        public int? NoOfNewCarDelBays { get; set; }

        public int? TotalNoBays { get; set; }

        public int? SrvcReception { get; set; }

        public int? CustWaitingArea { get; set; }

        public int? TotalSrvcAreaSqft { get; set; }

        public int? PartsDeptSqft { get; set; }

        public int? GeneralOfficeOth { get; set; }

        public int? TotBuildingGuideline { get; set; }

        public int? TotSiteReqirement { get; set; }

        public decimal? Acrea { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public DateTime ModifiedOn { get; set; }
        public int? PartsDeptNote5Sqft { get; set; }
    }
}
