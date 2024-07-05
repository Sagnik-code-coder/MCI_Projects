using MCI_NameTag_ModelCore;
using System.Data;
using static MCI_NameTag_ModelCore.GetDealerName;

namespace MCI_NameTag_Services_POC.Models.Interface
{
    public interface INameTagDBServices<TEntity> where TEntity : BaseEntity
    {
        List<GetDealerName> GetDealershipName(string dealerCode, string siteUrl);
        List<GetDealerNameAndOrderStatus> GetDealershipNameAndStatus(string employeeId, string dealerCode, string culture, string siteUrl);
        string insertNameTagDetails(string siteUrl, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost,
               decimal cost, decimal tax, decimal totalCost, bool IsActive, string createdBy, string modifiedBy);

        List<OrderHistory> GetProcessedOrderData();
        string insertNameTagMasterAndDetails(string siteUrl, string dealerCode, string confirmationEmail, string orderDescr, string userType, string createdBy, string modifiedBy, DataTable dt);

        void updateNameTagMasterAndDetails(string siteUrl, int orderID, string OrderDetailID, string dealerCode, string ordDescr, string userType, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost,
               decimal cost, decimal tax, decimal totalCost, bool IsActive, string modifiedBy, bool isDelete);

        List<GetConfirmationEmailID> getConfimrationEmailIDs(string siteUrl, int orderId);
        string insertUpdateNameTagMasterAndDetails(string siteUrl, string dealerCode, string confirmationEmail, string orderDescr, string userType, string createdBy, string modifiedBy, int? orderID, string orderDetailID, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int? quantity, string tennure, decimal? unitCost,
               decimal? cost, decimal? tax, decimal? totalCost, bool IsActive, bool isDelete, DataTable dt);
        DataTable GetOrderDataDealer(List<ProcessedOrders> processedOrders, string siteUrl, string culture);
        DataTable GetOrderDataCorporate(List<ProcessedOrders> processedOrders, string siteUrl, string culture);
        DataTable GetOrderData(List<ProcessedOrders> processedOrders, string siteUrl, string culture);
    }
}
    