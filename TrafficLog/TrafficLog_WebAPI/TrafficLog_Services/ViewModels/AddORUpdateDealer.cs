namespace TrafficLog_Services.ViewModels
{
    public class AddORUpdateDealer
    {
        public string Dealerdata { get; set; }
        public int Weekid { get; set; }
        public string Url { get; set; }
        public string? Local { get; set; }
        public string userDateTimeUtc { get; set; }

        public string UserType { get; set; }
    }
}
