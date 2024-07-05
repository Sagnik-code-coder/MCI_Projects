using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_Utility
{
    public class MCINameTagLogging //: SPDiagnosticsServiceBase
    {
        public static string DiagnosticAreaName = "MCINameTagLogging";
        private static MCINameTagLogging _Current;
        public static MCINameTagLogging Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new MCINameTagLogging();
                }



                return _Current;
            }
        }
        private MCINameTagLogging()//: base("MCI NameTag Logging Service", SPFarm.Local)
        {
        }
        //protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        //{
        //    List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
        //    {
        //        new SPDiagnosticsArea(DiagnosticAreaName, new List<SPDiagnosticsCategory>
        //        {
        //            new SPDiagnosticsCategory("MCINameTagLogging",
        //            TraceSeverity.Unexpected, EventSeverity.Error)
        //        })
        //    };
        //    return areas;
        //}
        public static void LogError(string categoryName, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "Info";
            }

            TelemetryConfiguration.Active.InstrumentationKey = "the_key";
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;

            var tc = new TelemetryClient();
            tc.TrackRequest(errorMessage, DateTimeOffset.UtcNow, new TimeSpan(0, 0, 3), "200", true);
            tc.TrackMetric(errorMessage, 100);
            tc.TrackEvent(errorMessage);

            tc.Flush();

            //SPDiagnosticsCategory category =
            //MCINameTagLogging.Current
            //.Areas[DiagnosticAreaName]
            //.Categories[categoryName];
            //MCINameTagLogging.Current.WriteTrace(0, category,
            //TraceSeverity.Unexpected, errorMessage);
        }
    }
}
