using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrafficLog_Services.Models;

public partial class HistoricalSubmissionReport
{
    //[Key]
    //public int Id { get; set; }

    public string? RegionCode { get; set; }

    public string? DistrictCode { get; set; }

    public string? DealerCode { get; set; }

    public string? SubmissionStatus { get; set; }

    public int ReportingWeekId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ReportingWeekCalendar ReportingWeek { get; set; } = null!;
}
