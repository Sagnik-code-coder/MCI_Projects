//using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_Utility
{
    public class MCINameTagLogger
    {
        private static MCINameTagLogger nametaglogger;
        private MCINameTagLogger()
        {

        }
        public static MCINameTagLogger getMCINameTagLogger()
        {
            if (nametaglogger == null)
                nametaglogger = new MCINameTagLogger();
            return nametaglogger;

        }
        public void WriteLog(string siteUrl, string strLog, Exception ex = null)
        {
            

                if (ex != null)
                {
                    MCINameTagLogging.LogError("MCINameTagLogging", Convert.ToString(ex.Message));
                    MCINameTagLogging.LogError("MCINameTagLogging", Convert.ToString(ex.InnerException));
                    MCINameTagLogging.LogError("MCINameTagLogging", Convert.ToString(ex.StackTrace));
                }
                else
                {
                    MCINameTagLogging.LogError("MCINameTagLogging", strLog);
                }
        }
    }
}
