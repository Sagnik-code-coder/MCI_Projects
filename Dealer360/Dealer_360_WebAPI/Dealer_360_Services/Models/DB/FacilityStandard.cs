using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class FacilityStandard
{
    public int FacilityStandardId { get; set; }

    public int? NewUnitSalesVol { get; set; }

    public int? MinNoVhclShorm { get; set; }

    public int? MinShowRoomSqft { get; set; }

    public int? SalesOfficSqft { get; set; }

    public int? TotalSalesArea { get; set; }

    public int? MinNoOfSrvcBays { get; set; }

    public int? MinNoOfNewDlvrBays { get; set; }

    public int? TotalNoOfBays { get; set; }

    public int? MinSrvcRecetnSqft { get; set; }

    public int? MinCustWaitnSqft { get; set; }

    public int? TotalSrvcAreaSqft { get; set; }

    public int? MinPartDeptSqft { get; set; }

    public int? MinGenOffcOtherSqft { get; set; }

    public int? MinTotBuildGuidSqft { get; set; }

    public int? MinTotSiteReqSqft { get; set; }

    public decimal? MinAcreage { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public int? PartDeptNtFiveSqft { get; set; }
}
