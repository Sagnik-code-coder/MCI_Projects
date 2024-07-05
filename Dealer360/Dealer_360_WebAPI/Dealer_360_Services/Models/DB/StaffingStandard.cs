using System;
using System.Collections.Generic;

namespace Dealer_360_Services.Models.DB;

public partial class StaffingStandard
{
    public int StaffingStandardId { get; set; }

    public int? NewUnitSalesVolumn { get; set; }

    public string? JobClassification { get; set; }

    public string? GeneralManagerOwner { get; set; }

    public string? Accountant { get; set; }

    public string? OfficeStaff { get; set; }

    public string? CustomRelCood { get; set; }

    public string? SalerManager { get; set; }

    public string? FinServiceManager { get; set; }

    public string? FleetLeasingManager { get; set; }

    public string? PreownedManager { get; set; }

    public string? SalesRepresentative { get; set; }

    public string? ClericalStaff { get; set; }

    public string? PartsManager { get; set; }

    public string? PartsAdvisor { get; set; }

    public string? PartsClerk { get; set; }

    public string? ServiceManager { get; set; }

    public string? ShopForeperson { get; set; }

    public string? ServicedAdvisor { get; set; }

    public string? ServiceClericalStaff { get; set; }

    public string? Technicians { get; set; }

    public string? Apprentices { get; set; }

    public string? Other { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public int? TotalPerson { get; set; }
}
