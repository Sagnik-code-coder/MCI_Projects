using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealer_360_Services.Models.View_Models
{
    [Keyless]
    public class GetDealerAwardsDetails
    {
        public string? DealerCode { get; set; }
        public string? AwardYear {  get; set; }
        
        public string? President_Club_Tier_1 {get;set; }
        
        public string? President_Club_Tier_2 { get; set; }
        
        public string? Grand_Performers { get; set; }
        
        public string? Dealer_of_Distinction { get; set; }


    }
}
