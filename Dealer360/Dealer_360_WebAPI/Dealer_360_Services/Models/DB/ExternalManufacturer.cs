using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class ExternalManufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual ICollection<OtherDealership> OtherDealerships { get; set; } = new List<OtherDealership>();
}
