using MCI_NameTag_ModelCore;
using Microsoft.EntityFrameworkCore;
using static MCI_NameTag_ModelCore.GetDealerName;

namespace MCI_NameTag_Services_POC.Models
{
    public class TDataContext : DbContext
    {
        public TDataContext(DbContextOptions<TDataContext> options) : base(options)
        {

            //var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            //// Database.SetInitializer<TrafficLogEntities>(null);
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = true;

        }
        public virtual DbSet<NameTagDetailsModel> NameTagDetailsModel { get; set; }
        public virtual DbSet<NameTagMasterDetailsModel> NameTagMasterDetailsModels { get; set; }
        //public virtual DbSet<NameTagMasterModel> NameTagMasterModelDetails { get; set; }
        public virtual DbSet<NameTagMaster> nameTagMaster { get; set; }
        public virtual DbSet<OrderProcessInfo> orderProcessInfos { get; set; }
        public virtual DbSet<NameTagOrderHistory> NameTagOrderHistories { get; set; }
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<DealerModel> DealerModels{ get; set; }
        public virtual DbSet<GetDealerName> GetDealerNames { get; set; }
        public virtual DbSet<GetDealerNameAndOrderStatus> GetDealerNameAndOrderStatus { get; set; }
        public virtual DbSet<GetDealerNameAndOrderStatuses> GetDealerNameAndOrderStatuses { get; set; }
        public virtual DbSet<GetConfirmationEmailID> GetConfirmationEmailIDs { get; set; }
    }
}
