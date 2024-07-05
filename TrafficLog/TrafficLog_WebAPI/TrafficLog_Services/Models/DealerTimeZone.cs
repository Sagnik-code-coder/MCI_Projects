using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class DealerTimeZone
{
    public short DealerTimeZoneId { get; set; }

    public string? DealerCode { get; set; }

    public decimal? TimeZone { get; set; }
}
