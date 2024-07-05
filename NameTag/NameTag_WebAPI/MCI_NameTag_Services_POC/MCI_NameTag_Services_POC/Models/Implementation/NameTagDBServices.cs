using MCI_NameTag_ModelCore;
using MCI_NameTag_Services_POC.Models.Interface;
using MCI_NameTag_UtilityCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using static MCI_NameTag_ModelCore.GetDealerName;

namespace MCI_NameTag_Services_POC.Models.Implementation
{
    public class NameTagDBServices<TEntity> : INameTagDBServices<TEntity> where TEntity : BaseEntity
    {
        private readonly TDataContext _context;
        private readonly DbSet<TEntity> _entities;

        public NameTagDBServices(TDataContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public List<GetDealerName> GetDealershipName(string dealerCode, string siteUrl)
        {
            List<GetDealerName> list = new List<GetDealerName>();
            try
            {
                List<SqlParameter> dealerCodeParam = new List<SqlParameter> { 
                    new SqlParameter { ParameterName = "@DealerCode", Value = (dealerCode == null ? (object)DBNull.Value : dealerCode) } };
                List<GetDealerName> tempDBSet = _context.GetDealerNames.FromSqlRaw("Exec dbo.usp_GetDealershipName @DealerCode", dealerCodeParam.ToArray()).ToList();
                if (tempDBSet != null)
                {
                    list = tempDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, GetDealershipName", ex);
                return null;
            }
            return list;
        }

        public List<GetDealerNameAndOrderStatus> GetDealershipNameAndStatus(string employeeId, string dealerCode, string culture, string siteUrl)
        {
            List<GetDealerNameAndOrderStatus> list = new List<GetDealerNameAndOrderStatus>();
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@EmployeeID",Value= (employeeId == null ? (object)DBNull.Value : employeeId) },
                new SqlParameter{ParameterName="@DealerCode",Value= (dealerCode == null ? (object)DBNull.Value : dealerCode) },
                new SqlParameter{ParameterName="@Culture",Value= (culture == null ? (object)DBNull.Value : culture) }
                    };
                var tempDBSet = _context.GetDealerNameAndOrderStatus.FromSqlRaw("dbo.usp_GetDealershipNameAndOrderStatus @EmployeeID,@DealerCode,@Culture", Params.ToArray()).ToList();
                if (tempDBSet != null)
                {
                    list = tempDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, GetDealershipNameAndStatus", ex);
                return null;
            }
            return list;
        }

        public List<GetDealerNameAndOrderStatuses> GetDealershipNameAndStatuses(string employeeId, string dealerCode, string culture, string siteUrl)
        {
            List<GetDealerNameAndOrderStatuses> list = new List<GetDealerNameAndOrderStatuses>();
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@EmployeeID",Value= (employeeId == null ? (object)DBNull.Value : employeeId) },
                new SqlParameter{ParameterName="@DealerCode",Value= (dealerCode == null ? (object)DBNull.Value : dealerCode) },
                new SqlParameter{ParameterName="@Culture",Value= (culture == null ? (object)DBNull.Value : culture) }
                    };
                var tempDBSet = _context.GetDealerNameAndOrderStatuses.FromSqlRaw("dbo.usp_GetDealershipNameAndOrderStatus @EmployeeID,@DealerCode,@Culture", Params.ToArray()).ToList();
                if (tempDBSet != null)
                {
                    list = tempDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, GetDealershipNameAndStatus", ex);
                return null;
            }
            return list;
        }

        public List<GetConfirmationEmailID> getConfimrationEmailIDs(string siteUrl, int orderId)
        {
            List<GetConfirmationEmailID> list = new List<GetConfirmationEmailID>();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter{ParameterName="@OrderID",Value= (orderId == 0 ? 0 : orderId) }
                };
                var tempDBSet = _context.GetConfirmationEmailIDs.FromSqlRaw("dbo.usp_GetConfirmEmailIDs @OrderID", parms.ToArray()).ToList();
                if (tempDBSet != null)
                {
                    list = tempDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, getConfimrationEmailIDs", ex);
                return null;
            }
            return list;
        }

        public void deleteNameTagOrder(string siteUrl, int orderId)
        {
            try
            {
                List<SqlParameter> Param = new List<SqlParameter>{
                    new SqlParameter{ParameterName="@OrderID",Value= (orderId == 0 ? 0 : orderId) }
                };
                var tempDBSet = _context.Database.ExecuteSqlRaw("dbo.usp_DeleteNameTagOrder @OrderID", Param.ToArray());

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, deleteNameTagOrder", ex);
            }
        }

        public List<NameTagMasterDetailsModel> getNameTagMasterDetails(string orderID, string culture)
        {
            List<NameTagMasterDetailsModel> list = new List<NameTagMasterDetailsModel>();
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>{
                    new SqlParameter{ParameterName="@OrderID",Value= (orderID == null ? (object)DBNull.Value : orderID) },
                    new SqlParameter{ParameterName="@Culture",Value= (culture == null ? (object)DBNull.Value : culture) }
                };
                var tempDBSet = _context.NameTagMasterDetailsModels.FromSqlRaw("dbo.usp_GetNameTagMasterDetails @OrderID,@Culture", Params.ToArray()).ToList();
                if (tempDBSet != null)
                {
                    list = tempDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog("GetNameTagMasterDetails", "Error in NameTag BasicDBServices, getNameTagMasterDetails", ex);
                return null;
            }
            return list;
        }

        public List<NameTagOrderHistory> getNameTagOrderHistory(string siteUrl, string culture, string DealerCode)
        {
            List<NameTagOrderHistory> lstNameTagOrderHistory = new List<NameTagOrderHistory>();
            try
            {

                List<SqlParameter> Params = new List<SqlParameter>{
                    new SqlParameter{ParameterName="@Culture",Value= culture },
                    new SqlParameter{ParameterName="@DealerCode",Value= DealerCode }
                };
                var orderHistory = _context.NameTagOrderHistories.FromSqlRaw("dbo.usp_GetNameTagOrderHistory @Culture, @DealerCode",Params.ToArray());

                if (orderHistory != null)
                {
                    lstNameTagOrderHistory = orderHistory.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, getNameTagOrderHistory", ex);
                return null;
            }
            return lstNameTagOrderHistory;
        }

        public void updateNameTagOrderHistory(string siteUrl, int orderStatusID, int[] arrOrderID, string[] arrProcessedOrder, string currentUser)
        {
            try
            {
                var dtOrderID = new DataTable();
                dtOrderID.TableName = "dbo.ut_OrderStatusIDList";
                dtOrderID.Columns.Add("NameTagOrderID", typeof(int));
                foreach (int orderID in arrOrderID)
                {
                    dtOrderID.Rows.Add(orderID);

                }
                var dtProcessedOrder = new DataTable();
                dtProcessedOrder.TableName = "dbo.ut_OrderProcessInfo";
                dtProcessedOrder.Columns.Add("OrderProcessID", typeof(int));
                dtProcessedOrder.Columns.Add("OrderID", typeof(int));
                dtProcessedOrder.Columns.Add("ProcessType", typeof(string));
                dtProcessedOrder.Columns.Add("CreatedBy", typeof(string));
                dtProcessedOrder.Columns.Add("CreatedOn", typeof(DateTime));
                dtProcessedOrder.Columns.Add("ModifiedBy", typeof(string));
                dtProcessedOrder.Columns.Add("ModifiedOn", typeof(DateTime));
                foreach (string item in arrProcessedOrder)
                {
                    DataRow drProcesses = dtProcessedOrder.NewRow();
                    drProcesses["OrderProcessID"] = item.Split(';')[0];
                    drProcesses["OrderID"] = item.Split(';')[1];
                    drProcesses["ProcessType"] = item.Split(';')[2];
                    drProcesses["CreatedBy"] = item.Split(';')[3];
                    drProcesses["CreatedOn"] = item.Split(';')[4];
                    drProcesses["ModifiedBy"] = item.Split(';')[5];
                    drProcesses["ModifiedOn"] = item.Split(';')[6];

                    dtProcessedOrder.Rows.Add(drProcesses);
                }
                List<SqlParameter> Params =new List<SqlParameter>{
                    new SqlParameter{ParameterName="@approvedOrders",
                        SqlDbType= SqlDbType.Structured,
                TypeName = dtOrderID.TableName,
                Value = dtOrderID },

                new SqlParameter{ParameterName="@ProcessedOrder", 
                SqlDbType=SqlDbType.Structured,TypeName = dtProcessedOrder.TableName,
                Value = dtProcessedOrder},

                new SqlParameter{ParameterName="@orderStatus",Value= orderStatusID },
                new SqlParameter{ParameterName="@modifiedBy",Value= currentUser }
                };

                _context.Database.ExecuteSqlRaw("dbo.usp_UpdateNameTagOrder @approvedOrders,@ProcessedOrder, @orderStatus, @modifiedBy", Params.ToArray());

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, updateNameTagOrderHistory", ex);
            }
        }


        public string insertNameTagDetails(string siteUrl, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost,
               decimal cost, decimal tax, decimal totalCost, bool IsActive, string createdBy, string modifiedBy)
        {
            string newOrderID = "";
            int ordId = 100001;
            List<NameTagDetailsModel> lstNameTagDetails = new List<NameTagDetailsModel>();
            try
            {
                List<SqlParameter> parammeters = new List<SqlParameter>{
                    new SqlParameter{ParameterName="@OrderID",Value= ordId },
                    new SqlParameter{ParameterName="@EmployeeID",Value= employeeID },
                    new SqlParameter{ParameterName="@EmployeeType",Value= employeeType },
                    new SqlParameter{ParameterName="@EmployeeFirstName",Value= employeeFirstName },
                    new SqlParameter{ParameterName="@EmployeeLastName",Value= employeeLastName },
                    new SqlParameter{ParameterName="@NameTagDisplayName",Value= nameTagDisplayName },
                    new SqlParameter{ParameterName="@JobTitle",Value= jobTitle },
                    new SqlParameter{ParameterName="@ShipToAddress",Value= shipToAddress },
                    new SqlParameter{ParameterName="@NameTagType",Value= "Regular/Metal" },
                    new SqlParameter{ParameterName="@Quantity",Value= quantity },
                    new SqlParameter{ParameterName="@Tennure",Value= tennure },
                    new SqlParameter{ParameterName="@UnitCost",Value= unitCost },
                    new SqlParameter{ParameterName="@Cost",Value= cost },
                    new SqlParameter{ParameterName="@Tax",Value= tax },
                    new SqlParameter{ParameterName="@TotalCost",Value= totalCost },
                    new SqlParameter{ParameterName="@IsActive",Value= true },
                    new SqlParameter{ParameterName="@CreatedBy",Value= createdBy },
                    new SqlParameter{ParameterName="@ModifiedBy",Value= modifiedBy },
                    new SqlParameter{ParameterName="@IsDelete",Value= false }
                };
                var nameTagDetailsDBSet = _context.NameTagDetailsModel.FromSqlRaw("usp_InsertNameTagDetails @OrderID,@EmployeeID,@EmployeeType,@EmployeeFirstName,@EmployeeLastName,@NameTagDisplayName,@JobTitle, @ShipToAddress,@NameTagType,@Quantity,@Tennure,@UnitCost,@Cost,@Tax,@TotalCost,@IsActive,@CreatedBy,@ModifiedBy,@IsDelete",
                  parammeters.ToArray());
                if (nameTagDetailsDBSet != null)
                {
                    lstNameTagDetails = nameTagDetailsDBSet.ToList();
                }

                if (lstNameTagDetails.Count > 0)
                {
                    newOrderID = lstNameTagDetails.Select(C => C.OrderID).First().ToString();
                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, insertNameTagDetails", ex);
            }
            return newOrderID;
        }

        public string insertNameTagMasterAndDetails(string siteUrl, string dealerCode, string confirmationEmail, string orderDescr, string userType, string createdBy, string modifiedBy, DataTable dt)
        {
            string newOrderID = "";
            List<NameTagDetailsModel> lstNameTagDetails = new List<NameTagDetailsModel>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter> {
                    new SqlParameter{ParameterName="@NameTagDetails",Value= dt ,TypeName="ut_NameTagDetailsTableType" ,SqlDbType=SqlDbType.Structured },
                    new SqlParameter{ParameterName="@DealerCode",Value= dealerCode },
                    new SqlParameter{ParameterName="@OrderDescription",Value= orderDescr },
                    new SqlParameter{ParameterName="@UserType",Value= userType },
                    new SqlParameter{ParameterName="@ConfirmationEmailID",Value= confirmationEmail },
                    new SqlParameter{ParameterName="@CreatedBy",Value= createdBy },
                    new SqlParameter{ParameterName="@ModifiedBy",Value= modifiedBy }
                };
                var nameTagDetailsDBSet = _context.NameTagDetailsModel.FromSqlRaw("usp_InsertNameTagDetails @NameTagDetails,@DealerCode,@OrderDescription,@UserType,@ConfirmationEmailID,@CreatedBy,@ModifiedBy", param.ToArray());
                
                if (nameTagDetailsDBSet != null)
                {
                    lstNameTagDetails = nameTagDetailsDBSet.ToList();
                }

                if (lstNameTagDetails.Count > 0)
                {
                    newOrderID = lstNameTagDetails.Select(C => C.OrderID).First().ToString();
                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, insertNameTagMasterAndDetails", ex);
            }
            return newOrderID;
        }

        public string insertUpdateNameTagMasterAndDetails(string siteUrl, string dealerCode, string confirmationEmail, string orderDescr, string userType, string createdBy, string modifiedBy, int? orderID, string orderDetailID, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int? quantity, string tennure, decimal? unitCost,
               decimal? cost, decimal? tax, decimal? totalCost, bool IsActive, bool isDelete, DataTable dt)
        {
            string newOrderID = "";
            List<NameTagDetailsModel> lstNameTagDetails = new List<NameTagDetailsModel>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter> {
                    new SqlParameter{ParameterName="@NameTagDetails",Value= dt,
                    SqlDbType = System.Data.SqlDbType.Structured,
                    TypeName = "ut_NameTagDetailsTableType" },
                    new SqlParameter{ParameterName="@DealerCode",Value= dealerCode},
                    new SqlParameter{ParameterName="@OrderDescription",Value= orderDescr },
                    new SqlParameter{ParameterName="@UserType",Value= userType },
                    new SqlParameter{ParameterName="@ConfirmationEmailID",Value= (confirmationEmail == null ? (object)DBNull.Value : confirmationEmail) },
                    new SqlParameter{ParameterName="@CreatedBy",Value= createdBy },
                    new SqlParameter{ParameterName="@ModifiedBy",Value= modifiedBy },

                    new SqlParameter{ParameterName="@OrderID",Value= orderID },
                    new SqlParameter{ParameterName="@OrderDetailID",Value= orderDetailID },
                    new SqlParameter{ParameterName="@EmployeeID",Value= (employeeID == null ? (object)DBNull.Value : employeeID) },
                    new SqlParameter{ParameterName="@EmployeeType",Value= (employeeType == null ? (object)DBNull.Value : employeeType) },
                    new SqlParameter{ParameterName="@EmployeeFirstName",Value= (employeeFirstName == null ? (object)DBNull.Value : employeeFirstName) },
                    new SqlParameter{ParameterName="@EmployeeLastName",Value= (employeeLastName == null ? (object)DBNull.Value : employeeLastName) },
                    new SqlParameter{ParameterName="@NameTagDisplayName",Value= (nameTagDisplayName == null ? (object)DBNull.Value : nameTagDisplayName) },
                    new SqlParameter{ParameterName="@JobTitle",Value= (jobTitle == null ? (object)DBNull.Value : jobTitle) },
                    new SqlParameter{ParameterName="@ShipToAddress",Value= (shipToAddress == null ? "" : "") },
                    new SqlParameter{ParameterName="@NameTagType",Value= "Regular/Metal" },
                    new SqlParameter{ParameterName="@Quantity",Value= (quantity == null ? 0 : quantity) },
                    new SqlParameter{ParameterName="@Tennure",Value= (tennure == null ? (object)DBNull.Value : tennure) },
                    new SqlParameter{ParameterName="@UnitCost",Value= (unitCost == null ? 0 : unitCost) },
                    new SqlParameter{ParameterName="@Cost",Value= (cost == null ? 0 : cost) },
                    new SqlParameter{ParameterName="@Tax",Value= (tax == null ? 0 : tax) },
                    new SqlParameter{ParameterName="@TotalCost",Value= (totalCost == null ? 0 : totalCost) },
                    new SqlParameter{ParameterName="@IsActive",Value= true },
                    new SqlParameter{ParameterName="@IsDelete",Value= isDelete }

                };
                var nameTagDetailsDBSet = _context.NameTagDetailsModel.FromSqlRaw("usp_InsertNameTagDetails @NameTagDetails,@DealerCode,@OrderDescription,@UserType,@ConfirmationEmailID,@CreatedBy,@ModifiedBy,@OrderID,@OrderDetailID,@EmployeeID,@EmployeeType,@EmployeeFirstName,@EmployeeLastName,@NameTagDisplayName,@JobTitle,@ShipToAddress,@NameTagType,@Quantity,@Tennure,@UnitCost,@Cost,@Tax,@TotalCost,@IsActive,@IsDelete", param.ToArray());

                if (nameTagDetailsDBSet != null)
                {
                    lstNameTagDetails = nameTagDetailsDBSet.ToList();
                }

                if (lstNameTagDetails.Count > 0)
                {
                    newOrderID = lstNameTagDetails.Select(C => C.OrderID).First().ToString();
                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, insertNameTagMasterAndDetails", ex);
            }
            return newOrderID;
        }

        public void updateNameTagMasterAndDetails(string siteUrl, int orderID, string orderDetailID, string dealerCode, string ordDescr, string userType, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost,
               decimal cost, decimal tax, decimal totalCost, bool IsActive, string modifiedBy, bool isDelete)
        {
            List<NameTagDetailsModel> lstNameTagDetails = new List<NameTagDetailsModel>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter> {
                    new SqlParameter{ParameterName="@OrderID",Value= orderID },
                    new SqlParameter{ParameterName="@OrderDetailID",Value= orderDetailID },
                    new SqlParameter{ParameterName="@DealerCode",Value= dealerCode },
                    new SqlParameter{ParameterName="@OrderDescription",Value= ordDescr },
                    new SqlParameter{ParameterName="@UserType",Value= userType },
                    new SqlParameter{ParameterName="@EmployeeID",Value= employeeID },
                    new SqlParameter{ParameterName="@EmployeeType",Value= employeeType },
                    new SqlParameter{ParameterName="@EmployeeFirstName",Value= employeeFirstName },
                    new SqlParameter{ParameterName="@EmployeeLastName",Value= employeeLastName },
                    new SqlParameter{ParameterName="@NameTagDisplayName",Value= nameTagDisplayName },
                    new SqlParameter{ParameterName="@JobTitle",Value= jobTitle },
                    new SqlParameter{ParameterName="@ShipToAddress",Value= shipToAddress },
                    new SqlParameter{ParameterName="@NameTagType",Value= "Regular/Metal" },
                    new SqlParameter{ParameterName="@Quantity",Value= quantity },
                    new SqlParameter{ParameterName="@Tennure",Value= tennure },
                    new SqlParameter{ParameterName="@UnitCost",Value= unitCost },
                    new SqlParameter{ParameterName="@Cost",Value= cost },
                    new SqlParameter{ParameterName="@Tax",Value= tax },
                    new SqlParameter{ParameterName="@TotalCost",Value= totalCost },
                    new SqlParameter{ParameterName="@IsActive",Value= true },
                    new SqlParameter{ParameterName="@CreatedBy",Value= modifiedBy },
                    new SqlParameter{ParameterName="@ModifiedBy",Value= modifiedBy },
                    new SqlParameter{ParameterName="@IsDelete",Value= isDelete }
                    };

                var nameTagDetailsDBSet = _context.NameTagDetailsModel.FromSqlRaw("usp_UpdateNameTagDetails @OrderID,@OrderDetailID,@DealerCode,@OrderDescription,@UserType,@EmployeeID,@EmployeeType,@EmployeeFirstName,@EmployeeLastName,@NameTagDisplayName,@JobTitle, @ShipToAddress,@NameTagType,@Quantity,@Tennure,@UnitCost,@Cost,@Tax,@TotalCost,@IsActive,@CreatedBy,@ModifiedBy,@IsDelete",
                  param.ToArray());
                if (nameTagDetailsDBSet != null)
                {
                    lstNameTagDetails = nameTagDetailsDBSet.ToList();
                }

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag NameTagDbServices, updateNameTagMasterAndDetails", ex);
            }
        }

        public List<OrderHistory> GetProcessedOrderData()
        {
            List<OrderHistory> orderHistory = new List<OrderHistory>();
            try
            {
                var tempDBSet = _context.OrderHistories.FromSqlRaw("dbo.usp_GetOrderHistoryData");
                if (tempDBSet != null)
                {
                    orderHistory = tempDBSet.ToList();
                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog("", "Error in NameTag NameTagDbServices, GetProcessedOrderData", ex);
                return orderHistory;
            }
            return orderHistory;
        }
        public DataTable GetOrderData(List<ProcessedOrders> processedOrders, string siteUrl, string culture)
        {
            try
            {
                List<OrderCost> orderCost = new List<OrderCost>();
                List<OrderProcessInfo> lstOrderProcessInfo = new List<OrderProcessInfo>();
                List<OrderProcessInfo> lstOrderProcessInfoFinal = new List<OrderProcessInfo>();
                List<NameTagMaster> nameTagMasterData = new List<NameTagMaster>();
                List<NameTagMaster> nameTagMasterDataFinal = new List<NameTagMaster>();
                List<NameTagDetailsModel> nameTagDetailsData = new List<NameTagDetailsModel>();
                DataTable dtNameTagMaster = new DataTable();
                DataTable dtNameTagDetails = new DataTable();
                DataTable dtNameTagMasterFinal = new DataTable();
                dtNameTagMasterFinal = Logger.ToDataTable(nameTagMasterData);
                DataTable dtNameTagDetailsFinal = new DataTable();
                dtNameTagDetailsFinal = Logger.ToDataTable(nameTagDetailsData);
                DataTable dtorderCost = new DataTable();
                DataTable dtorderCostFinal = new DataTable();
                dtorderCostFinal = Logger.ToDataTable(orderCost);
                DataSet ds = new DataSet();
                DataTable dtOrderProcessInfo = new DataTable();
                foreach (ProcessedOrders resource in processedOrders)
                {
                    if (resource == null) continue;
                    lstOrderProcessInfo = _context.orderProcessInfos.Where(o => o.OrderProcessID == (resource.ProcessedOrderID)).ToList();
                    foreach (OrderProcessInfo opi in lstOrderProcessInfo)
                        lstOrderProcessInfoFinal.Add(opi);
                    dtOrderProcessInfo = Logger.ToDataTable(lstOrderProcessInfo);
                }
                foreach (OrderProcessInfo resource in lstOrderProcessInfo)
                {
                    if (resource == null) continue;
                    nameTagMasterData = _context.nameTagMaster.Where(o => o.OrderID == (resource.OrderID) && o.IsActive == true//).ToList();
                    && o.DealerCode.Trim() != "0").ToList();
                    foreach (NameTagMaster ntm in nameTagMasterData)
                        nameTagMasterDataFinal.Add(ntm);
                    dtNameTagMaster = Logger.ToDataTable(nameTagMasterData);
                    dtNameTagMasterFinal.Merge(dtNameTagMaster);
                }
                foreach (NameTagMaster resource in nameTagMasterDataFinal)
                {
                    orderCost = _context.NameTagDetailsModel.GroupBy(x => x.OrderID)
                        .Select(g => new OrderCost { OrderID = g.Key, Cost = g.Sum(x => x.TotalCost) }).Where(d => d.OrderID == (resource.OrderID)).ToList();
                    dtorderCost = Logger.ToDataTable(orderCost);
                    dtorderCostFinal.Merge(dtorderCost);
                }
                dtNameTagMasterFinal.Columns.Add("Cost", typeof(String));
                foreach (DataRow dr in dtorderCostFinal.Rows)
                {
                    int orderID = Convert.ToInt32(dr["OrderID"].ToString());
                    string totalCost = dr["Cost"].ToString();
                    foreach (DataRow drMaster in dtNameTagMasterFinal.Rows)
                    {
                        int orderIDMaster = Convert.ToInt32(drMaster["OrderID"].ToString());
                        if (orderID == orderIDMaster)
                        {
                            drMaster["Cost"] = totalCost;
                            dtNameTagMasterFinal.AcceptChanges();
                        }
                    }
                }
                DataTable dtFinal = new DataTable();
                if (dtNameTagMasterFinal.Rows.Count > 0)
                {
                    dtFinal = dtNameTagMasterFinal.AsEnumerable()
                                        .GroupBy(r => r["DealerCode"])
                                        .Select(x =>
                                        {
                                            var row = dtNameTagMasterFinal.NewRow();
                                            row["DealerCode"] = x.Key;
                                            row["Cost"] = x.Sum(r => Convert.ToDecimal(r["Cost"]));
                                            return row;
                                        }).CopyToDataTable();
                }
                return dtFinal;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService, GetOrderData", ex);
                return null;
            }

        }
        public DataTable GetOrderDataDealer(List<ProcessedOrders> processedOrders, string siteUrl, string culture)
        {
            try
            {
                #region Variable declarartion
                List<OrderCost> orderCost = new List<OrderCost>();
                List<OrderProcessInfo> lstOrderProcessInfo = new List<OrderProcessInfo>();
                //List<OrderProcessInfo> lstOrderProcessInfoFinal = new List<OrderProcessInfo>();
                List<NameTagMaster> nameTagMasterData = new List<NameTagMaster>();
                List<NameTagMaster> nameTagMasterDataFinal = new List<NameTagMaster>();
                List<NameTagDetailsModel> nameTagDetailsData = new List<NameTagDetailsModel>();
                List<NameTagDetailsModel> nameTagDetailsDataFinal = new List<NameTagDetailsModel>();
                DataTable dtNameTagMaster = new DataTable();
                DataTable dtNameTagDetails = new DataTable();
                DataTable dtNameTagMasterFinal = new DataTable();
                //dtNameTagMasterFinal = Utility.ToDataTable(nameTagMasterData);
                DataTable dtNameTagDetailsFinal = new DataTable();
                //dtNameTagDetailsFinal = Utility.ToDataTable(nameTagDetailsData);
                DataTable dtorderCost = new DataTable();
                DataTable dtorderCostFinal = new DataTable();
                //dtorderCostFinal = Utility.ToDataTable(orderCost);
                DataSet ds = new DataSet();
                DataTable dtOrderProcessInfo = new DataTable();
                StringBuilder dealerCodes = new StringBuilder();
                DataTable dtDealer = new DataTable();
                #endregion
                #region Get and Merged details and master data
                try
                {
                    foreach (ProcessedOrders resource in processedOrders)
                    {
                        if (resource == null) continue;
                        lstOrderProcessInfo = _context.orderProcessInfos.Where(o => o.OrderProcessID == (resource.ProcessedOrderID)).ToList();
                    }
                    foreach (OrderProcessInfo resource in lstOrderProcessInfo)
                    {
                        if (resource == null) continue;
                        nameTagMasterData = _context.nameTagMaster.Where(o => o.OrderID == (resource.OrderID) && o.IsActive == true
                                                                        && o.DealerCode.Trim() != "0").ToList();
                        foreach (NameTagMaster ntm in nameTagMasterData)
                            nameTagMasterDataFinal.Add(ntm);
                    }
                    //dtNameTagMasterFinal = Utility.ToDataTable(nameTagMasterDataFinal);
                    foreach (NameTagMaster resource in nameTagMasterDataFinal)
                    {
                        if (resource == null) continue;
                        nameTagDetailsData = _context.NameTagDetailsModel.Where(o => o.OrderID == (resource.OrderID) && o.IsActive == true
                                                                                   && o.EmployeeType.Trim().ToLower() == "Dealer".Trim().ToLower()).ToList();
                        foreach (NameTagDetailsModel ntd in nameTagDetailsData)
                            nameTagDetailsDataFinal.Add(ntd);
                    }
                    //dtNameTagDetailsFinal = Utility.ToDataTable(nameTagDetailsDataFinal);
                    dtNameTagDetailsFinal.Columns.Add("DealerCode", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("Region", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("District", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("Name", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("Address", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("DealerCity", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("DealerProvince", typeof(String));
                    dtNameTagDetailsFinal.Columns.Add("DealerPostalCode", typeof(String));
                    foreach (DataRow dr in dtNameTagMasterFinal.Rows)
                    {
                        DataRow lastRow = dtNameTagMasterFinal.Rows[dtNameTagMasterFinal.Rows.Count - 1];
                        int dCode = Convert.ToInt32(dr["DealerCode"].ToString());
                        dealerCodes.Append(dCode);
                        if (dr != lastRow)
                            dealerCodes.Append(",");
                        int oID = Convert.ToInt32(dr["OrderID"].ToString());
                        foreach (DataRow drd in dtNameTagDetailsFinal.Rows)
                        {
                            int oIDD = Convert.ToInt32(drd["OrderID"].ToString());
                            if (oID == oIDD)
                            {
                                drd["DealerCode"] = dCode;
                                dtNameTagDetailsFinal.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService - Get and Merged details and master data , GetOrderDataDealer", ex);
                    return null;
                }
                #endregion
                #region Get Dealer Data
                //Get Dealer data
                try
                {
                    List<DealerModel> lstDealerdetails = new List<DealerModel>();
                    List<SqlParameter> param = new List<SqlParameter> {
                        new SqlParameter{ParameterName="@DealerCode",Value= Convert.ToString(dealerCodes) }};
                    var dealerDetailsDBSet = _context.DealerModels.FromSqlRaw("usp_GetDealerData @DealerCode", param.ToArray()).ToList();

                    if (dealerDetailsDBSet != null)
                    {
                        lstDealerdetails = dealerDetailsDBSet.ToList();
                    }
                    //dtDealer = Utility.ToDataTable(lstDealerdetails);
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService - Getting dealer data, GetOrderDataDealer", ex);
                    return null;
                }
                //Get Dealer data
                #endregion
                #region Final Data
                //Final Data
                try
                {
                    foreach (DataRow dr in dtDealer.Rows)
                    {
                        int dCode = Convert.ToInt32(dr["DealerCode"].ToString());
                        string region = dr["Region"].ToString();
                        string district = dr["District"].ToString();
                        string name = dr["Name"].ToString();
                        string address = dr["Address"].ToString();
                        string dealerCity = dr["DealerCity"].ToString();
                        string dealerProvince = dr["DealerProvince"].ToString();
                        string dealerPostalCode = dr["DealerPostalCode"].ToString();
                        foreach (DataRow drd in dtNameTagDetailsFinal.Rows)
                        {
                            int ddCode = Convert.ToInt32(drd["DealerCode"].ToString());
                            if (dCode == ddCode)
                            {
                                drd["Region"] = region;
                                drd["District"] = district;
                                drd["Name"] = name;
                                drd["Address"] = address;
                                drd["DealerCity"] = dealerCity;
                                drd["DealerProvince"] = dealerProvince;
                                drd["DealerPostalCode"] = dealerPostalCode;
                                dtNameTagDetailsFinal.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService - Creating final data, GetOrderDataDealer", ex);
                    return null;
                }
                //Final Data
                #endregion
                return dtNameTagDetailsFinal;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService, GetOrderDataDealer", ex);
                return null;
            }

        }
        public DataTable GetOrderDataCorporate(List<ProcessedOrders> processedOrders, string siteUrl, string culture)
        {
            try
            {
                #region Variable declarartion
                List<OrderCost> orderCost = new List<OrderCost>();
                List<OrderProcessInfo> lstOrderProcessInfo = new List<OrderProcessInfo>();
                //List<OrderProcessInfo> lstOrderProcessInfoFinal = new List<OrderProcessInfo>();
                List<NameTagMaster> nameTagMasterData = new List<NameTagMaster>();
                List<NameTagMaster> nameTagMasterDataFinal = new List<NameTagMaster>();
                List<NameTagDetailsModel> nameTagDetailsData = new List<NameTagDetailsModel>();
                List<NameTagDetailsModel> nameTagDetailsDataFinal = new List<NameTagDetailsModel>();
                DataTable dtNameTagMaster = new DataTable();
                DataTable dtNameTagDetails = new DataTable();
                DataTable dtNameTagMasterFinal = new DataTable();
                //dtNameTagMasterFinal = Utility.ToDataTable(nameTagMasterData);
                DataTable dtNameTagDetailsFinal = new DataTable();
                //dtNameTagDetailsFinal = Utility.ToDataTable(nameTagDetailsData);
                DataTable dtorderCost = new DataTable();
                DataTable dtorderCostFinal = new DataTable();
                //dtorderCostFinal = Utility.ToDataTable(orderCost);
                DataSet ds = new DataSet();
                DataTable dtOrderProcessInfo = new DataTable();
                StringBuilder dealerCodes = new StringBuilder();
                DataTable dtDealer = new DataTable();
                #endregion
                #region Get and Merged details and master data
                try
                {
                    foreach (ProcessedOrders resource in processedOrders)
                    {
                        if (resource == null) continue;
                        lstOrderProcessInfo = _context.orderProcessInfos.Where(o => o.OrderProcessID == (resource.ProcessedOrderID)).ToList();
                    }
                    foreach (OrderProcessInfo resource in lstOrderProcessInfo)
                    {
                        if (resource == null) continue;
                        nameTagMasterData = _context.nameTagMaster.Where(o => o.OrderID == (resource.OrderID) && o.IsActive == true
                                                                        && o.DealerCode.Trim() == "0").ToList();
                        foreach (NameTagMaster ntm in nameTagMasterData)
                            nameTagMasterDataFinal.Add(ntm);
                    }
                    //dtNameTagMasterFinal = Utility.ToDataTable(nameTagMasterDataFinal);
                    foreach (NameTagMaster resource in nameTagMasterDataFinal)
                    {
                        if (resource == null) continue;
                        nameTagDetailsData = _context.NameTagDetailsModel.Where(o => o.OrderID == (resource.OrderID) && o.IsActive == true
                                                                                   && o.EmployeeType.Trim().ToLower() == "MCI".Trim().ToLower()).ToList();
                        foreach (NameTagDetailsModel ntd in nameTagDetailsData)
                            nameTagDetailsDataFinal.Add(ntd);
                    }
                    //dtNameTagDetailsFinal = Utility.ToDataTable(nameTagDetailsDataFinal);
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService - Get and Merged details and master data , GetOrderDataCorporate", ex);
                    return null;
                }
                #endregion
                return dtNameTagDetailsFinal;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTagDBService, GetOrderDataCorporate", ex);
                return null;
            }

        }
    }


}
