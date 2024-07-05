using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class TrafficLogCx90PreSplitView
{
    public int Carline { get; set; }

    public decimal? Traffic { get; set; }

    public decimal? Writes { get; set; }

    public decimal? MonthlySalesForecast { get; set; }

    public decimal? MonthlyTarget { get; set; }

    public string? DealerCode { get; set; }

    public int? ReportingWeekId { get; set; }
}
