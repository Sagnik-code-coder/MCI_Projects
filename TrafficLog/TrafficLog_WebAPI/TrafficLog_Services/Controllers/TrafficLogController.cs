using MCI_TrafficLog_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.SharePoint;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
//using Microsoft.SharePoint.Client;
//using System.Data.Entity.Migrations;
using TrafficLog_Services.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrafficLog_Services.ViewModels;


namespace TrafficLog_Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLogController : ControllerBase
    {
        readonly TrafficLogContext _context;
        public TrafficLogController(TrafficLogContext context)
        {
            _context = context;
        }
        //public string GetJSONString(DataTable table)
        //{
        //    System.Text.StringBuilder headStrBuilder = new StringBuilder(table.Columns.Count * 5); //pre-allocate some space, default is 16 bytes
        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        headStrBuilder.AppendFormat("\"{0}\" : \"{0}{1}¾\",", table.Columns[i].Caption, i);
        //    }
        //    headStrBuilder.Remove(headStrBuilder.Length - 1, 1); // trim away last ,

        //    StringBuilder sb = new StringBuilder(table.Rows.Count * 5); //pre-allocate some space
        //    sb.Append("{\"");
        //    sb.Append(table.TableName);
        //    sb.Append("\" : [");
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        string tempStr = headStrBuilder.ToString();
        //        sb.Append("{");
        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {
        //            table.Rows[i][j] = table.Rows[i][j].ToString().Replace("'", "");
        //            tempStr = tempStr.Replace(table.Columns[j] + j.ToString() + "¾", table.Rows[i][j].ToString());
        //        }
        //        sb.Append(tempStr + "},");
        //    }
        //    sb.Remove(sb.Length - 1, 1); // trim last ,
        //    sb.Append("]}");
        //    return sb.ToString();
        //}

        [HttpGet("GetDealerDropDown")]
        public string GetDealerDropDown([FromQuery] ApiParameters parameters)
        {
            List<string> dealerView = null;
            try
            {
                
                    dealerView = (from dealerview in _context.DealerViews
                                  where dealerview.DealerCode != null && !dealerview.DealerCode.Equals("")
                                  select dealerview.DealerCode).Distinct().ToList<string>();

                
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDealerDropDown Web API, GetDealerDropDown", ex);
            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }

        public struct regionData
        {
            public string strtype { get; set; }
            public string strvalue { get; set; }
            public regionData(string type, string value)
            {
                strtype = type;
                strvalue = value;
            }
        }

        [HttpGet("GetRegionDropDown")]
        public string GetRegionDropDown([FromQuery] ApiParameters parameters)
        {
            List<regionData> dealerView = new List<regionData>();
            List<string> lstRegion = null;
            List<string> lstArea = null;
            List<string> lstVolumeGroup = null;

            try
            {
                    DateTime dateTimeNow = DateTime.Today;
                    lstRegion = ((from dealerview in _context.DealerViews
                                  where dealerview.Region != null && !dealerview.Region.Equals("")
                                  select dealerview.Region).Distinct().ToList<string>());
                    lstArea = ((from dealerview in _context.DealerViews
                                where dealerview.Area != null && !dealerview.Area.Equals("")
                                select dealerview.Area).Distinct().ToList<string>());
                    lstVolumeGroup = ((from dealerview in _context.DealerViews
                                       where dealerview.Volumn != null && !dealerview.Volumn.Equals("")
                                       select dealerview.Volumn).Distinct().ToList<string>());
                    for (int i = 0; i < lstRegion.Count; i++)
                    {
                        dealerView.Add(new regionData("Region", lstRegion[i]));

                    }
                    dealerView.Add(new regionData("Dividor1", "DIVIDOR1"));
                    for (int i = 0; i < lstArea.Count; i++)
                    {
                        dealerView.Add(new regionData("Area", lstArea[i]));
                    }
                    dealerView.Add(new regionData("Dividor2", "DIVIDOR2"));
                    for (int i = 0; i < lstVolumeGroup.Count; i++)
                    {
                        dealerView.Add(new regionData("VolumeGroup", lstVolumeGroup[i]));
                    }


            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetRegionDropDown Web API, GetRegionDropDown", ex);
                string str = ex.Message;

            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }


        [HttpGet("GetDistrictDropDown")]
        public string GetDistrictDropDown([FromQuery] ApiParameters parameters)
        {
            List<string> dealerView = null;
            try
            {
                    DateTime dateTimeNow = DateTime.Today;
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where dealerview.District != null && !dealerview.District.Equals("")
                                      select dealerview.District).Distinct().ToList<string>();

                    }
           
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDistrictDropDown Web API, GetDistrictDropDown", ex);
            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }

        [HttpGet("GetDistrictDropDownFiltered")]
        public string GetDistrictDropDownFiltered([FromQuery] ApiParameters1 parameters1)
        {
            List<string> dealerView = null;
            try
            {
                    DateTime dateTimeNow = DateTime.Today;
                    if (parameters1.Region != "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where (dealerview.Region == parameters1.Region || dealerview.Area == parameters1.Region || dealerview.Volumn == parameters1.Region)
                                      select dealerview.District).Distinct().ToList<string>();
                    }
                    else
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      select dealerview.District).Distinct().ToList<string>();
                    }
            
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDistrictDropDownFiltered Web API, GetDistrictDropDownFiltered", ex);
            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }

        [HttpGet("GetDealerDropDownFiltered")]
        public string GetDealerDropDownFiltered([FromQuery] ApiParameters1 parameters1)
        {
            List<string> dealerView = null;
            try
            {
                    DateTime dateTimeNow = DateTime.Today;

                    if (parameters1.Region != "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where (dealerview.Region == parameters1.Region || dealerview.Area ==  parameters1.Region || dealerview.Volumn == parameters1.Region)
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }
                    else
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }

              

            }
            catch (Exception ex) 
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDealerDropDownFiltered Web API, GetDealerDropDownFiltered", ex);
            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }

        [HttpGet("GetDealerDropDownFilteredDistricWise")]
        public string GetDealerDropDownFilteredDistricWise([FromQuery] ApiParameters2 parameters2)
        {
            List<string> dealerView = null;
            try
            {
                    DateTime dateTimeNow = DateTime.Today;

                    if (parameters2.Region != "0" && parameters2.District != "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where ((dealerview.Region == parameters2.Region || dealerview.Area == parameters2.Region || dealerview.Volumn == parameters2.Region) && dealerview.District == parameters2.District)
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }
                    if (parameters2.Region == "0" && parameters2.District == "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }
                    if (parameters2.Region != "0" && parameters2.District == "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where (dealerview.Region == parameters2.Region || dealerview.Area == parameters2.Region || dealerview.Volumn == parameters2.Region)
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }
                    if (parameters2.Region == "0" && parameters2.District != "0")
                    {
                        dealerView = (from dealerview in _context.DealerViews
                                      where (dealerview.District == parameters2.District)
                                      select dealerview.DealerCode).Distinct().ToList<string>();
                    }

            

            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDealerDropDownFilteredDistricWise Web API, GetDealerDropDownFilteredDistricWise", ex);
            }
            return JsonConvert.SerializeObject(new { data = dealerView });

        }

        private ReportingWeekCalendar GetCurrentReportingWeekCalendar(TrafficLogContext dc, int weekid, DateTime dateTimeNow)
        {

            DateTime localDate = DateTime.Parse(dateTimeNow.ToShortDateString());

            ReportingWeekCalendar reportingWeekCalender = null;
            if (weekid != 0)
            {
                reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                                         where reportingweek.ReportingWeekId == weekid
                                         select reportingweek).FirstOrDefault();
            }
            else
            {
                //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                //                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                //                           && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                //                           || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate)
                //                           && (reportingweek.ReportingWeekStartDate <= dateTimeNow && reportingweek.SubmissionWindowEndDate >= dateTimeNow))
                //                          && ((reportingweek.IsDeleted ?? false) == false)
                //                         select reportingweek).FirstOrDefault();

                //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                //                         where ((dateTimeNow >= reportingweek.ReportingWeekStartDate && reportingweek.ReportingWeekEndDate >= dateTimeNow)
                //                           && (reportingweek.ReportingYearNumber == dateTimeNow.Year)
                //                           )
                //                          && ((reportingweek.IsDeleted ?? false) == false)
                //                         select reportingweek).FirstOrDefault();

                reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && reportingweek.SubmissionWindowEndDate >= dateTimeNow))
                                          && ((reportingweek.IsDeleted ?? false) == false)
                                         select reportingweek).FirstOrDefault();

                //bool isgreaterthen5pm = false;
                //if (reportingWeekCalender != null)
                //{
                //    TimeSpan timespan = new TimeSpan(17, 00, 00);
                //    DateTime time = reportingWeekCalender.SubmissionWindowStartDate.Value.Date.Add(timespan);
                //    if (dateTimeNow >= time)
                //    {
                //        isgreaterthen5pm = true;
                //    }

                //}
                // if (reportingWeekCalender == null || isgreaterthen5pm == false)
                if (reportingWeekCalender == null)
                {
                    ReportingWeekCalendar reportingWeekCalenderNextWeek = (from reportingweek in dc.ReportingWeekCalendars
                                                                           where ((localDate >= reportingweek.ReportingWeekStartDate && reportingweek.ReportingWeekEndDate >= localDate))
                                                                            && ((reportingweek.IsDeleted ?? false) == false)
                                                                           select reportingweek).FirstOrDefault();
                    if (reportingWeekCalenderNextWeek == null) return null;
                    int yearc = reportingWeekCalenderNextWeek.ReportingWeekNumber ?? 0;
                    reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                                             where ((reportingweek.ReportingWeekNumber == reportingWeekCalenderNextWeek.ReportingWeekNumber - 1) && (reportingweek.ReportingYearNumber == reportingWeekCalenderNextWeek.ReportingYearNumber)
                                               )
                                              && ((reportingweek.IsDeleted ?? false) == false)
                                             select reportingweek).FirstOrDefault();
                    if (yearc == 1)
                    {

                        reportingWeekCalender = (from maxReportingweek in dc.ReportingWeekCalendars.OrderByDescending(x => x.ReportingWeekNumber)
                                                 where (maxReportingweek.ReportingYearNumber == reportingWeekCalenderNextWeek.ReportingYearNumber - 1)
                                                  && ((maxReportingweek.IsDeleted ?? false) == false)
                                                 select maxReportingweek).FirstOrDefault();
                    }


                }
            }

            return reportingWeekCalender;

        }

        private ReportingWeekCalendar GetRCWWithinSubmissionWindow(TrafficLogContext dc, int weekid, DateTime dateTimeNow)
        {

            ReportingWeekCalendar reportingWeekCalender = null;
            if (weekid != 0)
            {
                reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                                         where reportingweek.ReportingWeekId == weekid
                                         select reportingweek).FirstOrDefault();
            }
            else
            {

                reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                                         where (dateTimeNow >= reportingweek.SubmissionWindowStartDate && reportingweek.SubmissionWindowEndDate >= dateTimeNow.Date)
                                         select reportingweek).FirstOrDefault();


            }

            return reportingWeekCalender;

        }

        private DateTime ConvertFromLocaltoDealer(DateTime localDate, decimal? dealerTimeZone)
        {

            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);

            TimeSpan offSet = TimeSpan.FromHours((double)dealerTimeZone);

            DateTime newDateTime = utcDateTime.Add(offSet);
            MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", string.Format("Dealer Time {0} Dealer TimeZone {1}", newDateTime, dealerTimeZone));
            //TicketingSystemLogging.LogError("TicketingSystemLogging", string.Format("Dealer Time {0} Dealer TimeZone {1}", newDateTime, dealerTimeZone));
            return newDateTime;

        }

        [HttpPost("AddOrUpdateDealer")]
        
        public async Task<string> AddOrUpdateDealerAsync(AddORUpdateDealer parameters3)
        {
            try
            {
                DateTime localDateTime = DateTime.Now;

                //int? timezone = 0;
                decimal? timezone = 0;
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
                List<TrafficLogResources> trafficlogs = JsonConvert.DeserializeObject<List<TrafficLogResources>>(parameters3.Dealerdata);
                string emailaddress = string.Empty;
                int reportingWeekId = 0;
                string region = string.Empty, district = string.Empty, dealerCode = string.Empty, userName = string.Empty;



                //To get Utc of current datetime
                //DateTime localDateTime = DateTime.Parse(userDateTimeUtc);
                //DateTime utcDateTime = localDateTime.ToUniversalTime();
                //string currentUser = SPContext.Current.Web.CurrentUser.LoginName;
                string currentUser = userName;

                // DateTime localDate = DateTime.Parse(localDateTime.ToShortDateString());
                //To get Utc of current datetime

                //using (TrafficLogContext _context = new TrafficLogContext())

                //{
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {

                            if (parameters3.UserType == "Dealer")
                            {
                                if (trafficlogs != null && trafficlogs.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(dealerCode))
                                    {
                                        dealerCode = trafficlogs[0].DealerCode;
                                    }
                                }
                                if (!string.IsNullOrEmpty(dealerCode))
                                {

                                    DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == dealerCode).FirstOrDefault();
                                    if (dealerTimeZone != null)
                                    {
                                        timezone = dealerTimeZone.TimeZone;
                                    }
                                }

                                localDateTime = ConvertFromLocaltoDealer(localDateTime, timezone);
                            }
                            //DateTime localDateTime = DateTime.Parse(userDateTime);
                            //DateTime localDate = DateTime.Parse(localDateTime.ToShortDateString());
                            //ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, weekid, localDate);
                            foreach (TrafficLogResources resource in trafficlogs)
                            {
                                resource.CarLine = new CarLine();
                                resource.CarLine.CarLineId = resource.CarLineId;
                                resource.CarLine.CarLineName = resource.Model;
                                DealerView dealer_View = _context.DealerViews.Where(x => x.DealerCode == resource.DealerCode).FirstOrDefault();

                                TrafficLog trafficlog = resource.ConvertToTrafficLog();
                                var targetRecord = _context.TrafficLogs.Where(x => x.DealerCode == resource.DealerCode && x.ReportingWeekId == resource.ReportingWeekId && x.CarLineId == resource.CarLineId).FirstOrDefault();
                                var exists = targetRecord != null;

                                if (exists)
                                {
                                    targetRecord.ValidFrom = targetRecord.ValidFrom;
                                    targetRecord.ValidTo = targetRecord.ValidTo;
                                    targetRecord.ModifiedBy = currentUser;
                                    //targetRecord.ModifiedDateTime = DateTime.UtcNow;
                                    //targetRecord.ModifiedDateTime = localDateTime;//To get the utc time of data modified
                                    targetRecord.ModifiedDateTime = DateTime.Now;//To get the utc time of data modified
                                    targetRecord.WeeklyTraffic = resource.WeeklyTraffic;
                                    targetRecord.WeeklyWrites = resource.WeeklyWrites;
                                    targetRecord.UserName = currentUser;
                                    targetRecord.MonthlySalesForecast = resource.MonthlySalesForecast;
                                    targetRecord.MonthlyTarget = resource.MonthlyTarget;
                                    _context.Entry(targetRecord).Property(x => x.UserName).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.WeeklyTraffic).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.WeeklyWrites).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.MonthlySalesForecast).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.MonthlyTarget).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.MonthlySalesForecast).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.ModifiedBy).IsModified = true;
                                    _context.Entry(targetRecord).Property(x => x.ModifiedDateTime).IsModified = true;
                                    emailaddress = dealer_View.Email;
                                    _context.SaveChanges();
                                    reportingWeekId = resource.ReportingWeekId;
                                    region = dealer_View != null ? dealer_View.Region.Trim() : "";
                                    district = dealer_View != null ? dealer_View.District.Trim() : "";
                                    dealerCode = resource.DealerCode;
                                    userName = resource.UserName;


                                }
                                else
                                {
                                    var trafficlogObject = new TrafficLog
                                    {
                                        DealerArea = dealer_View != null ? dealer_View.Area.Trim() : "",
                                        DealerCity = dealer_View != null ? dealer_View.City.Trim() : "",
                                        DealerCode = resource.DealerCode,
                                        DealerDistrict = dealer_View != null ? dealer_View.District.Trim() : "",
                                        DealerMetro = dealer_View != null ? dealer_View.Metro.Trim() : "",
                                        DealerName = dealer_View != null ? dealer_View.Name.Trim() : "",
                                        DealerProvince = dealer_View != null ? dealer_View.Prov.Trim() : "",
                                        DealerRegion = dealer_View != null ? dealer_View.Region.Trim() : "",
                                        ReportingWeekId = resource.ReportingWeekId,
                                        CarLineId = resource.CarLineId,
                                        MonthlySalesForecast = resource.MonthlySalesForecast,
                                        WeeklyTraffic = resource.WeeklyTraffic,
                                        WeeklyWrites = resource.WeeklyWrites,
                                        UserName = currentUser,
                                        MonthlyTarget = resource.MonthlyTarget,
                                        CreatedBy = resource.UserName,
                                        //CreatedDateTime = DateTime.UtcNow,
                                        //CreatedDateTime = localDateTime,//To get the utc time of data submitted
                                        CreatedDateTime = DateTime.Now,
                                        ModifiedBy = currentUser,
                                        //ModifiedDateTime = DateTime.UtcNow
                                        //ModifiedDateTime = localDateTime,//To get the utc time of data modified
                                        ModifiedDateTime = DateTime.Now

                                    };
                                    emailaddress = dealer_View.Email.Trim();
                                    _context.TrafficLogs.Add(trafficlogObject);
                                    _context.SaveChanges();
                                    reportingWeekId = resource.ReportingWeekId;
                                    region = dealer_View != null ? dealer_View.Region.Trim() : "";
                                    district = dealer_View != null ? dealer_View.District.Trim() : "";
                                    dealerCode = resource.DealerCode;
                                    userName = resource.UserName.Trim();

                                }
                            }

                            var targetMEPRecord = _context.HistoricalSubmissionReports.Where(x => x.ReportingWeekId == reportingWeekId && x.DealerCode == dealerCode).FirstOrDefault();
                            if (targetMEPRecord == null)
                            {
                                string submissionStatus = "LATE";
                                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "localDate: " + localDateTime);
                                //TicketingSystemLogging.LogError("TicketingSystemLogging", "localDate: " + localDateTime);
                                ReportingWeekCalendar rcwSubmissionWindow = GetRCWWithinSubmissionWindow(_context, 0, localDateTime);

                                int comparingreportingid = reportingWeekId;
                                //if (userType == "Dealer")
                                //{
                                //    comparingreportingid = reportingWeekId - 1;
                                //}
                                if (rcwSubmissionWindow != null && rcwSubmissionWindow.ReportingWeekId == comparingreportingid)
                                {
                                    //TimeSpan timespan = new TimeSpan(17, 00, 00);
                                    //DateTime time = rcwSubmissionWindow.SubmissionWindowStartDate.Value.Date.Add(timespan);
                                    MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", rcwSubmissionWindow.SubmissionWindowStartDate.ToString());
                                    //TicketingSystemLogging.LogError("TicketingSystemLogging", rcwSubmissionWindow.SubmissionWindowStartDate.ToString());
                                    if (rcwSubmissionWindow.SubmissionWindowStartDate <= localDateTime && localDateTime <= rcwSubmissionWindow.SubmissionWindowFirstDeadline)
                                    {
                                        submissionStatus = "ON TIME";

                                    }

                                }
                                //update MEP Flag
                                var historicalSubmissionReport = new HistoricalSubmissionReport
                                {
                                    //Id = 0,
                                    RegionCode = region,
                                    DistrictCode = district,
                                    DealerCode = dealerCode,
                                    SubmissionStatus = submissionStatus,
                                    ReportingWeekId = reportingWeekId,
                                    CreatedBy = userName,
                                    //CreatedDateTime = DateTime.UtcNow,
                                    //CreatedDateTime = localDateTime //To get the utc of the data created
                                    CreatedDateTime = DateTime.Now

                                };
                                _context.HistoricalSubmissionReports.Add(historicalSubmissionReport);


                            }
                            _context.SaveChanges();

                            //SendNotification(emailaddress, url, local);
                            //SendNotificationWithTime(dealerCode, url, local, localDateTime);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", ex.ToString());
                            //TicketingSystemLogging.LogError("TicketingSystemLogging", ex.ToString());
                            return "failed";

                        }
                    }

                //}

            }
            catch (Exception ex)
            {

                //TicketingSystemLogging.LogError("TicketingSystemLogging", ex.ToString());
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", ex.ToString());
                return "failed";
            }

            return "success";
        }

        
        
        [HttpGet("GetDealer")]
        public string GetDealer([FromQuery] GETDealer parameters4)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                DateTime localDateTime = DateTime.Now;
                decimal? timezone = 0;
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

                    if (parameters4.UserType == "Dealer")
                    {
                        if (!string.IsNullOrEmpty(parameters4.DealerCode))
                        {
                            DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == parameters4.DealerCode).FirstOrDefault();
                            if (dealerTimeZone != null)
                            {
                                timezone = dealerTimeZone.TimeZone;
                            }
                        }
                        localDateTime = ConvertFromLocaltoDealer(currentDateTime, timezone);
                    }
                    // DateTime localDateTime = DateTime.Parse(userDateTime);
                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());
                    List<TrafficLogResources> trafficLogs = null;


                    //List<MonthlySalesTarget_View> monthlyTarget = dc.MonthlySalesTarget_Views.Where(x => x.DRYEAR == dateTimeNow.Year && x.DEALER == dealerCode && x.MONTHID == monthID).ToList();
                    DealerView dealer_View = _context.DealerViews.Where(x => x.DealerCode == parameters4.DealerCode).FirstOrDefault();
                    List<CarLine> carlines = _context.CarLines.ToList();
                    List<CarLine> carlinesValid = _context.CarLines.Where(x => localDateTime >= x.CarLineEffectiveDate && (localDateTime <= x.CarLineEndDate || x.CarLineEndDate == null)).ToList();
                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters4.Weekid, localDateTime);

                    string monthID = "QTY" + (reportingWeekCalender.ReportingMonthNumber ?? 0).ToString();
                    List<MonthlySalesTargetView> monthlyTarget = _context.MonthlySalesTargetViews.Where(x => x.Dryear == reportingWeekCalender.ReportingYearNumber && x.Dealer == parameters4.DealerCode && x.Monthid == monthID).ToList();

                    // ReportingWeekCalendar reportingWeekCalender = GetRCWWithinSubmissionWindow(dc, weekid, dateTimeNow);
                    //dc.ReportingWeekCalendars.Where(x => dateTimeNow >= x.SubmissionWindowStartDate && dateTimeNow <= x.SubmissionWindowEndDate).FirstOrDefault();
                    if (reportingWeekCalender != null)
                    {
                        // trafficLogs = dc.TrafficLogs.Where(x => x.ReportingWeekID == reportingWeekCalender.ReportingWeekID && x.DealerCode == dealerCode).ToList();
                        var trafficLogMonths = (from trafficlog in _context.TrafficLogs.ToList<TrafficLog>()
                                                join trafficmonth in (from o in _context.ReportingWeekCalendars
                                                                      where o.ReportingMonthNumber == reportingWeekCalender.ReportingMonthNumber && o.ReportingYearNumber == reportingWeekCalender.ReportingYearNumber
                                                                      select o)
                                                on trafficlog.ReportingWeekId equals trafficmonth.ReportingWeekId
                                                where trafficlog.DealerCode == parameters4.DealerCode && trafficlog.ReportingWeekId != reportingWeekCalender.ReportingWeekId
                                                group trafficlog by trafficlog.CarLineId into g
                                                select new { CarLineId = g.FirstOrDefault().CarLineId, WeeklyWritesSum = g.Sum(pc => pc.WeeklyWrites) }
                                             );


                        trafficLogs = (from trafficlog in _context.TrafficLogs.ToList<TrafficLog>()
                                       where trafficlog.ReportingWeekId == reportingWeekCalender.ReportingWeekId && trafficlog.DealerCode == parameters4.DealerCode
                                       orderby trafficlog.CarLine.CarLineName
                                       select new TrafficLogResources
                                       {
                                           DealerArea = trafficlog.DealerArea,
                                           DealerCity = trafficlog.DealerCity,
                                           DealerCode = trafficlog.DealerCode,
                                           DealerDistrict = trafficlog.DealerDistrict,
                                           DealerMetro = trafficlog.DealerMetro,
                                           DealerName = trafficlog.DealerName,
                                           DealerProvince = trafficlog.DealerProvince,
                                           DealerRegion = trafficlog.DealerRegion,
                                           ReportingWeekId = trafficlog.ReportingWeekId,
                                           CarLineId = trafficlog.CarLineId,
                                           UserName = trafficlog.UserName,
                                           ModifiedBy = trafficlog.UserName,
                                           MonthlySalesForecast = trafficlog.MonthlySalesForecast,
                                           WeeklyTraffic = trafficlog.WeeklyTraffic,
                                           WeeklyWrites = trafficlog.WeeklyWrites,
                                           Model = trafficlog.CarLine == null ? "" : trafficlog.CarLine.CarLineName,
                                           Closing = ((trafficlog.WeeklyTraffic / (trafficlog.WeeklyWrites == 0 ? 1 : trafficlog.WeeklyWrites)) * 100).ToString(),
                                           Writes_MTD = (trafficLogMonths.Where(pc => pc.CarLineId == trafficlog.CarLineId).Select(x => (x.WeeklyWritesSum ?? 0))).FirstOrDefault().ToString(),
                                           MonthlyTarget = trafficlog.MonthlyTarget,
                                           TrafficLogId = trafficlog.TrafficLogId
                                       }).ToList<TrafficLogResources>();

                        if (trafficLogs == null || trafficLogs.Count == 0)
                        {

                            trafficLogs = (from c in carlinesValid
                                           join mt in monthlyTarget on c.CarLineName equals mt.Carline into mmt
                                           from groupmmt in mmt.DefaultIfEmpty()

                                           orderby c.CarLineName
                                           select new TrafficLogResources
                                           {
                                               TrafficLogId = 0,
                                               ReportingWeekId = reportingWeekCalender.ReportingWeekId,
                                               UserName = parameters4.UserName,
                                               CarLineId = c.CarLineId,
                                               Model = c.CarLineName,
                                               DealerCode = parameters4.DealerCode,
                                               DealerName = dealer_View.Name,
                                               DealerArea = dealer_View.Area,
                                               DealerRegion = dealer_View.Region,
                                               DealerDistrict = dealer_View.District,
                                               DealerMetro = dealer_View.Metro,
                                               Closing = "",
                                               DealerProvince = dealer_View.Prov,
                                               DealerCity = dealer_View.City,
                                               MonthlyTarget = (short)(groupmmt?.Qty ?? 0),
                                               Writes_MTD = (trafficLogMonths.Where(pc => pc.CarLineId == c.CarLineId).Select(x => (x.WeeklyWritesSum ?? 0))).FirstOrDefault().ToString(),
                                           }).ToList<TrafficLogResources>();
                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
                    return JsonConvert.SerializeObject(new { data = trafficLogs });
                
            }
            catch (Exception ex)
            {

                //TicketingSystemLogging.LogError("TicketingSystemLogging", ex.ToString());
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", ex.ToString());
                return "failed";

            }
        }

        [HttpGet("GetSubmissionReportWOW")]
        public string GetSubmissionReportWOW([FromQuery] SubmissionReport p1)
        {
            List<SubmissionReport_WOW> trafficLogs = new List<SubmissionReport_WOW>();

                DateTime dateTimeNow = DateTime.Now;
                ReportingWeekCalendar reportingWeekCalender = null;

                try
                {
                    reportingWeekCalender = (from reportingweek in _context.ReportingWeekCalendars
                                             where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                                              || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                                              && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                                             select reportingweek).FirstOrDefault();


                    if (reportingWeekCalender != null)
                    {


                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p1.DealerCode);
                        if (p1.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                        }
                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                        var areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", p1.Area);
                        if (p1.Area == "0")
                        {
                            areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", p1.Region);
                        if (p1.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        trafficLogs = _context.SubmissionReport_WOWs.FromSqlRaw<SubmissionReport_WOW>("dbo.usp_MCIReport_WOW @DealerCode, @ReportWeekID, @AREA, @REGION", dealerCodeParam, reportingWeeIDParam, areaParam, regionParam).ToList();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
                }
                catch (Exception ex)
                {
                    MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSubmissionReportWOW Web API, GetSubmissionReportWOW", ex);
                }

                return JsonConvert.SerializeObject(new { data = trafficLogs });
           
        }

        [HttpGet("GetSubmissionReportMOM")]
        public string GetSubmissionReportMOM([FromQuery] SubmissionReport p1)
        {
            List<SubmissionReport_MOM> trafficLogs = new List<SubmissionReport_MOM>();

            try
            {

                    DateTime dateTimeNow = DateTime.Now;
                    ReportingWeekCalendar reportingWeekCalender = null;

                    reportingWeekCalender = (from reportingweek in _context.ReportingWeekCalendars
                                             where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                                              || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                                              && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                                             select reportingweek).FirstOrDefault();


                    if (reportingWeekCalender != null)
                    {


                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p1.DealerCode);
                        if (p1.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                        }

                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                        var areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", p1.Area);
                        if (p1.Area == "0")
                        {
                            areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", p1.Region);
                        if (p1.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        trafficLogs = _context.Database.SqlQueryRaw<SubmissionReport_MOM>("dbo.usp_MCIReport_MOM @DealerCode, @ReportWeekID, @AREA, @REGION", dealerCodeParam, reportingWeeIDParam, areaParam, regionParam).ToList();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }

                
            }
            catch(Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSubmissionReportMOM Web API, GetSubmissionReportMOM", ex);
            }

            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetSubmissionReportMTD")]
        public string GetSubmissionReportMTD([FromQuery] SubmissionReport p1)
        {
            List<SubmissionReport_MTD> trafficLogs = new List<SubmissionReport_MTD>();

            try
            {
                    DateTime dateTimeNow = DateTime.Now;
                    ReportingWeekCalendar reportingWeekCalender = null;

                    reportingWeekCalender = (from reportingweek in _context.ReportingWeekCalendars
                                             where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                                              || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                                              && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                                             select reportingweek).FirstOrDefault();


                    if (reportingWeekCalender != null)
                    {


                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p1.DealerCode);
                        if (p1.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                        }

                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                        var areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", p1.Area);
                        if (p1.Area == "0")
                        {
                            areaParam = new Microsoft.Data.SqlClient.SqlParameter("@AREA", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", p1.Region);
                        if (p1.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        trafficLogs = _context.Database.SqlQueryRaw<SubmissionReport_MTD>("dbo.usp_MCIReport_MTD @DealerCode, @ReportWeekID, @AREA, @REGION", dealerCodeParam, reportingWeeIDParam, areaParam, regionParam).ToList();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSubmissionReportMTD Web API, GetSubmissionReportMTD", ex);
            }

            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetDetailReport_MTD")]
        public string GetDetailReport_MTD([FromQuery] GETDetailReport parameters5)
        {
            List<DetailReport_MTD> trafficLogs = new List<DetailReport_MTD>();

            try
            {

                    DateTime localDateTime = DateTime.Now;
                    DateTime currentDateTime = DateTime.Now;

                    decimal? timezone = 0;
                    if (parameters5.Weekid != 0)
                    {

                        if (!string.IsNullOrEmpty(parameters5.DealerCode))
                        {

                            DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == parameters5.DealerCode).FirstOrDefault();
                            if (dealerTimeZone != null)
                            {
                                timezone = dealerTimeZone.TimeZone;
                            }
                        }

                        localDateTime = ConvertFromLocaltoDealer(currentDateTime, timezone);
                    }

                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());
                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters5.Weekid, localDateTime);
                    if (reportingWeekCalender != null)
                    {
                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", parameters5.DealerCode);
                        if (parameters5.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", DBNull.Value);
                        }
                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@REPORTWEEKID", System.Data.SqlDbType.Int);
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        var reportingSubmissionParam = new Microsoft.Data.SqlClient.SqlParameter("@SUBMISSION", System.Data.SqlDbType.VarChar);
                        reportingSubmissionParam.Value = "MTD";

                        var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", parameters5.District);
                        if (parameters5.District == "0")
                        {
                            districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", parameters5.Region);
                        if (parameters5.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }

                        trafficLogs = _context.DetailReport_MTDs.FromSqlRaw<DetailReport_MTD>("dbo.[usp_MCIDetail_MTD] @DEALERCODE, @REPORTWEEKID, @SUBMISSION, @REGION, @DISTRICT", dealerCodeParam, reportingWeeIDParam, reportingSubmissionParam, districtParam, regionParam).ToList();

                    }
                
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDetailReport_MTD Web API, GetDetailReport_MTD", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });

        }

        [HttpGet("GetDetailReport_MTDTrend")]
        public string GetDetailReport_MTDTrend([FromQuery] GETDetailReport parameters5)
        {
            List<DetailReport_MTDTrend> trafficLogs = new List<DetailReport_MTDTrend>();

            try
            {

                    DateTime localDateTime = DateTime.Now;
                    DateTime currentDateTime = DateTime.Now;

                    decimal? timezone = 0;
                    if (parameters5.Weekid != 0)
                    {

                        if (!string.IsNullOrEmpty(parameters5.DealerCode))
                        {

                            DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == parameters5.DealerCode).FirstOrDefault();
                            if (dealerTimeZone != null)
                            {
                                timezone = dealerTimeZone.TimeZone;
                            }
                        }

                        localDateTime = ConvertFromLocaltoDealer(currentDateTime, timezone);
                    }

                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());
                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters5.Weekid, localDateTime);
                    if (reportingWeekCalender != null)
                    {

                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", parameters5.DealerCode);
                        if (parameters5.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", DBNull.Value);
                        }
                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@REPORTWEEKID", System.Data.SqlDbType.Int);
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        var reportingSubmissionParam = new Microsoft.Data.SqlClient.SqlParameter("@SUBMISSION", System.Data.SqlDbType.VarChar);
                        reportingSubmissionParam.Value = "MTDTrend";

                        var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", parameters5.District);
                        if (parameters5.District == "0")
                        {
                            districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", parameters5.Region);
                        if (parameters5.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }

                        trafficLogs = _context.DetailReport_MTDTrends.FromSqlRaw<DetailReport_MTDTrend>("dbo.[usp_MCIDetail_MTDTrend] @DEALERCODE, @REPORTWEEKID, @SUBMISSION, @REGION, @DISTRICT", dealerCodeParam, reportingWeeIDParam, reportingSubmissionParam, districtParam, regionParam).ToList();

                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDetailReport_MTDTrend Web API, GetDetailReport_MTDTrend", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });

        }

        [HttpGet("GetDetailReport_WOW")]
        public string GetDetailReport_WOW([FromQuery] GETDetailReport parameters5)
        {
            List<DetailReport_WOW> trafficLogs = new List<DetailReport_WOW>();

            try
            {
                    //DateTime localDateTime = DateTime.Parse(userDateTime);
                    //DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());

                    DateTime localDateTime = DateTime.Now;
                    DateTime currentDateTime = DateTime.Now;

                    decimal? timezone = 0;
                    if (parameters5.Weekid != 0)
                    {

                        if (!string.IsNullOrEmpty(parameters5.DealerCode))
                        {

                            DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == parameters5.DealerCode).FirstOrDefault();
                            if (dealerTimeZone != null)
                            {
                                timezone = dealerTimeZone.TimeZone;
                            }
                        }

                        localDateTime = ConvertFromLocaltoDealer(currentDateTime, timezone);
                    }

                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());

                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters5.Weekid, localDateTime);
                    if (reportingWeekCalender != null)
                    {
                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", parameters5.DealerCode);
                        if (parameters5.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", DBNull.Value);
                        }
                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@REPORTWEEKID", System.Data.SqlDbType.Int);
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        var reportingSubmissionParam = new Microsoft.Data.SqlClient.SqlParameter("@SUBMISSION", System.Data.SqlDbType.VarChar);
                        reportingSubmissionParam.Value = "WOW";

                        var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", parameters5.District);
                        if (parameters5.District == "0")
                        {
                            districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", parameters5.Region);
                        if (parameters5.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }

                        trafficLogs = _context.DetailReport_WOWs.FromSqlRaw<DetailReport_WOW>("dbo.[usp_MCIDetail_WOW] @DEALERCODE, @REPORTWEEKID, @SUBMISSION, @REGION, @DISTRICT", dealerCodeParam, reportingWeeIDParam, reportingSubmissionParam, districtParam, regionParam).ToList();

                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDetailReport_WOW Web API, GetDetailReport_WOW", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });

        }

        [HttpGet("GetDetailReport_MOM")]
        public string GetDetailReport_MOM([FromQuery] GETDetailReport parameters5)
        {
            List<DetailReport_MOM> trafficLogs = new List<DetailReport_MOM>();

            try
            {

                    //DateTime localDateTime = DateTime.Parse(userDateTime);
                    //DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());

                    DateTime localDateTime = DateTime.Now;
                    DateTime currentDateTime = DateTime.Now;
                    decimal? timezone = 0;
                    if (parameters5.Weekid != 0)
                    {



                        if (!string.IsNullOrEmpty(parameters5.DealerCode))
                        {

                            DealerTimeZone dealerTimeZone = _context.DealerTimeZones.Where(x => x.DealerCode == parameters5.DealerCode).FirstOrDefault();
                            if (dealerTimeZone != null)
                            {
                                timezone = dealerTimeZone.TimeZone;
                            }
                        }

                        localDateTime = ConvertFromLocaltoDealer(currentDateTime, timezone);
                    }

                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());
                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters5.Weekid, localDateTime);

                    if (reportingWeekCalender != null)
                    {
                        var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", parameters5.DealerCode);
                        if (parameters5.DealerCode == "0")
                        {
                            dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", DBNull.Value);
                        }
                        var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@REPORTWEEKID", System.Data.SqlDbType.Int);
                        reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                        var reportingSubmissionParam = new Microsoft.Data.SqlClient.SqlParameter("@SUBMISSION", System.Data.SqlDbType.VarChar);
                        reportingSubmissionParam.Value = "MOM";

                        var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", parameters5.District);
                        if (parameters5.District == "0")
                        {
                            districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", DBNull.Value);
                        }
                        var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", parameters5.Region);
                        if (parameters5.Region == "0")
                        {
                            regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                        }

                        trafficLogs = _context.DetailReport_MOMs.FromSqlRaw<DetailReport_MOM>("dbo.[usp_MCIDetail_MOM] @DEALERCODE, @REPORTWEEKID, @SUBMISSION, @REGION, @DISTRICT", dealerCodeParam, reportingWeeIDParam, reportingSubmissionParam, districtParam, regionParam).ToList();

                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetDetailReport_MOM Web API, GetDetailReport_MOM", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });

        }

        [HttpGet("GetSummaryReport")]
        public string GetSummaryReport([FromQuery] GETSummaryReport parameters6)
        {
            DateTime localDateTime = DateTime.Now;
            // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());

            List<SummaryReport> trafficLogs = new List<SummaryReport>();

            try
            {

                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, parameters6.Weekid, localDateTime);
                    var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", parameters6.DealerCode);
                    if (parameters6.DealerCode == "0")
                    {
                        dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DEALERCODE", DBNull.Value);
                    }
                    var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@REPORTWEEKID", System.Data.SqlDbType.Int);
                    reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                    var reportingSubmissionParam = new Microsoft.Data.SqlClient.SqlParameter("@SUBMISSION", System.Data.SqlDbType.VarChar);
                    reportingSubmissionParam.Value = parameters6.Submission;

                    var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", parameters6.District);
                    if (parameters6.District == "0")
                    {
                        districtParam = new Microsoft.Data.SqlClient.SqlParameter("@DISTRICT", DBNull.Value);
                    }
                    var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", parameters6.Region);
                    if (parameters6.Region == "0")
                    {
                        regionParam = new Microsoft.Data.SqlClient.SqlParameter("@REGION", DBNull.Value);
                    }
                    trafficLogs = _context.SummaryReports.FromSqlRaw<SummaryReport>("dbo.[usp_SUMMARY] @DEALERCODE, @SUBMISSION, @REPORTWEEKID, @DISTRICT, @REGION", dealerCodeParam, reportingSubmissionParam, reportingWeeIDParam, districtParam, regionParam).ToList();

              
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSummaryReport Web API, GetSummaryReport", ex);
            }

            return JsonConvert.SerializeObject(new { data = trafficLogs });

        }

        [HttpGet("GetSummaryReportMTD")]
        public string GetSummaryReportMTD([FromQuery] Summary_Report p)
        {
            List<SummaryReport_MTD> trafficLogs = new List<SummaryReport_MTD>();

            try
            {

                    DateTime localDateTime = DateTime.Now;
                    // DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());
                    //ReportingWeekCalendar reportingWeekCalender = null;

                    //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                    //                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                    //                          || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                    //                          && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                    //                         select reportingweek).FirstOrDefault();


                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, p.Weekid, localDateTime);



                    if (reportingWeekCalender != null)
                    {

                        using (TrafficLogContext _context = new TrafficLogContext())
                        {
                            var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p.DealerCode);
                            if (p.DealerCode == "0")
                            {
                                dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                            }

                            var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                            var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", p.District);
                            if (p.District == "0")
                            {
                                districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", DBNull.Value);
                            }
                            var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", p.Region);
                            if (p.Region == "0")
                            {
                                regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", DBNull.Value);
                            }
                            reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                            trafficLogs = _context.SummaryReport_MTDs.FromSqlRaw<SummaryReport_MTD>("dbo.[usp_SummaryReport_MTD] @DealerCode, @ReportWeekID, @District, @Region", dealerCodeParam, reportingWeeIDParam, districtParam, regionParam).ToList();

                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSummaryReportMTD Web API, GetSummaryReportMTD", ex);
            }

            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetSummaryReportWOW")]
        public string GetSummaryReportWOW([FromQuery] Summary_Report p)
        {
            List<SummaryReport_WOW> trafficLogs = new List<SummaryReport_WOW>();

            try
            {
                    DateTime localDateTime = DateTime.Now;
                    //   DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());



                    //ReportingWeekCalendar reportingWeekCalender = null;

                    //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                    //                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                    //                          || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                    //                          && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                    //                         select reportingweek).FirstOrDefault();

                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, p.Weekid, localDateTime);


                    if (reportingWeekCalender != null)
                    {

                        using (TrafficLogContext _context = new TrafficLogContext())
                        {
                            var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p.DealerCode);
                            if (p.DealerCode == "0")
                            {
                                dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                            }

                            var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                            var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", p.District);
                            if (p.District == "0")
                            {
                                districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", DBNull.Value);
                            }
                            var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", p.Region);
                            if (p.Region == "0")
                            {
                                regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", DBNull.Value);
                            }
                            reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                            trafficLogs = _context.SummaryReport_WOWs.FromSqlRaw<SummaryReport_WOW>("dbo.[usp_SummaryReport_WOW] @DealerCode, @ReportWeekID, @District, @Region", dealerCodeParam, reportingWeeIDParam, districtParam, regionParam).ToList();

                        }
                    }


                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
                
            }
            catch (Exception ex) 
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSummaryReportWOW Web API, GetSummaryReportWOW", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetSummaryReportMOM")]
        public string GetSummaryReportMOM([FromQuery] Summary_Report p)
        {
            List<SummaryReport_MOM> trafficLogs = new List<SummaryReport_MOM>();

            try
            {

                    DateTime localDateTime = DateTime.Now;
                    //  DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());

                    //ReportingWeekCalendar reportingWeekCalender = null;

                    //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                    //                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                    //                          || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                    //                          && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                    //                         select reportingweek).FirstOrDefault();

                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, p.Weekid, localDateTime);


                    if (reportingWeekCalender != null)
                    {

                        using (TrafficLogContext _context = new TrafficLogContext())
                        {
                            var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p.DealerCode);
                            if (p.DealerCode == "0")
                            {
                                dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                            }

                            var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                            var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", p.District);
                            if (p.District == "0")
                            {
                                districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", DBNull.Value);
                            }
                            var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", p.Region);
                            if (p.Region == "0")
                            {
                                regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", DBNull.Value);
                            }
                            reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                            trafficLogs = _context.SummaryReport_MOMs.FromSqlRaw<SummaryReport_MOM>("dbo.[usp_SummaryReport_MOM] @DealerCode, @ReportWeekID, @District, @Region", dealerCodeParam, reportingWeeIDParam, districtParam, regionParam).ToList();

                        }
                    }


                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSummaryReportMOM Web API, GetSummaryReportMOM", ex);
            }

            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetSummaryReportMTDTrend")]
        public string GetSummaryReportMTDTrend([FromQuery] Summary_Report p)
        {
            List<SummaryReport_MTDTrend> trafficLogs = new List<SummaryReport_MTDTrend>();

            try
            {
                    DateTime localDateTime = DateTime.Now;
                    //  DateTime dateTimeNow = DateTime.Parse(localDateTime.ToShortDateString());


                    //ReportingWeekCalendar reportingWeekCalender = null;

                    //reportingWeekCalender = (from reportingweek in dc.ReportingWeekCalendars
                    //                         where ((dateTimeNow >= reportingweek.SubmissionWindowStartDate && dateTimeNow <= reportingweek.SubmissionWindowEndDate)
                    //                          || (dateTimeNow >= reportingweek.ReportingWeekStartDate && dateTimeNow <= reportingweek.ReportingWeekEndDate))
                    //                          && (reportingweek.ReportingMonthNumber == dateTimeNow.Month && reportingweek.ReportingYearNumber == dateTimeNow.Year)
                    //                         select reportingweek).FirstOrDefault();

                    ReportingWeekCalendar reportingWeekCalender = GetCurrentReportingWeekCalendar(_context, p.Weekid, localDateTime);


                    if (reportingWeekCalender != null)
                    {

                        using (TrafficLogContext _context = new TrafficLogContext())
                        {
                            var dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", p.DealerCode);
                            if (p.DealerCode == "0")
                            {
                                dealerCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@DealerCode", DBNull.Value);
                            }

                            var reportingWeeIDParam = new Microsoft.Data.SqlClient.SqlParameter("@ReportWeekID", System.Data.SqlDbType.Int);
                            var districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", p.District);
                            if (p.District == "0")
                            {
                                districtParam = new Microsoft.Data.SqlClient.SqlParameter("@District", DBNull.Value);
                            }
                            var regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", p.Region);
                            if (p.Region == "0")
                            {
                                regionParam = new Microsoft.Data.SqlClient.SqlParameter("@Region", DBNull.Value);
                            }
                            reportingWeeIDParam.Value = reportingWeekCalender.ReportingWeekId;

                            trafficLogs = _context.SummaryReport_MTDTrends.FromSqlRaw<SummaryReport_MTDTrend>("dbo.[usp_SummaryReport_MTDTrend] @DealerCode, @ReportWeekID, @District, @Region", dealerCodeParam, reportingWeeIDParam, districtParam, regionParam).ToList();

                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Message = "Reporting Week information not found in Admin table" });
                    }
               
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetSummaryReportMTDTrend Web API, GetSummaryReportMTDTrend", ex);
            }
            return JsonConvert.SerializeObject(new { data = trafficLogs });
        }

        [HttpGet("GetReportingWeekCalender")]
        public string GetReportingWeekCalender([FromQuery] ApiParameters parameters)
        {
            List<ReportingWeekCalenderView> reportingWeekCalendar_View = null;
            try
            {
                    DateTime dateTimeNow = DateTime.Today;

                    {
                        reportingWeekCalendar_View = (from dealerview in _context.ReportingWeekCalenderViews select dealerview).ToList<ReportingWeekCalenderView>();

                    }

            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in GetReportingWeekCalender Web API, GetReportingWeekCalender", ex);
                string message = ex.Message;
            }
            return JsonConvert.SerializeObject(new { data = reportingWeekCalendar_View });

        }

        [HttpPost("AddOrUpdateReportingWeekCalender")]
        public string AddOrUpdateReportingWeekCalender(AddORUpdateReportingWeekCalender parameters7)
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            List<ReportingWeekCalenderView> reportingweek = JsonConvert.DeserializeObject<List<ReportingWeekCalenderView>>(parameters7.ReportingData);
            string emailaddress = string.Empty;

            try
            {
                //using (TrafficLogContext _context = new TrafficLogContext())

                //{

                    foreach (ReportingWeekCalenderView resource in reportingweek)
                    {

                        ReportingWeekCalendar reportingweekCalender = _context.ReportingWeekCalendars.Where(x => x.ReportingWeekId == resource.ReportingWeekId).FirstOrDefault();



                        var exists = reportingweekCalender != null;

                        if (exists)
                        {
                            reportingweekCalender.ReportingWeekDescriptionEn = resource.ReportingWeekDescriptionEn;
                            reportingweekCalender.ReportingWeekDescriptionFr = resource.ReportingWeekDescriptionFr;
                            reportingweekCalender.ModifiedBy = parameters7.UserName;
                            reportingweekCalender.ModifiedDateTime = DateTime.UtcNow;
                            reportingweekCalender.ReportingWeekStartDate = resource.ReportingWeekStartDate;
                            reportingweekCalender.ReportingWeekEndDate = resource.ReportingWeekEndDate;
                            reportingweekCalender.SubmissionWindowStartDate = resource.SubmissionWindowStartDate;
                            reportingweekCalender.SubmissionWindowEndDate = resource.SubmissionWindowEndDate;
                            reportingweekCalender.SubmissionWindowFirstDeadline = resource.SubmissionWindowFirstDeadline;
                            reportingweekCalender.SubmissionWindowDescriptionEn = resource.SubmissionWindowDescriptionEn;
                            reportingweekCalender.SubmissionWindowDescriptionFr = resource.SubmissionWindowDescriptionFr;
                            _context.Entry(reportingweekCalender).Property(x => x.ReportingWeekDescriptionEn).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.ReportingWeekDescriptionFr).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.ModifiedBy).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.ModifiedDateTime).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.ReportingWeekStartDate).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.ReportingWeekEndDate).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.SubmissionWindowStartDate).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.SubmissionWindowEndDate).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.SubmissionWindowFirstDeadline).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.SubmissionWindowDescriptionEn).IsModified = true;
                            _context.Entry(reportingweekCalender).Property(x => x.SubmissionWindowDescriptionFr).IsModified = true;

                            _context.SaveChanges();


                        }
                        else
                        {
                            //var trafficlogObject = new TrafficLog
                            //{
                            //    DealerArea = reportingweekCalender != null ? reportingweekCalender.Area : "",
                            //    DealerCity = reportingweekCalender != null ? reportingweekCalender.City : "",
                            //    DealerCode = resource.DealerCode,
                            //    DealerDistrict = reportingweekCalender != null ? reportingweekCalender.District : "",
                            //    DealerMetro = reportingweekCalender != null ? reportingweekCalender.Metro : "",
                            //    DealerName = reportingweekCalender != null ? reportingweekCalender.Name : "",
                            //    DealerProvince = reportingweekCalender != null ? reportingweekCalender.Prov : "",
                            //    DealerRegion = reportingweekCalender != null ? reportingweekCalender.Region : "",
                            //    ReportingWeekID = resource.ReportingWeekID,
                            //    CarLineID = resource.CarLineID,
                            //    MonthlySalesForecast = resource.MonthlySalesForecast,
                            //    WeeklyTraffic = resource.WeeklyTraffic,
                            //    WeeklyWrites = resource.WeeklyWrites,
                            //    UserName = resource.UserName,
                            //    MonthlyTarget = resource.MonthlyTarget,
                            //    CreatedBy = resource.UserName,
                            //    CreatedDateTime = DateTime.UtcNow,
                            //    ModifiedBy = resource.UserName,
                            //    ModifiedDateTime = DateTime.UtcNow

                            //};
                            //emailaddress = reportingweekCalender.Email;
                            //_context.TrafficLogs.Add(trafficlogObject);
                            //_context.SaveChanges();

                        }
                    }
                    // SendNotification(emailaddress, url, local);
                //}
            }
            catch (Exception ex)
            {
                MCITrafficLogLogger.GetMCITrafficLogLogger().WriteLog("", "Error in AddOrUpdateReportingWeekCalender Web API, AddOrUpdateReportingWeekCalender", ex);
            }
            return "success";
        }
            

    }
}
