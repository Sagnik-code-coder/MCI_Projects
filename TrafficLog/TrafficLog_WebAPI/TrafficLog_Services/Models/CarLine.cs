using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class CarLine
{
    public int CarLineId { get; set; }

    public string? CarLineName { get; set; }

    public DateTime? CarLineEffectiveDate { get; set; }

    public DateTime? CarLineEndDate { get; set; }

    public bool? IsVisibleInReports { get; set; }
}
