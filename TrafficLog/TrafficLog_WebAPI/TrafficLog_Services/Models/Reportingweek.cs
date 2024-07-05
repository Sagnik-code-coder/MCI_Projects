using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class Reportingweek
{
    public DateTime? ReportinStartDate { get; set; }

    public DateTime? ReportinEndDate { get; set; }

    public string? SubmissionWindowDescriptionEnCurrent { get; set; }

    public string? SubmissionWindowDescriptionFrCurrent { get; set; }

    public string? SubmissionWindowDescriptionEnUpdated { get; set; }

    public string? SubmissionWindowDescriptionFrUpdated { get; set; }

    public string? F7 { get; set; }

    public string? SubmissionWindowDescriptionEnUpdatedFrw { get; set; }

    public string? SubmissionWindowDescriptionFrUpdatedFrw { get; set; }

    public string? WeekDescriptionEnFrw { get; set; }

    public string? WeekDescriptionFrFrw { get; set; }

    public string? F12 { get; set; }

    public string? WeekDescriptionEnFrwRevised2021 { get; set; }

    public string? WeekDescriptionFrFrwRevised2021 { get; set; }

    public string? WeekDescriptionEnFrwRevised2020 { get; set; }

    public string? WeekDescriptionFrFrwRevised2020 { get; set; }

    public string? SubmissionWindowStartDateRevised2021 { get; set; }

    public string? SubmissionWindowEndDateRevised2021 { get; set; }

    public string? SubmissionWindowFirstDeadlineRevised2021 { get; set; }
}
