using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrafficLog_Services.Models;

[Keyless]
public partial class DealerView
{
    
    public string? DealerCode { get; set; }

    //public string DealerArea { get; set; }

    //public int ReportWeekID { get; set; }

    public string? Name { get; set; }

    public string? Area { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? Prov { get; set; }

    public string? District { get; set; }

    public string? Metro { get; set; }

    public string? Email { get; set; }

    public string? Volumn { get; set; }
}
