namespace MCI_NameTag_Services_POC.Models.ViewModels
{
    public class InsertUpdateNameTagMasterAndDetailsModel
    {
        public string? siteUrl {  get; set; }
        public String[]? arrNameTagDetails {  get; set; }
        public string? dealerCode { get; set; }
        public string? confirmationEmail { get; set; } 
        public string? orderDescr { get; set; }
        public string? userType { get; set; } 
        public string? currentUser { get; set; }
        public int? orderID { get; set; }
    }
}
