using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class OtherDealership
{
    public int OtherDealerShipId { get; set; }

    public int DealerCode { get; set; }

    public int OtherManufacturerId { get; set; }

    public int NoOfDealerShip { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public virtual ExternalManufacturer OtherManufacturer { get; set; } = null!;
}
