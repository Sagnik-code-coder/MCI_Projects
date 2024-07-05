using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class CarLineViewCx90p
{
    public int CarLineId { get; set; }

    public string? CarLineName { get; set; }

    public DateTime? CarLineEffectiveDate { get; set; }

    public DateTime? CarLineEndDate { get; set; }
}
