using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class AwardMaster
{
    public int AwardId { get; set; }

    public string AwardName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual ICollection<DealerAward> DealerAwards { get; set; } = new List<DealerAward>();
}
