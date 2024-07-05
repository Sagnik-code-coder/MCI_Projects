using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_NameTag_Utility;
//using System.Net;
using Microsoft.SharePoint.Client;
using PnP.Framework;

namespace NameTag_WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }

        public static void Execute(Guid targetInstanceId)
        {
            int OrderProcessedID = 0;
            try
            {
                DateTime date = DateTime.Now;
                MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Orders Begins", null);
                if (date.Day.Equals(1) || date.Day.Equals(16))
                {
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Orders 1st or 16th of the month", null);
                    ClientContext cc = new AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                    //SPWebApplication webApp = this.Parent as SPWebApplication;
                    //SPWeb web = webApp.Sites[0].OpenWeb("NameTag");
                    Web web = cc.Web;
                    string connStr = AppSettings.getAppsettingsValue("TIMERJOBCONN", web.Url);
                    string subject = AppSettings.getAppsettingsValue("AutoProcessSubject", web.Url);
                    string body = AppSettings.getAppsettingsValue("AutoProcessBody", web.Url);
                    string toEmail = AppSettings.getAppsettingsValue("AutoProcessToEmail", web.Url);
                    //logpath = AppSettings.getAppsettingsValue("TIMERJOBLOGPATH", web.Url);
                    DataSet dsApprovedTickets = new DataSet();
                    DataTable dtApprovedTickets = new DataTable();
                    #region order processing
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        try
                        {
                            SqlCommand objSqlCommandUpdate = new SqlCommand("usp_AutoUpdateNameTagOrder", con);
                            objSqlCommandUpdate.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            SqlParameter RuturnValue = new SqlParameter("@OrderProcessedID", SqlDbType.Int);
                            RuturnValue.Direction = ParameterDirection.Output;
                            objSqlCommandUpdate.Parameters.Add(RuturnValue);
                            objSqlCommandUpdate.ExecuteNonQuery();
                            OrderProcessedID = (int)objSqlCommandUpdate.Parameters["@OrderProcessedID"].Value;
                            objSqlCommandUpdate.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Order processing order error", ex);
                        }

                    }
                    #endregion
                    #region finance file
                    DataSet dsFinance = new DataSet();
                    DataTable dtFinance = new DataTable();
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        try
                        {
                            SqlCommand objSqlCommandFinance = new SqlCommand("usp_FinanceData", con);
                            objSqlCommandFinance.CommandType = CommandType.StoredProcedure;
                            objSqlCommandFinance.Parameters.AddWithValue("OrderProcessID", OrderProcessedID);
                            con.Open();
                            SqlDataAdapter objSqlDataAdapterFinance = new SqlDataAdapter(objSqlCommandFinance);
                            objSqlDataAdapterFinance.Fill(dsFinance);
                            dtFinance = dsFinance.Tables[0];
                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Order getting finance data error", ex);
                        }
                    }
                    string financeFilePath = Utility.ReadFromExcel("", dtFinance, web.Url, true, true);
                    #endregion
                    #region dealer file
                    DataSet dsDealer = new DataSet();
                    DataTable dtDealer = new DataTable();
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        try
                        {
                            SqlCommand objSqlCommandDealer = new SqlCommand("usp_DealerData", con);
                            objSqlCommandDealer.CommandType = CommandType.StoredProcedure;
                            objSqlCommandDealer.Parameters.AddWithValue("OrderProcessID", OrderProcessedID);
                            con.Open();
                            SqlDataAdapter objSqlDataAdapterDealer = new SqlDataAdapter(objSqlCommandDealer);
                            objSqlDataAdapterDealer.Fill(dsDealer);
                            dtDealer = dsDealer.Tables[0];
                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Order getting dealer data error", ex);
                        }
                    }
                    string dealerFilePath = Utility.ReadFromExcelDealer("", dtDealer, web.Url, true, true);
                    #endregion
                    #region corporate file
                    DataSet dsCorporate = new DataSet();
                    DataTable dtCorporate = new DataTable();
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        try
                        {
                            SqlCommand objSqlCommandCorporate = new SqlCommand("up_CorporateData", con);
                            objSqlCommandCorporate.CommandType = CommandType.StoredProcedure;
                            objSqlCommandCorporate.Parameters.AddWithValue("OrderProcessID", OrderProcessedID);
                            con.Open();
                            SqlDataAdapter objSqlDataAdapterCorporate = new SqlDataAdapter(objSqlCommandCorporate);
                            objSqlDataAdapterCorporate.Fill(dsCorporate);
                            dtCorporate = dsCorporate.Tables[0];
                        }
                        catch (Exception ex)
                        {
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Order getting corporate data error", ex);
                        }
                    }
                    string corporateFilePath = Utility.ReadFromExcelCorporate("", dtCorporate, web.Url, true, true);
                    #endregion
                    if (financeFilePath != "" || dealerFilePath != "" || corporateFilePath != "")
                    {
                        Utility.sendEmailWithAttachment(string.Format(subject, DateTime.Now.ToShortDateString()), body, web.Url, toEmail, financeFilePath, dealerFilePath, corporateFilePath);
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Order process successful", null);
                    }
                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "AutoProcess NameTag Orders Ends", null);
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(string.Empty, "Error in AutoProcessNameTagOrders, Execute()", ex);
            }

        }
    }
}
