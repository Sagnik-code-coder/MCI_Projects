﻿namespace TrafficLog_Services.ViewModels
{
    public class GETDetailReport
    {
        public string DealerCode { get; set; }

        public string UserName { get; set; }

        public string Region { get; set; }
        public string District { get; set; }
        public int Weekid { get; set; }
        public string userDateTimeUtc { get; set; }
    }
}
