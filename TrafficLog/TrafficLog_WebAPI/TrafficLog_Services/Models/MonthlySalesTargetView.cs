using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class MonthlySalesTargetView
{
    public long Id { get; set; }

    public int? Dryear { get; set; }

    public string? District { get; set; }

    public string? Dealer { get; set; }

    public string Carline { get; set; } = null!;

    public string? Monthid { get; set; }

    public int? Qty { get; set; }
}
