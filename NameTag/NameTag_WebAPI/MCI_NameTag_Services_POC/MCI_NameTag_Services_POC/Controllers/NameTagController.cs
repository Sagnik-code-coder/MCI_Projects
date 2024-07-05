using MCI_NameTag_ModelCore;
using MCI_NameTag_Services_POC.Models;
using MCI_NameTag_Services_POC.Models.Implementation;
using MCI_NameTag_Services_POC.Models.ViewModels;
using MCI_NameTag_UtilityCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Text;
using static MCI_NameTag_ModelCore.GetDealerName;

namespace MCI_NameTag_Services_POC.Controllers
{
    //[CustomAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class NameTagController : ControllerBase
    {
        readonly TDataContext _context; // TDataContext needs to be registered.


        public NameTagController(TDataContext context)
        {
            _context = context;
        }
        [HttpGet("getUserPermissionInfo")]
        public string Get(string userName, string siteUrl)
        {
            try
            {
                //TDataContext.ConnectionString("Test");
                if (string.IsNullOrEmpty(siteUrl))
                    siteUrl = ""; // Provide your default SharePoint site URL

                // Use appropriate authentication method for SharePoint

                // Simulated userDetail for demonstration purposes
                var userDetail = new MCI_NameTag_ModelCore.User { UserName = userName, SiteUrl = siteUrl };

                return JsonConvert.SerializeObject(userDetail);
            }
            catch (Exception ex)
            {
                // Log the error
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, GetUserPermissionInfo", ex);
                return ex.Message;

            }
        }
        
        [HttpGet("GetDealerName")]
        public string GetDealerName(string dealerCode, string siteUrl)
        {
            try
            {
                List<GetDealerName> list = new List<GetDealerName>();
                NameTagDBServices<GetDealerName> obj = new NameTagDBServices<GetDealerName>(_context);
                list = obj.GetDealershipName(dealerCode, siteUrl);
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in Confirmation Email Web API, GetDealerName", ex);
                return ex.Message.ToString();
            }
        }
        [HttpGet("getUsersProfile")]
        public string getUsersProfile(string siteUrl, string culture, string dealerCode)
        {
            List<GetDealerNameAndOrderStatus> listUsers = new List<GetDealerNameAndOrderStatus>();
            List<GetDealerNameAndOrderStatus> nameTagDetailsList = new List<GetDealerNameAndOrderStatus>();

            try
            {
                //listUsers = Permissions.getPermissionInstance().GetUsersProfile2(siteUrl, displayName, dealerName);

                foreach (GetDealerNameAndOrderStatus user in listUsers)
                {
                    NameTagDBServices<GetDealerNameAndOrderStatus> _nameTagDBService = new NameTagDBServices<GetDealerNameAndOrderStatus>(_context);
                    nameTagDetailsList = _nameTagDBService.GetDealershipNameAndStatus(user.EmployeeID, dealerCode, culture, siteUrl);

                    foreach (GetDealerNameAndOrderStatus usr in nameTagDetailsList)
                    {
                        // Your logic here...
                        // MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "WebMethod - LineNo: 65; UserProfile EmployeeID : " + user.EmployeeID, null);
                        // MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "WebMethod - LineNo: 65; UserProfile EmployeeID : " + usr.EmployeeID, null);
                        if (user.EmployeeID == usr.EmployeeID && dealerCode != "0")
                        {
                            user.OrderID = usr.OrderID;
                            user.OrderStatus = usr.OrderStatus;
                            user.DealershipName = usr.DealershipName;
                            user.CreatedOn = usr.CreatedOn;
                        }
                        else if (user.EmployeeID == usr.EmployeeID && dealerCode == "0")
                        {
                            user.OrderID = usr.OrderID;
                            user.OrderStatus = usr.OrderStatus;
                            user.DealershipName = "Mazda Canada";
                            user.CreatedOn = usr.CreatedOn;
                        }
                    }
                }

                return JsonConvert.SerializeObject(listUsers);
            }
            catch (Exception ex)
            {
                // Log the error
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, GetUsersProfile", ex);
                return ex.Message;
            }
        }

        [HttpGet("getConfirmationEmailIDs")]
        public string GetConfirmationEmailIDs(string siteUrl, int orderId)
        {
            try
            {
                List<GetConfirmationEmailID> emailIDs = new List<GetConfirmationEmailID>();

                    NameTagDBServices<GetConfirmationEmailID> _nameTagDBService = new NameTagDBServices<GetConfirmationEmailID>(_context);
                    emailIDs = _nameTagDBService.getConfimrationEmailIDs(siteUrl, orderId); //confusion1
                

                return JsonConvert.SerializeObject(emailIDs);
            }
            catch (Exception ex)
            {
                // Log the error using ASP.NET Core logging
                //_logger.LogError(ex, "Error in Confirmation Email Web API, GetConfirmationEmailIDs");
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in Confirmation Email Web API, GetConfirmationEmailIDs", ex);
                // Return a generic error response
                return "An error occurred while processing the request.";
            }
        }
        [HttpGet("deleteNameTagOrder")]
        public string DeleteNameTagOrder(string siteUrl, int orderId)
        {
            string IsDeleted = "False";
            try
            {
                    NameTagDBServices<NameTagDetails> _nameTagDBService = new NameTagDBServices<NameTagDetails>(_context);
                    _nameTagDBService.deleteNameTagOrder(siteUrl, orderId); // Adjusted method name to PascalCase
                    IsDeleted = "True";
               
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag WebService, DeleteNameTagOrder", ex);
                return "An error occurred while processing the request.";
            }

            return IsDeleted;
        }
        [HttpGet("getNameTagMasterDetails")]
        public string GetNameTagMasterDetails(string orderID, string culture)
        {
            List<NameTagMasterDetailsModel> list = new List<NameTagMasterDetailsModel>();
            try
            {
                    NameTagDBServices<NameTagMasterDetailsModel> _nameTagDBService = new NameTagDBServices<NameTagMasterDetailsModel>(_context);
                    list = _nameTagDBService.getNameTagMasterDetails(orderID, culture);

                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog("GetNameTagMasterDetails", "Error in NameTag WebService, GetNameTagMasterDetails", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpGet("getNameTagOrders")]
        public string GetNameTagOrders(string siteUrl, string culture, string dealerCode)
        {
            List<NameTagOrderHistory> lstNameTagOrderHistory = new List<NameTagOrderHistory>();
            try
            {
                NameTagDBServices<NameTagOrderHistory> _NameTagDBServices = new NameTagDBServices<NameTagOrderHistory>(_context);
                lstNameTagOrderHistory = _NameTagDBServices.getNameTagOrderHistory(siteUrl, culture, dealerCode);
                

                return JsonConvert.SerializeObject(lstNameTagOrderHistory);
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag WebService, GetNameTagOrders", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpGet("getCostFromAppSetting")]
        public IActionResult GetCostFromAppSetting(string siteUrl, string camlQuery)
        {
            string cost = "";
            try
            {
                AppSettings _appSettings = new AppSettings();
                //cost = AppSettings.getAppsettingsValueByQuery(camlQuery, siteUrl).FirstOrDefault();
             

                return Ok(cost);
            }
            catch (Exception ex)
            {
                //MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag WebService, GetCostFromAppSetting", ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        [HttpGet("getDLFromAppSetting")]
        public IActionResult GetDLFromAppSetting(string siteUrl, string camlQuery)
        {
            string cost = "";
            try
            {
                AppSettings _appSettings = new AppSettings();
                //cost = AppSettings.getAppsettingsValueByQuery(camlQuery, siteUrl).FirstOrDefault();
                

                return Ok(cost);
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag WebService, GetCostFromAppSetting", ex);
                return StatusCode(100, "An error occurred while processing the request.");
            }
        }
        [HttpPost("updateNameTagOrders")]
        public void UpdateNameTagOrders(UpdateNameTagOrder obj)
        {
            try
            {
                NameTagDBServices<NameTagOrderHistory> _NameTagDBServices = new NameTagDBServices<NameTagOrderHistory>(_context);
                _NameTagDBServices.updateNameTagOrderHistory(obj.siteUrl, obj.orderStatusID, obj.arrOrderID, obj.arrProcessedOrder, obj.currentUser);
                

            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(obj.siteUrl, "Error in NameTag WebService, UpdateNameTagOrders", ex);
                
            }
        }
        //[HttpPost("insertNameTagDetails")]
        //public void InsertNameTagDetails(string siteUrl, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost, decimal cost, decimal tax, decimal totalCost)
        //{
        //    string orderID = "";
        //    try
        //    {
        //        string currentUser = "Static User(sys)";//User.Identity.Name; /* Obtain the current user in ASP.NET Core */
        //        NameTagDBServices<NameTagDetails> _nameTagDBService = new NameTagDBServices<NameTagDetails>(_context);
        //        orderID = _nameTagDBService.insertNameTagDetails(siteUrl, employeeID, employeeType, employeeFirstName, employeeLastName, nameTagDisplayName, jobTitle, shipToAddress, nameTagType, quantity, tennure, unitCost, cost, tax, totalCost, true, currentUser, "");
                

        //    }
        //    catch (Exception ex)
        //    {
        //        MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, InsertNameTagDetails", ex);
               
        //    }
        //}        //[HttpPost("insertNameTagDetails")]
        //public void InsertNameTagDetails(string siteUrl, string employeeID, string employeeType, string employeeFirstName, string employeeLastName, string nameTagDisplayName, string jobTitle, string shipToAddress, string nameTagType, int quantity, string tennure, decimal unitCost, decimal cost, decimal tax, decimal totalCost)
        //{
        //    string orderID = "";
        //    try
        //    {
        //        string currentUser = "Static User(sys)";//User.Identity.Name; /* Obtain the current user in ASP.NET Core */
        //        NameTagDBServices<NameTagDetails> _nameTagDBService = new NameTagDBServices<NameTagDetails>(_context);
        //        orderID = _nameTagDBService.insertNameTagDetails(siteUrl, employeeID, employeeType, employeeFirstName, employeeLastName, nameTagDisplayName, jobTitle, shipToAddress, nameTagType, quantity, tennure, unitCost, cost, tax, totalCost, true, currentUser, "");
                

        //    }
        //    catch (Exception ex)
        //    {
        //        MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, InsertNameTagDetails", ex);
               
        //    }
        //}
        [HttpPost("insertNameTagMasterAndDetails")]
        public string InsertNameTagMasterAndDetails(string siteUrl, String[] arrNameTagDetails, string dealerCode, string confirmationEmail, string orderDescr, string userType, string currentUser)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("OrderID");
                dt.Columns.Add("EmployeeID");
                dt.Columns.Add("EmployeeType");
                dt.Columns.Add("EmployeeFirstName");
                dt.Columns.Add("EmployeeLastName");
                dt.Columns.Add("NameTagDisplayName");
                dt.Columns.Add("JobTitle");
                dt.Columns.Add("ShipToAddress");
                dt.Columns.Add("NameTagType");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Tennure");
                dt.Columns.Add("UnitCost");
                dt.Columns.Add("Cost");
                dt.Columns.Add("Tax");
                dt.Columns.Add("TotalCost");
                dt.Columns.Add("IsActive");
                dt.Columns.Add("CreatedBy");
                dt.Columns.Add("CreatedOn");
                dt.Columns.Add("ModifiedBy");
                dt.Columns.Add("ModifiedOn");

                foreach (var str in arrNameTagDetails)
                {
                    DataRow row = dt.NewRow();
                    row["OrderID"] = "0";
                    string[] splitted = str.Split(';');
                    row["EmployeeID"] = splitted[0];
                    row["EmployeeType"] = splitted[1];
                    row["EmployeeFirstName"] = splitted[2];
                    row["EmployeeLastName"] = splitted[3];
                    row["NameTagDisplayName"] = splitted[4];
                    row["JobTitle"] = splitted[5];
                    row["ShipToAddress"] = splitted[6];
                    row["NameTagType"] = splitted[7];
                    row["Quantity"] = splitted[9];
                    row["Tennure"] = splitted[10];
                    row["UnitCost"] = splitted[11];
                    row["Cost"] = splitted[12];
                    row["Tax"] = splitted[13];
                    row["TotalCost"] = splitted[14];
                    row["IsActive"] = true;
                    row["CreatedBy"] = currentUser;
                    row["CreatedOn"] = DateTime.Now;
                    row["ModifiedBy"] = "";
                    row["ModifiedOn"] = "";
                    dt.Rows.Add(row);
                }

                NameTagDBServices<NameTagDetails> _NameTagDBServices = new NameTagDBServices<NameTagDetails>(_context);
                _NameTagDBServices.insertNameTagMasterAndDetails(siteUrl, dealerCode, confirmationEmail, orderDescr, userType, currentUser, currentUser, dt);
                

                return "Data inserted successfully";
            }
            catch (Exception ex)
            {
                // Log the error
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, InsertNameTagMasterAndDetails", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpPost("insertUpdateNameTagMasterAndDetails")]
        public string InsertUpdateNameTagMasterAndDetails(string siteUrl, String[] arrNameTagDetails, string dealerCode, string confirmationEmail, string orderDescr, string userType, string currentUser, int orderID)
        {
            try
            {
                string newOrderID = "";
                string isSuccess = "False";

                if (orderID == 0)
                {
                    string orderDetailID = "";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("OrderID");
                    dt.Columns.Add("EmployeeID");
                    dt.Columns.Add("EmployeeType");
                    dt.Columns.Add("EmployeeFirstName");
                    dt.Columns.Add("EmployeeLastName");
                    dt.Columns.Add("NameTagDisplayName");
                    dt.Columns.Add("JobTitle");
                    dt.Columns.Add("ShipToAddress");
                    dt.Columns.Add("NameTagType");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("Tennure");
                    dt.Columns.Add("UnitCost");
                    dt.Columns.Add("Cost");
                    dt.Columns.Add("Tax");
                    dt.Columns.Add("TotalCost");
                    dt.Columns.Add("IsActive");
                    dt.Columns.Add("CreatedBy");
                    dt.Columns.Add("CreatedOn");
                    dt.Columns.Add("ModifiedBy");
                    dt.Columns.Add("ModifiedOn");
                    foreach (string str in arrNameTagDetails)
                    {
                        DataRow row = dt.NewRow();
                        row["OrderID"] = "";
                        row["EmployeeID"] = str.Split(';')[0];
                        row["EmployeeType"] = str.Split(';')[1];
                        row["EmployeeFirstName"] = str.Split(';')[2];
                        row["EmployeeLastName"] = str.Split(';')[3];
                        row["NameTagDisplayName"] = str.Split(';')[4];
                        row["JobTitle"] = str.Split(';')[5];
                        row["ShipToAddress"] = str.Split(';')[6];
                        row["NameTagType"] = str.Split(';')[7];
                        row["Quantity"] = str.Split(';')[9];
                        row["Tennure"] = str.Split(';')[10];
                        row["UnitCost"] = str.Split(';')[11];
                        row["Cost"] = str.Split(';')[12];
                        row["Tax"] = str.Split(';')[13];
                        row["TotalCost"] = str.Split(';')[14];
                        row["IsActive"] = true;
                        row["CreatedBy"] = currentUser;
                        row["CreatedOn"] = System.DateTime.Now;
                        row["ModifiedBy"] = "";
                        row["ModifiedOn"] = "";
                        dt.Rows.Add(row);
                    }


                     NameTagDBServices<NameTagDetails> _NameTagDBServices = new NameTagDBServices<NameTagDetails>(_context);
                     newOrderID = _NameTagDBServices.insertUpdateNameTagMasterAndDetails(siteUrl, dealerCode, confirmationEmail, orderDescr, userType, currentUser, currentUser, orderID, orderDetailID, null, null, null, null, null, null, null, null, null, null, null,
                        null, null, null, true, false, dt);
                        isSuccess = "True";
                    
                }
                else
                {
                    foreach (var str in arrNameTagDetails)
                    {
                        string employeeID = str.Split(';')[0];
                        string employeeType = str.Split(';')[1];
                        string employeeFirstName = str.Split(';')[2];
                        string employeeLastName = str.Split(';')[3];
                        string nameTagDisplayName = str.Split(';')[4];
                        string jobTitle = str.Split(';')[5];
                        string shipToAddress = str.Split(';')[6];
                        string nameTagType = str.Split(';')[7];
                        int quantity = (str.Split(';')[9] != "" ? Int32.Parse(str.Split(';')[9]) : 0);
                        string tennure = str.Split(';')[10];
                        decimal unitCost = (str.Split(';')[11] != "N/A" ? decimal.Parse(str.Split(';')[11]) : 0);
                        decimal cost = (str.Split(';')[12] != "N/A" ? decimal.Parse(str.Split(';')[12]) : 0);
                        decimal tax = (str.Split(';')[13] != "N/A" ? decimal.Parse(str.Split(';')[13]) : 0);
                        decimal totalCost = (str.Split(';')[14] != "N/A" ? decimal.Parse(str.Split(';')[14]) : 0);
                        bool isActive = true;
                        string modifiedBy = currentUser;
                        bool isDelete;
                        if (str.Split(';')[16] == "Deleted")
                        {
                            isDelete = true;
                        }
                        else
                        {
                            isDelete = false;
                        }
                        string orderDetailID = str.Split(';')[15];

                        try
                        {

                               NameTagDBServices<NameTagDetails> _NameTagDBServices = new NameTagDBServices<NameTagDetails>(_context);
                                newOrderID = _NameTagDBServices.insertUpdateNameTagMasterAndDetails(siteUrl, dealerCode, confirmationEmail, orderDescr, userType, currentUser, currentUser, orderID, orderDetailID, employeeID, employeeType, employeeFirstName, employeeLastName, nameTagDisplayName, jobTitle, shipToAddress, nameTagType, quantity, tennure, unitCost,
                            cost, tax, totalCost, isActive, isDelete, null);
                                isSuccess = "True";
                            
                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, UpdateNameTagMasterAndDetails", ex);
                        }
                    }
                }
                return JsonConvert.SerializeObject(new { IsSuccess = isSuccess, NewOrderID = newOrderID });
            }

            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, InsertUpdateNameTagMasterAndDetails", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpPost("updateNameTagMasterAndDetails")]
        public string UpdateNameTagMasterAndDetails(string siteUrl, String[] arrNameTagDetails, string dealerCode, string orderDescr, string userType, int orderID, string currentUser)
        {
            try
            {
                //string currentUser = User.Identity.Name; /* Obtain the current user in ASP.NET Core */

                foreach (var str in arrNameTagDetails)
                {
                    string employeeID = str.Split(';')[0];
                    string employeeType = str.Split(';')[1];
                    string employeeFirstName = str.Split(';')[2];
                    string employeeLastName = str.Split(';')[3];
                    string nameTagDisplayName = str.Split(';')[4];
                    string jobTitle = str.Split(';')[5];
                    string shipToAddress = str.Split(';')[6];
                    string nameTagType = str.Split(';')[7];
                    int quantity = (str.Split(';')[9] != "" ? Int32.Parse(str.Split(';')[9]) : 0);
                    string tennure = str.Split(';')[10];
                    decimal unitCost = (str.Split(';')[11] != "N/A" ? decimal.Parse(str.Split(';')[11]) : 0);
                    decimal cost = (str.Split(';')[12] != "N/A" ? decimal.Parse(str.Split(';')[12]) : 0);
                    decimal tax = (str.Split(';')[13] != "N/A" ? decimal.Parse(str.Split(';')[13]) : 0);
                    decimal totalCost = (str.Split(';')[14] != "N/A" ? decimal.Parse(str.Split(';')[14]) : 0);
                    bool isActive = true;
                    string modifiedBy = currentUser;
                    bool isDelete;
                    if (str.Split(';')[16] == "Deleted")
                    {
                        isDelete = true;
                    }
                    else
                    {
                        isDelete = false;
                    }
                    string orderDetailID = str.Split(';')[15];

                    NameTagDBServices<NameTagDetails> _nameTagDBService = new NameTagDBServices<NameTagDetails>(_context);
                    _nameTagDBService.updateNameTagMasterAndDetails(siteUrl, orderID, orderDetailID, dealerCode, orderDescr, userType, employeeID, employeeType, employeeFirstName, employeeLastName, nameTagDisplayName, jobTitle, shipToAddress, nameTagType, quantity, tennure, unitCost,
               cost, tax, totalCost, isActive, modifiedBy, isDelete);
                    
                }

                return "Update successful";
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, UpdateNameTagMasterAndDetails", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpPost("downloadFinanceFile")]
        public string DownloadFinanceFile(string objProcessedOrderID, string siteUrl, string culture)
        {
            try
            {
                List<ProcessedOrders> result = JsonConvert.DeserializeObject<List<ProcessedOrders>>(objProcessedOrderID);
                List<ProcessedOrders> filterObjects = new List<ProcessedOrders>();
                foreach (ProcessedOrders resource in result)
                {
                    if (resource == null) continue;
                    filterObjects.Add(resource);
                }
                DataTable ds;
                NameTagDBServices<OrderHistory> _nts = new NameTagDBServices<OrderHistory>(_context);
                ds = _nts.GetOrderData(filterObjects, siteUrl, culture.Trim());
                
                //string fileName = Utility.ReadFromExcel("", ds, siteUrl);
                //return Ok(fileName);
                return "Successfully processed the request.";
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag WebService, DownloadFinanceFile", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpPost("deleteExcel")]
        public string DeleteExcel(string siteUrl, string docName)
        {
            try
            {
                string userName = "YourUsername@YourDomain.com";
                string password = "YourPassword";

                using (ClientContext context = new ClientContext(siteUrl))
                {
                    SecureString securePass = new SecureString();
                    foreach (char c in password)
                    {
                        securePass.AppendChar(c);
                    }

                    //context.Credentials = new SharePointOnlineCredentials(userName, securePass);

                    Web web = context.Web;
                    List documentsList = web.Lists.GetByTitle("Documents"); // Change this to your document library name

                    CamlQuery query = new CamlQuery();
                    query.ViewXml = $@"<View><Query><Where><Eq><FieldRef Name='FileLeafRef'/><Value Type='Text'>{docName}</Value></Eq></Where></Query></View>";

                    ListItemCollection items = documentsList.GetItems(query);
                    context.Load(items);
                    context.ExecuteQuery();

                    if (items != null && items.Count == 1)
                    {
                        items[0].DeleteObject();
                        context.ExecuteQuery();

                        //MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "File Deleted, DeleteExcel - " + docName);

                        return "File deleted successfully.";
                    }
                    else
                    {
                        return "File not found or multiple files found with the same name.";
                    }
                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, DeleteExcel", ex);
                return "An error occurred while processing the request.";
            }
        }
        [HttpPost("sendEmail")]
        public string SendEmail(string mySelection, string siteUrl, string culture, string email, string orderID, string createdOn, string type, string subFieldEn, string subFieldFr, string bodyFieldEn, string bodyFieldFr, string status)
        {
            string currentUser = User.Identity.Name; ;
            string result = string.Empty;
            int totQty = 0;
            decimal totalCost = 0;
            try
            {
                //string filePath = Utility.writeToPdfEmail(mySearchSelection, MyHtmlTable, siteUrl, culture, dealerName, Model);
                //result = SendEmailWithAttachment(siteUrl, email, culture, filePath);
                List<NameTagDetailsModel> selection = JsonConvert.DeserializeObject<List<NameTagDetailsModel>>(mySelection);

                List<NameTagDetailsModel> filterObjects = new List<NameTagDetailsModel>();
                StringBuilder sb = new StringBuilder();
                foreach (NameTagDetailsModel resource in selection)
                {
                    if (resource == null) continue;
                    decimal subTotal = Convert.ToDecimal(resource.Quantity * resource.Cost);
                    sb.Append("<tr>");
                    sb.Append("<td style='border: 1px solid; '>" + type + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.EmployeeFirstName + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.NameTagDisplayName + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.JobTitle + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.Tennure + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.Cost + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + resource.Quantity + "</td>");
                    //sb.Append("<td style='border: 1px solid; '>" + resource.Cost + "</td>");
                    sb.Append("<td style='border: 1px solid; '>" + subTotal.ToString() + "</td>");
                    //sb.Append("<td style='border: 1px solid; '>" + resource.Tax + "</td>");
                    //sb.Append("<td style='border: 1px solid; '>" + resource.TotalCost + "</td>");
                    totQty = totQty + Convert.ToInt16(resource.Quantity);
                    totalCost = totalCost + Convert.ToDecimal(resource.Cost);
                    //filterObjects.Add(resource);
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td style='border: 1px solid; '></td>");
                sb.Append("<td style='border: 1px solid; '></td>");
                sb.Append("<td style='border: 1px solid; '></td>");
                sb.Append("<td style='border: 1px solid; '></td>");
                sb.Append("<td style='border: 1px solid; '></td>");
                sb.Append("<td style='border: 1px solid; '>Total:</td>");
                sb.Append("<td style='border: 1px solid; '>" + totQty.ToString() + "</td>");
                sb.Append("<td style='border: 1px solid; '>" + totalCost.ToString() + "</td>");
                sb.Append("</tr>");
                string body = string.Empty;
                string subAppsettingName = culture == "en-US" ? subFieldEn : subFieldFr;
                //string subject = string.Format((AppSettings.getAppsettingsValue(subAppsettingName, siteUrl) ?? "Email NameTag System"), orderID);
                string bodyAppsettingName = culture == "en-US" ? bodyFieldEn : bodyFieldFr;
                //string body = string.Format((AppSettings.getAppsettingsValue(bodyAppsettingName, siteUrl)), totalQuotePiece, totalQuoteLaborhour, quoteMSRP, quoteMSRPP, sb.ToString());
                if (status == "Submitted")
                {
                    //body = string.Format((AppSettings.getAppsettingsValue(bodyAppsettingName, siteUrl)), currentUser.Split(' ')[0], createdOn, sb.ToString());
                }
                else if (status == "Confirmed")
                {
                    //body = string.Format((AppSettings.getAppsettingsValue(bodyAppsettingName, siteUrl)), currentUser.Split(' ')[0], createdOn, sb.ToString(), status, "Green");
                }
                else
                {
                    //body = string.Format((AppSettings.getAppsettingsValue(bodyAppsettingName, siteUrl)), currentUser.Split(' ')[0], createdOn, sb.ToString(), status, "Red");
                }
                StringBuilder toAddresses = new StringBuilder();
                if (!string.IsNullOrEmpty(email))
                {
                    toAddresses.Append(email);
                    toAddresses.Append(", ");
                }
                //Utility.sendEmail(subject, body, siteUrl, email, "");
                //Utility.sendEmail(subject, body, siteUrl, toAddresses.ToString(), "");
                result = "success";
            }
            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(siteUrl, "Error in NameTag Web API, SendEmail", ex);
                return null;
            }
            return result;
        }
        [HttpGet("get-processed-order-data")]
        public string GetProcessedOrderData()
        {
            try
            {
                NameTagDBServices<OrderHistory> _nts = new NameTagDBServices<OrderHistory>(_context);
                var orderHistory = _nts.GetProcessedOrderData();
                return JsonConvert.SerializeObject(new { data = orderHistory });
            }
            catch (Exception ex)
            {
                return "An error occurred while processing the request.";
            }
        }
        
[HttpPost("GetOrderData")]
 public string GetOrderData(List<ProcessedOrders> processedOrders, string siteUrl, string culture)

        {

            try

            {

                List<OrderCost> orderCost = new List<OrderCost>();

                List<OrderProcessInfo> lstOrderProcessInfo = new List<OrderProcessInfo>();

                List<NameTagMaster> nameTagMasterData = new List<NameTagMaster>();

                List<NameTagMaster> nameTagMasterDataFinal = new List<NameTagMaster>();

                List<NameTagDetailsModel> nameTagDetailsData = new List<NameTagDetailsModel>();


                foreach (ProcessedOrders resource in processedOrders)

                {

                    if (resource == null) continue;

                    lstOrderProcessInfo = _context.orderProcessInfos

                        .Where(o => o.OrderProcessID == resource.ProcessedOrderID)

                        .ToList();

                }

                foreach (OrderProcessInfo resource in lstOrderProcessInfo)

                {

                    if (resource == null) continue;

                    nameTagMasterData = _context.nameTagMaster

                        .Where(o => o.OrderID == resource.OrderID && o.IsActive == true && o.DealerCode.Trim() != "0")

                        .ToList();

                    nameTagMasterDataFinal.AddRange(nameTagMasterData);

                }

                foreach (NameTagMaster resource in nameTagMasterDataFinal)

                {

                    orderCost = _context.NameTagDetailsModel

                        .GroupBy(x => x.OrderID)

                        .Select(g => new OrderCost { OrderID = g.Key, Cost = g.Sum(x => x.TotalCost) })

                        .Where(d => d.OrderID == resource.OrderID)

                        .ToList();

                }

                return JsonConvert.SerializeObject(nameTagMasterDataFinal);

            }

            catch (Exception ex)

            {

                MCINameTagLogger.GetMCINameTagLogger().WriteLog("", "Error in NameTag Web API, GetOrderData", ex);

                return ex.Message;

            }

        }

        [HttpGet("GetDealershipNameAndStatus")]
        public string GetDealershipNameAndStatus(string EmployeeID, string culture, string dealerCode)
        {
            
            try
            {
                List<GetDealerNameAndOrderStatuses> nameTagDetailsList = new List<GetDealerNameAndOrderStatuses>();

                NameTagDBServices<GetDealerNameAndOrderStatuses> _nameTagDBService = new NameTagDBServices<GetDealerNameAndOrderStatuses>(_context);
                nameTagDetailsList = _nameTagDBService.GetDealershipNameAndStatuses(EmployeeID, dealerCode, culture, "");

                return JsonConvert.SerializeObject(nameTagDetailsList);
            }
            catch (Exception ex)
            {
                // Log the error
                MCINameTagLogger.GetMCINameTagLogger().WriteLog("", "Error in NameTag Web API, GetDealershipNameAndStatus", ex);
                return ex.Message;
            }
        }


        #region Parameters
        [HttpPost("insertUpdateNameTagMasterAndDetails_New")]
        public string InsertUpdateNameTagMasterAndDetails_New(InsertUpdateNameTagMasterAndDetailsModel obj)
        {
            try
            {
                string newOrderID = "";
                string isSuccess = "False";

                if (obj.orderID == 0)
                {
                    string orderDetailID = "";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("OrderID");
                    dt.Columns.Add("EmployeeID");
                    dt.Columns.Add("EmployeeType");
                    dt.Columns.Add("EmployeeFirstName");
                    dt.Columns.Add("EmployeeLastName");
                    dt.Columns.Add("NameTagDisplayName");
                    dt.Columns.Add("JobTitle");
                    dt.Columns.Add("ShipToAddress");
                    dt.Columns.Add("NameTagType");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("Tennure");
                    dt.Columns.Add("UnitCost");
                    dt.Columns.Add("Cost");
                    dt.Columns.Add("Tax");
                    dt.Columns.Add("TotalCost");
                    dt.Columns.Add("IsActive");
                    dt.Columns.Add("CreatedBy");
                    dt.Columns.Add("CreatedOn");
                    dt.Columns.Add("ModifiedBy");
                    dt.Columns.Add("ModifiedOn");
                    foreach (string str in obj.arrNameTagDetails)
                    {
                        DataRow row = dt.NewRow();
                        row["OrderID"] = "";
                        row["EmployeeID"] = str.Split(';')[0];
                        row["EmployeeType"] = str.Split(';')[1];
                        row["EmployeeFirstName"] = str.Split(';')[2];
                        row["EmployeeLastName"] = str.Split(';')[3];
                        row["NameTagDisplayName"] = str.Split(';')[4];
                        row["JobTitle"] = str.Split(';')[5];
                        row["ShipToAddress"] = str.Split(';')[6];
                        row["NameTagType"] = str.Split(';')[7];
                        row["Quantity"] = str.Split(';')[9];
                        row["Tennure"] = str.Split(';')[10];
                        row["UnitCost"] = str.Split(';')[11];
                        row["Cost"] = str.Split(';')[12];
                        row["Tax"] = str.Split(';')[13];
                        row["TotalCost"] = str.Split(';')[14];
                        row["IsActive"] = true;
                        row["CreatedBy"] = obj.currentUser;
                        row["CreatedOn"] = System.DateTime.Now;
                        row["ModifiedBy"] = "";
                        row["ModifiedOn"] = "";
                        dt.Rows.Add(row);
                    }


                    NameTagDBServices<NameTagDetails> _NameTagDBServices = new NameTagDBServices<NameTagDetails>(_context);
                    newOrderID = _NameTagDBServices.insertUpdateNameTagMasterAndDetails(obj.siteUrl, obj.dealerCode, obj.confirmationEmail, obj.orderDescr, obj.userType, obj.currentUser, obj.currentUser, obj.orderID, orderDetailID, null, null, null, null, null, null, null, null, null, null, null,
                       null, null, null, true, false, dt);
                    isSuccess = "True";

                }
                else
                {
                    foreach (var str in obj.arrNameTagDetails)
                    {
                        string employeeID = str.Split(';')[0];
                        string employeeType = str.Split(';')[1];
                        string employeeFirstName = str.Split(';')[2];
                        string employeeLastName = str.Split(';')[3];
                        string nameTagDisplayName = str.Split(';')[4];
                        string jobTitle = str.Split(';')[5];
                        string shipToAddress = str.Split(';')[6];
                        string nameTagType = str.Split(';')[7];
                        int quantity = (str.Split(';')[9] != "" ? Int32.Parse(str.Split(';')[9]) : 0);
                        string tennure = str.Split(';')[10];
                        decimal unitCost = (str.Split(';')[11] != "N/A" ? decimal.Parse(str.Split(';')[11]) : 0);
                        decimal cost = (str.Split(';')[12] != "N/A" ? decimal.Parse(str.Split(';')[12]) : 0);
                        decimal tax = (str.Split(';')[13] != "N/A" ? decimal.Parse(str.Split(';')[13]) : 0);
                        decimal totalCost = (str.Split(';')[14] != "N/A" ? decimal.Parse(str.Split(';')[14]) : 0);
                        bool isActive = true;
                        string modifiedBy = obj.currentUser;
                        bool isDelete;
                        if (str.Split(';')[16] == "Deleted")
                        {
                            isDelete = true;
                        }
                        else
                        {
                            isDelete = false;
                        }
                        string orderDetailID = str.Split(';')[15];

                        try
                        {

                            NameTagDBServices<NameTagDetails> _NameTagDBServices = new NameTagDBServices<NameTagDetails>(_context);
                            newOrderID = _NameTagDBServices.insertUpdateNameTagMasterAndDetails(obj.siteUrl, obj.dealerCode, obj.confirmationEmail, obj.orderDescr, obj.userType, obj.currentUser, obj.currentUser, obj.orderID, orderDetailID, employeeID, employeeType, employeeFirstName, employeeLastName, nameTagDisplayName, jobTitle, shipToAddress, nameTagType, quantity, tennure, unitCost,
                        cost, tax, totalCost, isActive, isDelete, null);
                            isSuccess = "True";

                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.GetMCINameTagLogger().WriteLog(obj.siteUrl, "Error in NameTag Web API, UpdateNameTagMasterAndDetails", ex);
                        }
                    }
                }
                return JsonConvert.SerializeObject(new { IsSuccess = isSuccess, NewOrderID = newOrderID });
            }

            catch (Exception ex)
            {
                MCINameTagLogger.GetMCINameTagLogger().WriteLog(obj.siteUrl, "Error in NameTag Web API, InsertUpdateNameTagMasterAndDetails", ex);
                return "An error occurred while processing the request.";
            }
        }
        //[HttpGet("getNameTagOrders_New")]
        //public string GetNameTagOrders_New([FromQuery]GetNameTagOrders obj)
        //{
        //    List<NameTagOrderHistory> lstNameTagOrderHistory = new List<NameTagOrderHistory>();
        //    try
        //    {
        //        NameTagDBServices<NameTagOrderHistory> _NameTagDBServices = new NameTagDBServices<NameTagOrderHistory>(_context);
        //        lstNameTagOrderHistory = _NameTagDBServices.getNameTagOrderHistory(obj.siteUrl, obj.culture, obj.dealerCode);


        //        return JsonConvert.SerializeObject(new { data = lstNameTagOrderHistory });
        //    }
        //    catch (Exception ex)
        //    {
        //        MCINameTagLogger.GetMCINameTagLogger().WriteLog(obj.siteUrl, "Error in NameTag WebService, GetNameTagOrders", ex);
        //        return "An error occurred while processing the request.";
        //    }
        //}

        #endregion
    }
}
