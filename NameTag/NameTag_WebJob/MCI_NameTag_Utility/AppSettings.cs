using Microsoft.SharePoint.Client;
using PnP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_Utility
{
    public class AppSettings
    {
        public static string getAppsettingsValue(string Title, string Url = "")
        {
            try
            {
                //SPWeb web = SPContext.Current.Web;

                string strValue = string.Empty;
                ClientContext cc = new AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "","");
                string url = Url == "" ? cc.Web.Url : Url;
                Site newSite = cc.Site;
                Web newWeb = cc.Web;
                   
                List listSettings = newWeb.Lists.GetByTitle("AppSettings");
                cc.Load(listSettings);
                cc.ExecuteQuery();
                if (listSettings != null)
                {
                    CamlQuery oQuery = new CamlQuery();
                    //oQuery.ViewAttsributes = "Scope = 'Recursive'";
                    oQuery.ViewXml = "<View Scope='RecursiveAll'><Query><Where><Eq><FieldRef Name='Title'/>" +
                        "<Value Type='Text'>" + Title + "</Value></Eq></Where></Query></View>";

                    ListItemCollection collListItems = listSettings.GetItems(oQuery);
                    cc.Load(collListItems);
                    cc.ExecuteQuery();
                    foreach (ListItem oListItem in collListItems)
                    {
                        strValue = (oListItem["Value"] ?? "").ToString();
                    }
                }
                return strValue;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(Url, "Error in getAppsettingsValue(Title), MCI_NameTag", ex);
                return string.Empty;
            }
        }

        public static List<string> getAppsettingsValueRole(string Role, string Url = "")
        {
            string lineNumber = string.Empty;
            try
            {
                
                ClientContext cc = new AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                string url = Url == "" ? cc.Web.Url : Url;
                Site newSite = cc.Site;
                Web newWeb = cc.Web;
                List<string> strValues = new List<string>();

                
                lineNumber = lineNumber + "1,";
                   

                lineNumber = lineNumber + "2,";
                List listSettings = newWeb.Lists.GetByTitle("AppSettings");
                cc.Load(listSettings);
                cc.ExecuteQuery();
                lineNumber = lineNumber + "3,";
                if (listSettings != null)
                {
                    CamlQuery oQuery = new CamlQuery();
                    //oQuery.ViewAttributes = "Scope = 'Recursive'";
                    oQuery.ViewXml = string.Format("<View Scope='RecursiveAll'><Query><Where>" +
                                                            "<Eq>" +
                                                                "<FieldRef Name='Role' />" +
                                                                "<Value Type='Choice'>" + Role + "</Value>" +
                                                            "</Eq>" +
                                                    "</Where></Query></View>");

                    ListItemCollection collListItems = listSettings.GetItems(oQuery);
                    cc.Load(collListItems);
                    cc.ExecuteQuery();
                    foreach (ListItem oListItem in collListItems)
                    {
                        strValues.Add((oListItem["Value"] ?? "").ToString());
                    }
                }
                        
                lineNumber = lineNumber + "4,";
                MCINameTagLogger.getMCINameTagLogger().WriteLog(Url, "Info in getAppsettingsValue(SystemName, Role), MCI_NameTag_Utility" + "," + lineNumber, null);
                return strValues;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(Url, "Error in getAppsettingsValue(SystemName, Role), MCI_Accessory_Utility" + "," + lineNumber, ex);
                return null;
            }
        }

        public static List<string> getAppsettingsValueByQuery(string CAMLQuery, string Url = "")
        {
            try
            {
                ClientContext cc = new AuthenticationManager().GetACSAppOnlyContext("https://mazdausa.sharepoint.com/sites/MCISPOTest", "", "");
                string url = Url == "" ? cc.Web.Url : Url;
                Site newSite = cc.Site;
                Web newWeb = cc.Web;
                List<string> strValues = new List<string>();

                List listSettings = newWeb.Lists.GetByTitle("AppSettings");
                cc.Load(listSettings);
                cc.ExecuteQuery();
                if (listSettings != null)
                {
                    CamlQuery oQuery = new CamlQuery();
                    //oQuery.ViewAttributes = "Scope = 'Recursive'";
                    oQuery.ViewXml = string.Format(CAMLQuery);

                    ListItemCollection collListItems = listSettings.GetItems(oQuery);
                    cc.Load(collListItems);
                    cc.ExecuteQuery();
                    foreach (ListItem oListItem in collListItems)
                    {
                        strValues.Add((oListItem["Value"] ?? "").ToString());
                    }
                }
                        
                return strValues;
            }
            catch (Exception ex)
            {
                MCINameTagLogger.getMCINameTagLogger().WriteLog(Url, "Error in getAppsettingsValueByQuery(CAML Query), MCI_NameTag_Utility", ex);
                return null;
            }
        }
    }
}
