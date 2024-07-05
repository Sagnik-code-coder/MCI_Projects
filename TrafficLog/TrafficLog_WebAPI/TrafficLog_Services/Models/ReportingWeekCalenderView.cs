using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class ReportingWeekCalenderView
{
    public int ReportingWeekId { get; set; }

    public int? ReportingWeekNumber { get; set; }

    public string? ReportingWeekDescriptionEn { get; set; }

    public string? ReportingWeekDescriptionFr { get; set; }

    public DateTime? ReportingWeekStartDate { get; set; }

    public DateTime? ReportingWeekEndDate { get; set; }

    public byte? ReportingMonthNumber { get; set; }

    public short? ReportingYearNumber { get; set; }

    public DateTime? SubmissionWindowStartDate { get; set; }

    public DateTime? SubmissionWindowEndDate { get; set; }

    public string? SubmissionWindowDescriptionEn { get; set; }

    public string? SubmissionWindowDescriptionFr { get; set; }

    public DateTime? SubmissionWindowFirstDeadline { get; set; }
}
