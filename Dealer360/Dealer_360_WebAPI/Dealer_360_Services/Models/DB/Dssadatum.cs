using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class Dssadatum
{
    public int Dssaid { get; set; }

    public int DealerCode { get; set; }

    public bool? MultipleOwner { get; set; }

    public string? Representation { get; set; }

    public string? MarketType { get; set; }

    public DateTime? CurrentAgreementTerm { get; set; }

    public string? OffSiteLocation { get; set; }

    public string? OffSiteLocationUse { get; set; }

    public string? AdditionalProvisions { get; set; }

    public string? ImportantCommnets { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual ICollection<Deficiency> Deficiencies { get; set; } = new List<Deficiency>();

    public virtual ICollection<OwnerDetail> OwnerDetails { get; set; } = new List<OwnerDetail>();
}
