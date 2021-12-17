using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetRepository timeSheetRepository;
        public TimeSheetController(ITimeSheetRepository timeSheetRepository)
        {
            this.timeSheetRepository = timeSheetRepository;       
        }
        //GetTimeSheet
        [HttpGet("GetallTimeSheet")]
        public ActionResult GetAllTimeSheet()
        {
            ActionResult result;
            IEnumerable<TimeSheet> timeSheet= new List<TimeSheet>();
            try
            {        
                timeSheet =this.timeSheetRepository.GetAllTimeSheet();
                result = Ok(timeSheet);
            }

            catch (Exception ex)
            {              
                result = new StatusCodeResult(500);
            }
            return result;
        }
        [HttpGet("GeteTimeSheetByID")]
        public ActionResult GetTimeSheetById(int Id)
        {
            ActionResult result;
            try
            {
                //this.loggerManager.LogInfo(string.Format("Get all project by ID is called,ID:{0}", ProjectID));
                TimeSheet timeSheet = new TimeSheet();
                timeSheet = this.timeSheetRepository.GetTimeSheetByID(Id);
                result = Ok(timeSheet);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                //this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getproject by id", ProjectID));
            }
            return result;
        }

        [HttpGet("/GeteTimeSheetByEmployeeID/{EmployeeID}")]
        public ActionResult GetTimeSheetByEmployeeID(int EmployeeID)
        {
            ActionResult result;
            try
            {
                //this.loggerManager.LogInfo(string.Format("Get all project by ID is called,ID:{0}", ProjectID));
                TimeSheet timeSheet = new TimeSheet();
                timeSheet = this.timeSheetRepository.GetTimeSheetByEmployeeID(EmployeeID);
                result = Ok(timeSheet);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                //this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getproject by id", ProjectID));
            }
            return result;
        }
        
        [HttpPost("InsertIntoTimeSheet")]
        public ActionResult InsertTimeSheet( TimeSheet timeSheet)
        {           
            ActionResult result;
            try
            {              
                this.timeSheetRepository.InsertIntoTimeSheet(timeSheet);
                result = Ok();
            }
            catch (Exception ex)
            {                
                result = new StatusCodeResult(500);
            }
            return result;
        }
        //UpdateEmployee
        [HttpPut]
        [Route("UpdateTimeSheet")]
        public ActionResult UpdateTimeSheet([FromBody] TimeSheet timeSheet, int Id)
        {
            ActionResult result;
            try
            {              
                this.timeSheetRepository.UpdateTimeSheet(timeSheet, Id);
                result = Ok();
            }
            catch (Exception ex)
            {               
                result = new StatusCodeResult(500);
            }
            return result;
        }
        //deactivateEmployee
        [HttpDelete]
        public ActionResult DeactivateTimeSheet(int Id)
        {
            ActionResult result;
            try
            {
                
                this.timeSheetRepository.DeactivateTimeSheet(Id);
                result = new StatusCodeResult(200);

            }
            catch (Exception e)
            {              
                result = new StatusCodeResult(401);
            }
            return result;
        }
    }
}