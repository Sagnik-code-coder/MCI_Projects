using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class SubmissionReportLoadError
{
    public double? F1 { get; set; }

    public double? F2 { get; set; }

    public double? F3 { get; set; }

    public int? ErrorCode { get; set; }

    public int? ErrorColumn { get; set; }

    public string? SubmissionStatus { get; set; }

    public int? ReportingWeekId { get; set; }
}
