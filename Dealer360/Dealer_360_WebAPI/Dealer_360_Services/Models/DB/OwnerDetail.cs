using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class OwnerDetail
{
    public int OwnerId { get; set; }

    public int Dssaid { get; set; }

    public string? Title { get; set; }

    public string? Name { get; set; }

    public decimal? PercentOwned { get; set; }

    public bool? ActiveStatus { get; set; }

    public bool? PublicStatus { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual Dssadatum Dssa { get; set; } = null!;
}
