using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class TrafficLogHistory
{
    public int TrafficLogId { get; set; }

    public int ReportingWeekId { get; set; }

    public string? UserName { get; set; }

    public int CarLineId { get; set; }

    public short? WeeklyTraffic { get; set; }

    public short? WeeklyWrites { get; set; }

    public short? MonthlySalesForecast { get; set; }

    public short? MonthlyTarget { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DealerCode { get; set; }

    public string? DealerName { get; set; }

    public string? DealerArea { get; set; }

    public string? DealerProvince { get; set; }

    public string? DealerMetro { get; set; }

    public string? DealerCity { get; set; }

    public string? DealerDistrict { get; set; }

    public string? DealerRegion { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }
}
