using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class StaffigStandard
{
    public int StaffingStandardId { get; set; }

    public int? NewUnitSalesVolumn { get; set; }

    public int? JobClassification { get; set; }

    public int? GeneralManagerOwner { get; set; }

    public int? Accountant { get; set; }

    public int? OfficeStaff { get; set; }

    public int? CustomRelCood { get; set; }

    public int? SalerManager { get; set; }

    public int? FinServiceManager { get; set; }

    public int? FleetLeasingManager { get; set; }

    public int? PreownedManager { get; set; }

    public int? SalesRepresentative { get; set; }

    public int? ClericalStaff { get; set; }

    public int? PartsManager { get; set; }

    public int? PartsAdvisor { get; set; }

    public int? PartsClerk { get; set; }

    public int? ServiceManager { get; set; }

    public int? ShopForeperson { get; set; }

    public int? ServicedAdvisor { get; set; }

    public int? ServiceClericalStaff { get; set; }

    public int? Technicians { get; set; }

    public int? Apprentices { get; set; }

    public int? Other { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }
}
