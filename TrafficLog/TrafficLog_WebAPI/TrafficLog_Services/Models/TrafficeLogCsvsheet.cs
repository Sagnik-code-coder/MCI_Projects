using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class TrafficeLogCsvsheet
{
    public DateTime TrafficStartDate { get; set; }

    public DateTime TrafficEndDate { get; set; }

    public DateTime SubmissionStateDate { get; set; }

    public DateTime SubmissinFirstDeadLine { get; set; }

    public DateTime SubmissionEndDate { get; set; }

    public string WeekNumber { get; set; } = null!;

    public string Month { get; set; } = null!;

    public int Year { get; set; }
}
