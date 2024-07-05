using Microsoft.EntityFrameworkCore;

namespace Dealer_360_Services.Models.View_Models
{
    [Keyless]
    public class InsertOrUpdateDealerAwardsDetails
    {
        public int DealerAwardsID { get; set; }
        public int AwardsID { get; set; }
        public int DealerCode { get; set; }
        public int AwardsYears { get; set; }
        public string CreatedBy { get; set; }   
        public int ReturnVal { get; set; }
        
    }
}
