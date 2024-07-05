using Microsoft.AspNetCore.Mvc;

namespace MCI_NameTag_Services_POC.Models.ViewModels
{
    public class UpdateNameTagOrder
    {
        public string siteUrl{get;set;}
        public int orderStatusID { get;set;}
        public int[] arrOrderID {  get;set;}
        public string[] arrProcessedOrder {  get;set;}
        public string currentUser { get;set;}
        public string culture { get; set;}
    }
}
