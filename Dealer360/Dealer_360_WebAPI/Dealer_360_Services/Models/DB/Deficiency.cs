using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class Deficiency
{
    public int DeficiencyId { get; set; }

    public int Dssaid { get; set; }

    public string? Deficiency1 { get; set; }

    public string? DeficiencyCommitment { get; set; }

    public DateTime? DfcncyCrrDueDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual Dssadatum Dssa { get; set; } = null!;
}
