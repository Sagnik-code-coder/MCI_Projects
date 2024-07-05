using System;
using System.Web;
//using Microsoft.Office.Server.UserProfiles;
//using Microsoft.SharePoint;
//using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MCI_NameTag_Model;
using System.Data;
using System.IO;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.OleDb;
//using OfficeOpenXml;
//using Newtonsoft.Json;
using System.Net.Mail;
using System.Reflection;
using System.Net;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using OfficeOpenXml;

namespace MCI_NameTag_Utility
{
    public static class Utility
    {
        static string excelFilePath = null;
        static string excelFilePathFinal = null;
        static User user = null;

        public static void sendEmail(string subject, string body, string url, string toEmail, string attachmentPath)
        {
            //Attachment
            System.Net.Mail.Attachment attachment = null;
            String fileName = null;
            FileInfo fileExist = null;
            ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
            url = cc.Web.Url;
            //fileExist = new FileInfo(attachmentPath);
            //if (!fileExist.Exists)
            MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "File Not Found in Physical Path", null);
            try
            {
                toEmail = (!string.IsNullOrEmpty(toEmail)) ? toEmail.Substring(0, toEmail.Length - 2) : toEmail;
                string fromEmail = AppSettings.getAppsettingsValue("FromEmail", url);
                string toAddresses = string.Empty;
                string fromAddressDisplayName = AppSettings.getAppsettingsValue("FromDisplayName", url);
                //Changed due to testing instaging
                toAddresses = AppSettings.getAppsettingsValue("ToEmail", url);
                toAddresses = (!string.IsNullOrEmpty(toAddresses)) ? toAddresses : toEmail;
                string CcEmail = AppSettings.getAppsettingsValue("CcEmail", url);
                int smtpPort = 0;
                int.TryParse((AppSettings.getAppsettingsValue("SMTPPort", url) ?? "").ToString(), out smtpPort);
                string smtpServer = AppSettings.getAppsettingsValue("SMTPServer", url);
                
                Site oSPSite = cc.Site;
                Web oSPWeb = cc.Web;
                if (string.IsNullOrEmpty(smtpServer) || (string.IsNullOrEmpty(fromEmail)))
                {
                    try
                    {
                        smtpServer = oSPSite.ServerRelativeUrl;
                        smtpPort = 25;//oSPSite.WebApplication.OutboundMailPort;
                        fromEmail = "";// oSPSite.WebApplication.OutboundMailSenderAddress;
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(string subject, string body, string toEmail , string url ), MCI_NameTag_Utility", ex);
                    }

                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "ToEmail is : " + toEmail);
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Before sendEmail ");
                if (!string.IsNullOrEmpty(smtpServer) && !string.IsNullOrEmpty(fromEmail))
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail, fromAddressDisplayName);
                    mail.IsBodyHtml = true;
                    mail.To.Add(toAddresses);
                    mail.CC.Add(CcEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    //try
                    //{
                    //    attachment = new Attachment(attachmentPath);
                    //    attachment.Name = "Final Maintainance Pricing Quote.pdf";// What should be the name of attachment.
                    //    mail.Attachments.Add(attachment);
                    //}
                    //catch (Exception ex)
                    //{
                    ////MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    //}
                    SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                    smtp.UseDefaultCredentials = true;
                    smtp.Send(mail);
                    //attachment.Dispose();
                }

                else
                {
                    var email = new EmailProperties();
                    email.CC = new List<string> { CcEmail };
                    email.To=new List<string> { toEmail };
                    email.From=fromEmail;
                    email.Subject = subject;
                    email.Body = body;
                    //StringDictionary headers = new StringDictionary();
                    //headers.Add("to", toAddresses);
                    //headers.Add("subject", subject);
                    //headers.Add("fAppendHtmlTag", "True"); // To enable HTML format
                    Microsoft.SharePoint.Client.Utilities.Utility.SendEmail(cc,email);
                    cc.ExecuteQuery();
                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "After sendEmail ");
                        

            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(), MCI_NameTag_Utility", ex);
            }
            finally
            {
                //fileExist.Delete();
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Deleted File From Local Drive  in sendEmail() is completed");
            }

        }
        public static string ReadFromExcel(string path, DataTable dtData, string siteUrl, bool hasHeader = true, bool job = false, bool manual = false)
        {
            if(dtData.Rows.Count>0)
            {
                string stringLog = "";
                try
                {
                    path = downLoadFile(siteUrl, "FILENAMEFINANCE");
                    
                    stringLog = "1.1, ";
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    stringLog = stringLog + "1.2, ";
                    using (var excelPack = new ExcelPackage())
                    {
                        stringLog = stringLog + "1.3, ";
                        //Load excel stream
                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            stringLog = stringLog + "1.4, ";
                            excelPack.Load(stream);
                        }
                        stringLog = stringLog + "1.5, ";
                        //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                        var ws = excelPack.Workbook.Worksheets[6];
                        stringLog = stringLog + "1.6, ";
                        //Get all details as DataTable -because Datatable make life easy :)
                        DataTable excelasTable = new DataTable();
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            stringLog = stringLog + "1.7, ";
                            //Get colummn details
                            if (!string.IsNullOrEmpty(firstRowCell.Text))
                            {
                                stringLog = stringLog + "1.8, ";
                                string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                                stringLog = stringLog + "1.9, ";
                                excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                            }
                        }
                        stringLog = stringLog + "1.10, ";
                        var startRow = hasHeader ? 4 : 1;
                        stringLog = stringLog + "1.11, ";
                        int i = 0;
                        //Get row details
                        //for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        if (dtData.Rows.Count > 0)
                        {
                            stringLog = stringLog + "1.12, ";
                            for (int rowNum = startRow; rowNum <= (startRow + dtData.Rows.Count - 1); rowNum++)
                            {
                                stringLog = stringLog + "1.13, ";
                                var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                                stringLog = stringLog + "1.14, ";
                                DataRow row = excelasTable.Rows.Add();
                                stringLog = stringLog + "1.15, ";
                                DataRow drData = dtData.Rows[i];
                                stringLog = stringLog + "1.16, ";
                                int dCode = Convert.ToInt32(drData["DealerCode"].ToString());
                                int ordNum = Convert.ToInt32(drData["OrderID"]);
                                stringLog = stringLog + "1.17, ";
                                string cost = drData["Cost"].ToString();
                                stringLog = stringLog + "1.18, ";
                                foreach (var cell in wsRow)
                                {
                                    stringLog = stringLog + "1.19, ";
                                    row[cell.Start.Column - 1] = cell.Text;
                                    stringLog = stringLog + "1.20, ";
                                    ws.Cells[rowNum, 3].Value = dCode;
                                    stringLog = stringLog + "1.21, ";
                                    ws.Cells[rowNum, 4].Value = Convert.ToDecimal(cost);
                                    stringLog = stringLog + "1.22, ";
                                    ws.Cells[rowNum, 7].Value = "Dealer Nametags";
                                    stringLog = stringLog + "1.23, ";
                                    ws.Cells[rowNum, 8].Value = "";
                                    stringLog = stringLog + "1.24, ";
                                    ws.Cells[rowNum, 9].Value = 636000;
                                    stringLog = stringLog + "1.25, ";
                                    ws.Cells[rowNum, 10].Value = 25467;
                                    stringLog = stringLog + "1.26, ";
                                    ws.Cells[rowNum, 11].Value = "";
                                    stringLog = stringLog + "1.27, ";
                                    // ws.Cells[rowNum, 19].Value = Convert.ToInt32(drData["OrderID"]);
                                    ws.Cells[rowNum, 19].Value = ordNum;
                                }
                                i++;
                            }
                        }
                        //excelPack.Save();
                        user = null;
                        ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                        Site spSite = cc.Site;
                        Web spWeb = cc.Web;
                        cc.Load(spWeb.CurrentUser);
                        cc.ExecuteQuery();
                        
                        stringLog = stringLog + "1.29, ";
                        user = spWeb.CurrentUser;

                        stringLog = stringLog + "1.30, ";
                        string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        stringLog = stringLog + "1.31, ";
                        string pathDownload = System.IO.Path.Combine(pathUser, "Downloads\\");
                        stringLog = stringLog + "1.32, ";
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                        stringLog = stringLog + "1.33, ";
                        if (job)
                        {
                            stringLog = stringLog + "1.34, ";
                            fileName = "Finance" + "_" + "AutoProcess" + ".xlsx";
                        }
                        else
                        {
                            stringLog = stringLog + "1.35, ";
                            fileName = fileName + "_" + user.Id + ".xlsx";
                        }
                        stringLog = stringLog + "1.36, ";
                        string p_strPath = System.IO.Path.Combine(pathDownload, fileName);// pathDownload + "Finance_File.xlsx";//@"C:\DEV\Finance1.xlsx";
                        stringLog = stringLog + "1.37, ";
                        if (System.IO.File.Exists(p_strPath))
                        {
                            stringLog = stringLog + "1.38, ";
                            System.IO.File.Delete(p_strPath);
                        }
                        stringLog = stringLog + "1.39, ";

                        // Create excel file on physical disk 
                        FileStream objFileStrm = System.IO.File.Create(p_strPath);
                        stringLog = stringLog + "1.40, ";
                        objFileStrm.Close();
                        stringLog = stringLog + "1.41, ";
                        // Write content to excel file 
                        System.IO.File.WriteAllBytes(p_strPath, excelPack.GetAsByteArray());
                        stringLog = stringLog + "1.42, ";
                        //Close Excel package
                        excelPack.Dispose();
                        stringLog = stringLog + "1.43, ";
                        //Get everything as generics and let end user decides on casting to required type
                        //var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                        //return (T)Convert.ChangeType(generatedType, typeof(T));
                        //Upload File to Site Assets
                        if (job || manual)
                        {
                            stringLog = stringLog + "1.44.1, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            excelFilePathFinal = p_strPath;
                            stringLog = stringLog + "1.45, ";
                            FileInfo fileExist = new FileInfo(path);
                            stringLog = stringLog + "1.46, ";
                            fileExist.Delete();
                            stringLog = stringLog + "1.47, ";
                        }
                        else
                        {
                            stringLog = stringLog + "1.44.2, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            excelFilePathFinal = UplaodFileToPageLibrary(p_strPath, siteUrl, user.Id, "F");
                        }
                        stringLog = stringLog + "1.48, ";
                    }
                    stringLog = stringLog + "1.49, ";
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "StringLog: " + stringLog, null);
                    return excelFilePathFinal;
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Error in Utility, ReadFromExcel", ex);
                    return string.Empty;
                }
            }
            else
            {
                excelFilePath = "";
                return excelFilePath;
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string downLoadFile(string strSiteUrl, string templateKey)
        {
            try
            {
                string directory = AppSettings.getAppsettingsValue("SITEDIRECTORY", strSiteUrl);
                strSiteUrl = AppSettings.getAppsettingsValue("SITEURLEXCEL", strSiteUrl);
                string templateFileName = AppSettings.getAppsettingsValue(templateKey, strSiteUrl);
                ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                //url = cc.Web.Url;
                Site site = cc.Site;
                Web web = cc.Web;

                cc.Load(web);
                cc.ExecuteQuery();
                user = web.CurrentUser;
                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = System.IO.Path.Combine(pathUser, "Downloads\\");
                List docLib = web.Lists.GetByTitle(directory); //web.Lists["Site Assets"];
                cc.Load(docLib);
                cc.ExecuteQuery();
                // loop through each item or document in the document library 
                ListItemCollection items = docLib.GetItems(CamlQuery.CreateAllItemsQuery());
                cc.Load(items);
                cc.ExecuteQuery();
                foreach (ListItem item in items)
                {
                    // Access the file 
                    cc.Load(item.File);
                    cc.ExecuteQuery();
                    Microsoft.SharePoint.Client.File file1 = item.File;
                    //if (file1.Name.Equals("Finance.xlsx"))
                    if (file1.Name.Equals(templateFileName))
                    {
                        // retrieve the file as a byte array byte[] bArray = file.OpenBinary(); 
                        string filePath = System.IO.Path.Combine(pathDownload, file1.Name);
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                        fileName = fileName + "_" + user.Id + ".xlsx";
                        filePath = System.IO.Path.Combine(pathDownload, fileName);
                        cc.Load(file1, f=>f.ServerRelativePath); 
                        cc.ExecuteQuery();
                        FileInformation fi= Microsoft.SharePoint.Client.File.OpenBinaryDirect(cc,file1.ServerRelativePath.ToString());
                        
                        //open the file stream and write the file 
                        using (FileStream filestream = System.IO.File.Create(filePath))
                        //using (MemoryStream filestream = new MemoryStream())
                        {
                            fi.Stream.CopyTo(filestream);
                            //filestream.Write(bArray, 0, bArray.Length);
                            Console.WriteLine("Data Saved");
                            //File.WriteAllBytes(pathDownload, bArray);
                            excelFilePath = filePath;
                        }
                    }
                }
                return excelFilePath;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(strSiteUrl, "Error in Utility, downLoadFile", ex);
                return string.Empty;
            }
        }
        public static string UplaodFileToPageLibrary(string filePath, string siteUrl, int userID, string from)
        {
            string fileName = null;
            FileInfo fileExist = null;
            ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
            //url = cc.Web.Url;
            Site site = cc.Site;
            Web spWeb = cc.Web;
            try
            {

                if (!string.IsNullOrWhiteSpace(filePath))
                {


                    User user = spWeb.CurrentUser;
                    Microsoft.SharePoint.Client.File spfile = null;
                    //spWeb.AllowUnsafeUpdates = true;
                    //Check if file exists in specified path
                    fileExist = new FileInfo(filePath);
                    if (!fileExist.Exists)
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "File Not Found in Physical Path");
                    MCINameTagLogger.getMCINameTagLogger().WriteLog("Error in UplaodFileToPageLibrary(), UplaodFileToPageLibrary", "File Not Found in Physical Path");
                    //Get handle of library
                    //Folder spLibrary = spWeb.Folders["Pages"];
                    List listDoc = spWeb.Lists.GetByTitle("Pages");
                    //var items = listDoc.GetItems(CamlQuery.CreateAllItemsQuery());
                    Folder fldr = listDoc.RootFolder;
                    var spLibrary = fldr.Files;
                    cc.Load(listDoc);
                    cc.Load(listDoc.RootFolder);
                    cc.Load(fldr.Files);
                    cc.ExecuteQuery();
                    //Extract file name (file will be uploaded with this name)
                    fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    if (from == "F")
                        fileName = "Finance" + "_" + user.Id + ".xlsx";
                    if (from == "D")
                        fileName = "Dealer" + "_" + user.Id + ".xlsx";
                    if (from == "C")
                        fileName = "Corporate" + "_" + user.Id + ".xlsx";
                    //Read file for uploading
                    FileStream fileStream = System.IO.File.OpenRead(filePath);
                    //Replace existing file
                    Boolean replaceExistingFile = true;
                    //Upload document to library
                    var file = spWeb.GetFileByServerRelativeUrl(string.Format("{0}/Pages/{1}", spWeb.Url, fileName));
                    cc.Load(file, f => f.Exists);
                    cc.ExecuteQuery();
                    FileCreationInformation fileInfo = new FileCreationInformation();
                    fileInfo.ContentStream = fileStream;
                    fileInfo.Url = fileName;
                    if (file.Exists)
                    {
                        fileInfo.Overwrite = replaceExistingFile;
                        file.CheckOut();
                        spfile = spLibrary.Add(fileInfo);
                    }
                    else
                    {
                        fileInfo.Overwrite = false;
                        spfile = spLibrary.Add(fileInfo);
                    }
                    spfile.CheckIn("file uploaded via code",CheckinType.MajorCheckIn);
                    //Added by Ashis - file was not opening for user who does not have Contribute+ permission
                    spfile.Publish("publish from code");
                    //spLibrary.Update();
                    fileStream.Close();
                    cc.ExecuteQuery();
                    //spWeb.AllowUnsafeUpdates = false;


                }
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog("Error in UplaodFileToPageLibrary(), UplaodFileToPageLibrary", ex.Message);
            }
            finally
            {
                fileExist.Delete();
                cc.ExecuteQuery();
                MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Deleted File From Local Drive  in UplaodFileToPageLibrary() is completed");
                //Downloaded file
                fileExist = new FileInfo(excelFilePath);
                if (!fileExist.Exists)
                {
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Excel Not Found in Physical Path");
                }
                else
                {
                    fileExist.Delete();
                    cc.ExecuteQuery();
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Deleted excel From Local Drive  in UplaodFileToPageLibrary() is completed");
                }
            }
            return fileName;
        }


        public static string ReadFromExcelDealer(string path, DataTable dtData, string siteUrl, bool hasHeader = true, bool job = false, bool manual = false)
        {
            if(dtData.Rows.Count>0)
            {
                string stringLog = "";
                ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                //url = cc.Web.Url;
                Site site = cc.Site;
                Web spWeb = cc.Web;
                try
                {
                    path = downLoadFile(siteUrl, "FILENAMEDEALER");
                    
                    stringLog = "1.1, ";
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excelPack = new ExcelPackage())
                    {
                        stringLog = stringLog + "1.2, ";
                        //Load excel stream
                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            stringLog = stringLog + "1.3, ";
                            excelPack.Load(stream);
                        }
                        stringLog = stringLog + "1.4, ";
                        //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                        var ws = excelPack.Workbook.Worksheets[0];
                        stringLog = stringLog + "1.5, ";
                        //Get all details as DataTable -because Datatable make life easy :)
                        DataTable excelasTable = new DataTable();
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            stringLog = stringLog + "1.6, ";
                            //Get colummn details
                            if (!string.IsNullOrEmpty(firstRowCell.Text))
                            {
                                stringLog = stringLog + "1.7, ";
                                string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                                stringLog = stringLog + "1.8, ";
                                excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                            }
                            stringLog = stringLog + "1.9, ";
                        }
                        stringLog = stringLog + "1.10, ";
                        var startRow = hasHeader ? 2 : 1;
                        stringLog = stringLog + "1.11, ";
                        int i = 0;
                        //Get row details
                        if (dtData.Rows.Count > 0)
                        {
                            stringLog = stringLog + "1.12, ";
                            for (int rowNum = startRow; rowNum <= (startRow + dtData.Rows.Count - 1); rowNum++)
                            {
                                stringLog = stringLog + "1.13, ";
                                var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                                //ws.Cells[2, 16].Value = "M121";
                                stringLog = stringLog + "1.14, ";
                                DataRow row = excelasTable.Rows.Add();
                                stringLog = stringLog + "1.15, ";
                                DataRow drData = dtData.Rows[i];
                                stringLog = stringLog + "1.16, ";
                                int dCode = Convert.ToInt32(drData["DealerCode"].ToString());
                                stringLog = stringLog + "1.17, ";
                                string region = Convert.ToString(drData["Region"]);
                                stringLog = stringLog + "1.18, ";
                                string district = Convert.ToString(drData["District"]);
                                stringLog = stringLog + "1.19, ";
                                string name = Convert.ToString(drData["Name"]);
                                stringLog = stringLog + "1.20, ";
                                string address = Convert.ToString(drData["Address"]);
                                stringLog = stringLog + "1.21, ";
                                string dealerCity = Convert.ToString(drData["DealerCity"]);
                                stringLog = stringLog + "1.22, ";
                                string dealerProvince = Convert.ToString(drData["DealerProvince"]);
                                stringLog = stringLog + "1.23, ";
                                string dealerPostalCode = Convert.ToString(drData["DealerPostalCode"]);
                                stringLog = stringLog + "1.24, ";
                                string empName = Convert.ToString(drData["EmployeeFirstName"]) + " " + Convert.ToString(drData["EmployeeLastName"]);
                                stringLog = stringLog + "1.25, ";
                                string jobPosition = Convert.ToString(drData["JobTitle"]);
                                stringLog = stringLog + "1.26, ";
                                string nameTagDisplayName = Convert.ToString(drData["NameTagDisplayName"]);
                                stringLog = stringLog + "1.27, ";
                                string submittedBy = Convert.ToString(drData["CreatedBy"]);
                                stringLog = stringLog + "1.28, ";
                                string nameTagType = Convert.ToString(drData["NameTagType"]);
                                stringLog = stringLog + "1.29, ";
                                int quantity = Convert.ToInt32(drData["Quantity"].ToString());
                                stringLog = stringLog + "1.30, ";
                                string tennure = Convert.ToString(drData["Tennure"]);
                                stringLog = stringLog + "1.31, ";
                                //foreach (var cell in wsRow)
                                //{
                                //    row[cell.Start.Column - 1] = cell.Text;
                                ws.Cells[rowNum, 1].Value = region.Trim(); ;
                                stringLog = stringLog + "1.32, ";
                                ws.Cells[rowNum, 2].Value = district.Trim();
                                stringLog = stringLog + "1.33, ";
                                ws.Cells[rowNum, 3].Value = name.Trim();
                                stringLog = stringLog + "1.34, ";
                                ws.Cells[rowNum, 4].Value = dCode;
                                stringLog = stringLog + "1.35, ";
                                ws.Cells[rowNum, 5].Value = address.Trim();
                                stringLog = stringLog + "1.36, ";
                                ws.Cells[rowNum, 6].Value = dealerCity.Trim();
                                stringLog = stringLog + "1.37, ";
                                ws.Cells[rowNum, 7].Value = dealerProvince.Trim();
                                stringLog = stringLog + "1.38, ";
                                ws.Cells[rowNum, 8].Value = dealerPostalCode.Trim();
                                stringLog = stringLog + "1.39, ";
                                ws.Cells[rowNum, 9].Value = empName.Trim();
                                stringLog = stringLog + "1.40, ";
                                ws.Cells[rowNum, 10].Value = jobPosition.Trim();
                                stringLog = stringLog + "1.41, ";
                                ws.Cells[rowNum, 11].Value = nameTagDisplayName.Trim();
                                stringLog = stringLog + "1.42, ";
                                ws.Cells[rowNum, 12].Value = submittedBy;
                                stringLog = stringLog + "1.43, ";
                                ws.Cells[rowNum, 13].Value = nameTagType.Trim();
                                stringLog = stringLog + "1.44, ";
                                ws.Cells[rowNum, 14].Value = quantity;
                                stringLog = stringLog + "1.45, ";
                                ws.Cells[rowNum, 15].Value = tennure.Trim();
                                stringLog = stringLog + "1.46, ";
                                ws.Cells[rowNum, 16].Value = "M121";
                                ws.Cells[rowNum, 17].Value = Convert.ToInt32(drData["OrderID"]);
                                stringLog = stringLog + "1.47, ";
                                //}
                                i++;
                            }
                        }
                        //excelPack.Save();
                        user = null;
                        stringLog = stringLog + "1.48, ";
                        
                        stringLog = stringLog + "1.49, ";
                            
                        stringLog = stringLog + "1.50, ";
                        user = spWeb.CurrentUser;
                            
                        string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        stringLog = stringLog + "1.51, ";
                        string pathDownload = System.IO.Path.Combine(pathUser, "Downloads\\");
                        stringLog = stringLog + "1.52, ";
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                        stringLog = stringLog + "1.53, ";
                        if (job)
                        {
                            fileName = "Dealer" + "_" + "AutoProcess" + ".xlsx";
                            stringLog = stringLog + "1.54, ";
                        }
                        else
                        {
                            fileName = fileName + "_" + user.Id + ".xlsx";
                            stringLog = stringLog + "1.55, ";
                        }
                        stringLog = stringLog + "1.56, ";
                        string p_strPath = System.IO.Path.Combine(pathDownload, fileName);// pathDownload + "Finance_File.xlsx";//@"C:\DEV\Finance1.xlsx";
                        stringLog = stringLog + "1.57, ";
                        if (System.IO.File.Exists(p_strPath))
                        {
                            System.IO.File.Delete(p_strPath);
                            stringLog = stringLog + "1.58, ";
                        }


                        // Create excel file on physical disk 
                        FileStream objFileStrm = System.IO.File.Create(p_strPath);
                        stringLog = stringLog + "1.59, ";
                        objFileStrm.Close();
                        stringLog = stringLog + "1.60, ";
                        // Write content to excel file 
                        System.IO.File.WriteAllBytes(p_strPath, excelPack.GetAsByteArray());
                        //Close Excel package
                        stringLog = stringLog + "1.61, ";
                        excelPack.Dispose();
                        //Get everything as generics and let end user decides on casting to required type
                        //var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                        //return (T)Convert.ChangeType(generatedType, typeof(T));
                        //Upload File to Site Assets
                        stringLog = stringLog + "1.62, ";
                        if (job || manual)
                        {
                            stringLog = stringLog + "1.63.1, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            excelFilePathFinal = p_strPath;
                            stringLog = stringLog + "1.64, ";
                            FileInfo fileExist = new FileInfo(path);
                            stringLog = stringLog + "1.65, ";
                            fileExist.Delete();
                            stringLog = stringLog + "1.66, ";
                        }
                        else
                        {
                            stringLog = stringLog + "1.63.2, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            excelFilePathFinal = UplaodFileToPageLibrary(p_strPath, siteUrl, user.Id, "D");
                            stringLog = stringLog + "1.67, ";
                        }

                    }
                    stringLog = stringLog + "1.68, ";
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "StringLog: " + stringLog, null);
                    return excelFilePathFinal;
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Error in Utility, ReadFromExcelDealer", ex);
                    return string.Empty;
                }
            }
            else
            {
                excelFilePath = "";
                return excelFilePath;
            }            
        }

        public static string ReadFromExcelCorporate(string path, DataTable dtData, string siteUrl, bool hasHeader = true, bool job = false, bool manual = false)
        {
            if(dtData.Rows.Count>0)
            {
                string stringLog = "";

                ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                //url = cc.Web.Url;
                Site site = cc.Site;
                Web spWeb = cc.Web;
                try
                {
                    path = downLoadFile(siteUrl, "FILENAMECORPORATE");
                    
                    stringLog = "1.1, ";
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    stringLog = stringLog + "1.2, ";
                    using (var excelPack = new ExcelPackage())
                    {
                        stringLog = stringLog + "1.3, ";
                        //Load excel stream
                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            stringLog = stringLog + "1.4, ";
                            excelPack.Load(stream);
                        }
                        stringLog = stringLog + "1.5, ";
                        //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                        var ws = excelPack.Workbook.Worksheets[0];
                        stringLog = stringLog + "1.6, ";
                        //Get all details as DataTable -because Datatable make life easy :)
                        DataTable excelasTable = new DataTable();
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            stringLog = stringLog + "1.7, ";
                            //Get colummn details
                            if (!string.IsNullOrEmpty(firstRowCell.Text))
                            {
                                stringLog = stringLog + "1.8, ";
                                string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                                stringLog = stringLog + "1.9, ";
                                excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                            }
                            stringLog = stringLog + "1.10, ";
                        }
                        stringLog = stringLog + "1.11, ";
                        var startRow = hasHeader ? 2 : 1;
                        stringLog = stringLog + "1.12, ";
                        int i = 0;
                        //Get row details
                        if (dtData.Rows.Count > 0)
                        {
                            stringLog = stringLog + "1.13, ";
                            for (int rowNum = startRow; rowNum <= (startRow + dtData.Rows.Count - 1); rowNum++)
                            {
                                stringLog = stringLog + "1.14, ";
                                var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                                stringLog = stringLog + "1.15, ";
                                //ws.Cells[2, 16].Value = "M121";
                                DataRow row = excelasTable.Rows.Add();
                                DataRow drData = dtData.Rows[i];
                                stringLog = stringLog + "1.16, ";
                                string empName = Convert.ToString(drData["EmployeeFirstName"]) + " " + Convert.ToString(drData["EmployeeLastName"]);
                                stringLog = stringLog + "1.17, ";
                                string jobPosition = Convert.ToString(drData["JobTitle"]);
                                stringLog = stringLog + "1.18, ";
                                string nameTagDisplayName = Convert.ToString(drData["NameTagDisplayName"]);
                                stringLog = stringLog + "1.19, ";
                                string submittedBy = Convert.ToString(drData["CreatedBy"]);
                                stringLog = stringLog + "1.20, ";
                                string nameTagType = Convert.ToString(drData["NameTagType"]);
                                stringLog = stringLog + "1.21, ";
                                int quantity = Convert.ToInt32(drData["Quantity"].ToString());
                                stringLog = stringLog + "1.22, ";
                                string tennure = Convert.ToString(drData["Tennure"]);
                                stringLog = stringLog + "1.23, ";
                                int ordNum = Convert.ToInt32(drData["OrderID"]);
                                //foreach (var cell in wsRow)
                                //{
                                //    row[cell.Start.Column - 1] = cell.Text;
                                ws.Cells[rowNum, 1].Value = "Mazda Corporate";
                                stringLog = stringLog + "1.24, ";
                                ws.Cells[rowNum, 2].Value = "YY";
                                stringLog = stringLog + "1.25, ";
                                ws.Cells[rowNum, 3].Value = "Mazda Canada";
                                stringLog = stringLog + "1.26, ";
                                ws.Cells[rowNum, 4].Value = Convert.ToInt32("88888");
                                stringLog = stringLog + "1.27, ";
                                ws.Cells[rowNum, 5].Value = "55 VOGEL ROAD";
                                stringLog = stringLog + "1.28, ";
                                ws.Cells[rowNum, 6].Value = "RICHMOND HILL";
                                stringLog = stringLog + "1.29, ";
                                ws.Cells[rowNum, 7].Value = "ON ";
                                stringLog = stringLog + "1.30, ";
                                ws.Cells[rowNum, 8].Value = "L4B 3K5";
                                stringLog = stringLog + "1.31, ";
                                ws.Cells[rowNum, 9].Value = empName.Trim();
                                stringLog = stringLog + "1.32, ";
                                ws.Cells[rowNum, 10].Value = jobPosition.Trim();
                                stringLog = stringLog + "1.33, ";
                                ws.Cells[rowNum, 11].Value = nameTagDisplayName.Trim();
                                stringLog = stringLog + "1.34, ";
                                ws.Cells[rowNum, 12].Value = submittedBy;
                                stringLog = stringLog + "1.35, ";
                                ws.Cells[rowNum, 13].Value = nameTagType.Trim();
                                stringLog = stringLog + "1.36, ";
                                ws.Cells[rowNum, 14].Value = quantity;
                                stringLog = stringLog + "1.37, ";
                                ws.Cells[rowNum, 15].Value = tennure.Trim();
                                stringLog = stringLog + "1.38, ";
                                ws.Cells[rowNum, 16].Value = "M121";
                                //ws.Cells[rowNum, 17].Value = Convert.ToInt32(drData["OrderID"]);
                                ws.Cells[rowNum, 17].Value = ordNum;
                                //}
                                stringLog = stringLog + "1.39, ";
                                i++;
                            }
                            stringLog = stringLog + "1.40, ";
                        }
                        //excelPack.Save();
                        user = null;
                        
                        stringLog = stringLog + "1.41, ";
                        stringLog = stringLog + "1.42, ";
                        user = spWeb.CurrentUser;
                            
                        stringLog = stringLog + "1.43, ";
                        string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        stringLog = stringLog + "1.44, ";
                        string pathDownload = System.IO.Path.Combine(pathUser, "Downloads\\");
                        stringLog = stringLog + "1.45, ";
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                        stringLog = stringLog + "1.46, ";
                        if (job)
                        {
                            stringLog = stringLog + "1.47, ";
                            fileName = "Corporate" + "_" + "AutoProcess" + ".xlsx";
                        }
                        else
                        {
                            fileName = fileName + "_" + user.Id + ".xlsx";
                        }
                        stringLog = stringLog + "1.48, ";
                        string p_strPath = System.IO.Path.Combine(pathDownload, fileName);// pathDownload + "Finance_File.xlsx";//@"C:\DEV\Finance1.xlsx";
                        stringLog = stringLog + "1.49, ";
                        if (System.IO.File.Exists(p_strPath))
                        {
                            stringLog = stringLog + "1.50, ";
                            System.IO.File.Delete(p_strPath);
                        }
                        stringLog = stringLog + "1.51, ";
                        // Create excel file on physical disk 
                        FileStream objFileStrm = System.IO.File.Create(p_strPath);
                        stringLog = stringLog + "1.52, ";
                        objFileStrm.Close();
                        stringLog = stringLog + "1.53, ";
                        // Write content to excel file 
                        System.IO.File.WriteAllBytes(p_strPath, excelPack.GetAsByteArray());
                        stringLog = stringLog + "1.54, ";
                        //Close Excel package
                        excelPack.Dispose();
                        stringLog = stringLog + "1.55, ";
                        //Get everything as generics and let end user decides on casting to required type
                        //var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                        //return (T)Convert.ChangeType(generatedType, typeof(T));
                        //Upload File to Site Assets
                        if (job || manual)
                        {
                            stringLog = stringLog + "1.55.1, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            stringLog = stringLog + "1.56, ";
                            excelFilePathFinal = p_strPath;
                            stringLog = stringLog + "1.57, ";
                            FileInfo fileExist = new FileInfo(path);
                            stringLog = stringLog + "1.58, ";
                            fileExist.Delete();
                            stringLog = stringLog + "1.59, ";
                        }
                        else
                        {
                            stringLog = stringLog + "1.59.1, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Job : " + job + " and Manual : " + manual, null);
                            stringLog = stringLog + "1.60, ";
                            MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "StringLog: " + stringLog, null);
                            excelFilePathFinal = UplaodFileToPageLibrary(p_strPath, siteUrl, user.Id, "C");
                        }

                    }
                    stringLog = stringLog + "1.61, ";
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "StringLog: " + stringLog, null);
                    return excelFilePathFinal;
                }
                catch (Exception ex)
                {
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(siteUrl, "Error in Utility, ReadFromExcelDealer", ex);
                    return string.Empty;
                }
            }
            else
            {
                excelFilePath = "";
                return excelFilePath;
            }
        }
        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;
            if (Linqlist == null) return dt;
            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type IcolType = GetProperty.PropertyType;
                        if ((IcolType.IsGenericType) && (IcolType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            IcolType = IcolType.GetGenericArguments()[0];
                        }
                        dt.Columns.Add(new DataColumn(GetProperty.Name, IcolType));
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo p in columns)
                {
                    dr[p.Name] = p.GetValue(Record, null) == null ? DBNull.Value : p.GetValue(Record, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static void sendEmailWithAttachment(string subject, string body, string url, string toEmail, string finance, string dealer, string corporate)
        {
            //Attachment
           System.Net.Mail.Attachment attachment = null;
            String fileName = null;
            FileInfo fileExist = null;
            FileInfo fileExistFinance = null;
            FileInfo fileExistDealer = null;
            FileInfo fileExistCorporate = null;


            ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
            //url = cc.Web.Url;
            Site site = cc.Site;
            Web spWeb = cc.Web;
            if (finance!="")
            {
                fileExistFinance = new FileInfo(finance);
                if (!fileExistFinance.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Finance File Not Found in Physical Path", null);                
            }
            if(dealer!="")
            {
                fileExistDealer = new FileInfo(dealer);
                if (!fileExistDealer.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Dealer File Not Found in Physical Path", null);
            }
            if(corporate!="")
            {
                fileExistCorporate = new FileInfo(corporate);
                if (!fileExistCorporate.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Corporate File Not Found in Physical Path", null);
            }
            
            try
            {
                //toEmail = (!string.IsNullOrEmpty(toEmail)) ? toEmail.Substring(0, toEmail.Length - 2) : toEmail;
                string fromEmail = AppSettings.getAppsettingsValue("FromEmail", url);
                string toAddresses = string.Empty;
                string fromAddressDisplayName = AppSettings.getAppsettingsValue("AutoProcessFromDisplay", url);
                //Changed due to testing instaging
                toAddresses = AppSettings.getAppsettingsValue("AutoProcessToEmail", url);
                toAddresses = (!string.IsNullOrEmpty(toAddresses)) ? toAddresses : toEmail;
                int smtpPort = 0;
                int.TryParse((AppSettings.getAppsettingsValue("SMTPPort", url) ?? "").ToString(), out smtpPort);
                string smtpServer = AppSettings.getAppsettingsValue("SMTPServer", url);
                

                
                if (string.IsNullOrEmpty(smtpServer) || (string.IsNullOrEmpty(fromEmail)))
                {
                    try
                    {
                        smtpServer = site.ServerRelativeUrl;//WebApplication.OutboundMailServiceInstance.Server.Address;
                        smtpPort = 25;//oSPSite.WebApplication.OutboundMailPort;
                        fromEmail = "";//oSPSite.WebApplication.OutboundMailSenderAddress;
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(string subject, string body, string toEmail , string url ), MCI_NameTag_Utility", ex);
                    }

                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "ToEmail is : " + toEmail);
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Before sendEmail ");
                if (!string.IsNullOrEmpty(smtpServer) && !string.IsNullOrEmpty(fromEmail))
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail, fromAddressDisplayName);
                    mail.IsBodyHtml = true;
                    mail.To.Add(toAddresses);
                    mail.Subject = subject;
                    mail.Body = body;
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(finance);
                        attachment.Name = "Finance AutoProcess.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding finance attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(dealer);
                        attachment.Name = "Dealer AutoProcess.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding dealer attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(corporate);
                        attachment.Name = "Corporate AutoProcess.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding corporate attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                    smtp.UseDefaultCredentials = true;
                    smtp.Send(mail);
                    //attachment.Dispose();
                }

                else
                {

                    var email = new EmailProperties();
                    email.To = new List<string> { toAddresses };
                    email.Subject = subject;
                    email.Body = body;
                    //StringDictionary headers = new StringDictionary();
                    //headers.Add("to", toAddresses);
                    //headers.Add("subject", subject);
                    //headers.Add("fAppendHtmlTag", "True"); // To enable HTML format
                    Microsoft.SharePoint.Client.Utilities.Utility.SendEmail(cc,email);
                    cc.ExecuteQuery();
                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "After sendEmail ");
                    

            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(), MCI_NameTag_Utility", ex);
            }
            finally
            {

                fileExistFinance.Delete();
                fileExistDealer.Delete();
                fileExistCorporate.Delete();
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Deleted File From Local Drive  in sendEmail() is completed");
            }

        }
        public static void sendProcessedEmailWithAttachment(string subject, string body, string url, string toEmail, string finance, string dealer, string corporate)
        {
            //Attachment
            System.Net.Mail.Attachment attachment = null;
            String fileName = null;
            FileInfo fileExist = null;
            FileInfo fileExistFinance = null;
            FileInfo fileExistDealer = null;
            FileInfo fileExistCorporate = null;
            if (finance != "")
            {
                fileExistFinance = new FileInfo(finance);
                if (!fileExistFinance.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Finance File Not Found in Physical Path", null);
            }
            if (dealer != "")
            {
                fileExistDealer = new FileInfo(dealer);
                if (!fileExistDealer.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Dealer File Not Found in Physical Path", null);
            }
            if (corporate != "")
            {
                fileExistCorporate = new FileInfo(corporate);
                if (!fileExistCorporate.Exists)
                    MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Corporate File Not Found in Physical Path", null);
            }

            try
            {
                //toEmail = (!string.IsNullOrEmpty(toEmail)) ? toEmail.Substring(0, toEmail.Length - 2) : toEmail;
                string fromEmail = AppSettings.getAppsettingsValue("FromEmail", url);
                string toAddresses = string.Empty;
                string fromAddressDisplayName = AppSettings.getAppsettingsValue("AutoProcessFromDisplay", url);
                //Changed due to testing instaging
                toAddresses = AppSettings.getAppsettingsValue("AutoProcessToEmail", url);
                toAddresses = (!string.IsNullOrEmpty(toAddresses)) ? toAddresses : toEmail;
                int smtpPort = 0;
                int.TryParse((AppSettings.getAppsettingsValue("SMTPPort", url) ?? "").ToString(), out smtpPort);
                string smtpServer = AppSettings.getAppsettingsValue("SMTPServer", url);

                ClientContext cc = new PnP.Framework.AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                
                Site oSPSite = cc.Site;
                Web newWeb = cc.Web;

                if (string.IsNullOrEmpty(smtpServer) || (string.IsNullOrEmpty(fromEmail)))
                {
                    try
                    {
                        smtpServer = oSPSite.ServerRelativeUrl;//oSPSite.WebApplication.OutboundMailServiceInstance.Server.Address;
                        smtpPort = 25;// oSPSite.WebApplication.OutboundMailPort;
                        fromEmail = "";//oSPSite.WebApplication.OutboundMailSenderAddress;
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(string subject, string body, string toEmail , string url ), MCI_NameTag_Utility", ex);
                    }

                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "ToEmail is : " + toEmail);
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Before sendEmail ");
                if (!string.IsNullOrEmpty(smtpServer) && !string.IsNullOrEmpty(fromEmail))
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail, fromAddressDisplayName);
                    mail.IsBodyHtml = true;
                    mail.To.Add(toAddresses);
                    mail.Subject = subject;
                    mail.Body = body;
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(finance);
                        attachment.Name = "Finance.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding finance attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(dealer);
                        attachment.Name = "Dealer.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding dealer attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    try
                    {
                        attachment = new System.Net.Mail.Attachment(corporate);
                        attachment.Name = "Corporate.xlsx";// What should be the name of attachment.
                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in adding corporate attachment, sendEmail(), MCI_NameTag_Utility", ex);
                    }
                    SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                    smtp.UseDefaultCredentials = true;
                    smtp.Send(mail);
                    //attachment.Dispose();
                }

                else
                {
                    var email = new EmailProperties();
                    email.To = new List<string> { toAddresses };
                    email.Subject = subject;
                    email.Body = body;
                    //StringDictionary headers = new StringDictionary();
                    //headers.Add("to", toAddresses);
                    //headers.Add("subject", subject);
                    //headers.Add("fAppendHtmlTag", "True"); // To enable HTML format
                    Microsoft.SharePoint.Client.Utilities.Utility.SendEmail(cc,email);
                    cc.ExecuteQuery();
                }
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "After sendEmail ");
                    

            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Error in sendEmail(), MCI_NameTag_Utility", ex);
            }
            finally
            {

                fileExistFinance.Delete();
                fileExistDealer.Delete();
                fileExistCorporate.Delete();
                MCINameTagLogger.getMCINameTagLogger().WriteLog(url, "Deleted File From Local Drive  in sendEmail() is completed");
            }

        }

    }


}
