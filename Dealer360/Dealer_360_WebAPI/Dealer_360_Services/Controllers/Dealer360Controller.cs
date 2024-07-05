using Dealer_360_Services.Models.DB;
using Dealer_360_Services.Models.View_Models;
using MCI_Dealer_360_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Dealer_360_Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dealer360Controller : ControllerBase
    {
        readonly SpDealer360Context _context;


        public Dealer360Controller(SpDealer360Context context)
        {
            _context = context;
        }
        [HttpGet("GetAwards")]
        public string GetAwards()
        {
            try
            {
                List<AwardMaster> list = new List<AwardMaster>();
                list = _context.AwardMasters.ToList();
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("GetAwards", "Error in Web API, GetAwards", ex);
                return ex.Message.ToString();
            }
        }
        [HttpGet("GetDealers")]
        public string GetDealers()
        {
            try
            {
                List<Dealer> list = new List<Dealer>();
                list = _context.Dealers.ToList();
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("GetDealers", "Error in Web API, GetDealers", ex);
                return ex.Message.ToString();
            }
        }
        [HttpGet("GetDealersAwardDetails")]
        public string GetDealersAwardDetails()
        {
            try
            {
                List<GetDealerAwardsDetails> list = new List<GetDealerAwardsDetails>();
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@DealerAwardsID",Value=0 },
                new SqlParameter{ParameterName="@Mode",Value= "Get" }
                    };
                list = _context.GetDealerAwardsDetail.FromSqlRaw("dbo.USP_GetDealerAwardsDetails @DealerAwardsID,@Mode", Params.ToArray()).ToList();
                list=list.Where(x=>x.DealerCode!="0").ToList();
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("GetDealersAwardDetails", "Error in Web API, GetDealersAwardDetails", ex);
                return ex.Message.ToString();
            }
        }
        [HttpPost("InsertOrUpdateDealersAwardDetails")]
        public string InsertOrUpdateDealersAwardDetails(InsertOrUpdateDealerAwardsDetails obj)
        {
            try
            {
                //List<GetDealerAwardsDetails> list = new List<GetDealerAwardsDetails>();
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@DealerAwardsID",Value=0 },
                new SqlParameter{ParameterName="@DealerCode",Value= obj.DealerCode },
                new SqlParameter{ParameterName="@AwardYear",Value= obj.AwardsYears },
                new SqlParameter{ParameterName="@AwardID",Value= obj.AwardsID },
                new SqlParameter{ParameterName="@CreatedBy",Value= obj.CreatedBy },
                new SqlParameter{ParameterName="@ReturnVal",Value= obj.ReturnVal, Direction=System.Data.ParameterDirection.Output }
                    };
                var temp = _context.Database.ExecuteSqlRaw("dbo.USP_InsertOrUpdateDealerAwardsDetails @DealerAwardsID,@DealerCode,@AwardYear,@AwardID,@CreatedBy,@ReturnVal=@ReturnVal OUTPUT", Params.ToArray());
                
                return Params.ToList()[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("InsertOrUpdateDealersAwardDetails", "Error in Web API, InsertOrUpdateDealersAwardDetails", ex);
                return ex.Message.ToString();
            }
        }
        [HttpGet("GetFacilityStandard")]
        public string GetFacilityStandard()
        {
            try
            {
                List<GetFacilityStandardVM> list = new List<GetFacilityStandardVM>();
                list = _context.GetFacilityStandards.FromSqlRaw("dbo.USP_GetFacilityStandard").ToList();
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("GetFacilityStandard", "Error in Web API, GetFacilityStandard", ex);
                return ex.Message.ToString();
            }
        }
        [HttpPost("InsertFacilityStandard")]
        public string InsertFacilityStandard(InsertFacilityStandardVM obj)
        {
            try
            {
                //List<GetDealerAwardsDetails> list = new List<GetDealerAwardsDetails>();
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@FacilityStandard",Value=obj.FacilityStandardDetails },
                new SqlParameter{ParameterName="@RowDelim",Value= obj.RowDelim },
                new SqlParameter{ParameterName="@CellDelim",Value= obj.CellDelim },
                new SqlParameter{ParameterName="@CreatedBy",Value= obj.CreatedBy },
                new SqlParameter{ParameterName="@ReturnVal",Value= 0, Direction=System.Data.ParameterDirection.Output }
                    };
                var temp = _context.Database.ExecuteSqlRaw("dbo.USP_InsertFacilityStandard @FacilityStandard,@RowDelim,@CellDelim,@CreatedBy,@ReturnVal=@ReturnVal OUTPUT", Params.ToArray());

                return Params.ToList()[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("InsertFacilityStandard", "Error in Web API, InsertFacilityStandard", ex);
                return ex.Message.ToString();
            }
        }
        [HttpGet("GetStaffingStandard")]
        public string GetStaffingStandard()
        {
            try
            {
                List<GetStaffingStandardVM> list = new List<GetStaffingStandardVM>();
                list = _context.GetStaffingStandards.FromSqlRaw("dbo.USP_GetStaffingStandard").ToList();
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("GetStaffingStandard", "Error in Web API, GetStaffingStandard", ex);
                return ex.Message.ToString();
            }
        }
        [HttpPost("InsertStaffingStandard")]
        public string InsertStaffingStandard(InsertStaffingStandardVM obj)
        {
            try
            {
                //List<GetDealerAwardsDetails> list = new List<GetDealerAwardsDetails>();
                List<SqlParameter> Params = new List<SqlParameter>
                { new SqlParameter{ParameterName="@StaffingStandard",Value=obj.StaffingStandardDetails },
                new SqlParameter{ParameterName="@RowDelim",Value= obj.RowDelim },
                new SqlParameter{ParameterName="@CellDelim",Value= obj.CellDelim },
                new SqlParameter{ParameterName="@CreatedBy",Value= obj.CreatedBy },
                new SqlParameter{ParameterName="@ReturnVal",Value= 0, Direction=System.Data.ParameterDirection.Output }
                    };
                var temp = _context.Database.ExecuteSqlRaw("dbo.USP_InsertStaffingStandard @StaffingStandard,@RowDelim,@CellDelim,@CreatedBy,@ReturnVal=@ReturnVal OUTPUT", Params.ToArray());

                return Params.ToList()[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MCIDealer360Logger.GetMCIDealer360Logger().WriteLog("InsertStaffingStandard", "Error in Web API, InsertStaffingStandard", ex);
                return ex.Message.ToString();
            }
        }

    }
}
