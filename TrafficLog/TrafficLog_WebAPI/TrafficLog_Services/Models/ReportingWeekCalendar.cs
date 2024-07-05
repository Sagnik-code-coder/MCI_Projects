using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrafficLog_Services.Models;

public partial class ReportingWeekCalendar
{
    [Key]
    public int ReportingWeekId { get; set; }

    //public string DealerArea { get; set; }

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

    public DateTime? CreatedDatetime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public string? ExcelFileNameLegacyProcess { get; set; }
}
