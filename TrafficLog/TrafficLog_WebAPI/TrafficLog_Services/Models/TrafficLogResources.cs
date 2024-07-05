using TrafficLog_Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrafficLog_Services.Models
{
    [NotMapped]
    [JsonObject]
    public class TrafficLogResources : TrafficLog
    {
        [JsonProperty]
        public string Model { get; set; }
        [JsonProperty]
        public string Closing { get; set; }
        [JsonProperty]
        public string Writes_MTD { get; set; }


        public TrafficLog ConvertToTrafficLog()
        {
            TrafficLog trafficLog = new TrafficLog()
            {

                TrafficLogId = this.TrafficLogId,
                ReportingWeekId = ReportingWeekId,
                UserName = this.UserName,
                CarLineId = this.CarLineId,
                MonthlySalesForecast = this.MonthlySalesForecast,
                WeeklyTraffic = this.WeeklyTraffic,
                WeeklyWrites = this.WeeklyWrites,
                MonthlyTarget = this.MonthlyTarget,
                CreatedBy = this.CreatedBy,
                CreatedDateTime = this.CreatedDateTime,
                ModifiedBy = this.ModifiedBy,
                ModifiedDateTime = this.ModifiedDateTime,
                IsDeleted = this.IsDeleted,
                DealerCode = this.DealerCode,
                DealerName = this.DealerName,
                DealerArea = this.DealerArea,
                DealerProvince = this.DealerProvince,
                DealerMetro = this.DealerMetro,
                DealerCity = this.DealerCity,
                DealerDistrict = this.DealerDistrict,
                DealerRegion = this.DealerRegion

            };

            return trafficLog;

        }
    }
}
