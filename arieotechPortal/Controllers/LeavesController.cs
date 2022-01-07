using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeavesController : ControllerBase
    {
        private readonly ILeavesRepository leavesRepository;
        private readonly ILoggerManager loggerManager;

        public LeavesController(ILeavesRepository leavesRepository, ILoggerManager loggerManager)
        {
            this.leavesRepository = leavesRepository;
            this.loggerManager = loggerManager;
        }

        //GET ALL LEAVES DETAILS 
        [HttpGet("/GetAllLeaves")]
        public ActionResult GetAllLeaves()
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all leaves called");
            IEnumerable<Leaves> leaves = new List<Leaves>();

            try
            {
                leaves = this.leavesRepository.GetAllLeaves();
                result = Ok(leaves);
            }

            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching leaves records-->{0}+,Details-->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }

        //INSERT/CREATE LEAVES
        [HttpPost("/InsertIntoLeaves")]
        public ActionResult InsertIntoLeaves(Leaves LeavesInsert)
        {
            LeavesInsert.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Insert Department called,DepartmentName:{0}", LeavesInsert));
                this.leavesRepository.InsertIntoLeaves(LeavesInsert);
                result = Ok();
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("This department already exist in the database,DepartmentName:{0}", LeavesInsert));
                result = new StatusCodeResult(500);
                ;
            }
            return result;


            //UPDATE LEAVES
            [HttpPut("/UpdateLeaves")]

            ActionResult UpdateLeaves([FromBody] Leaves LeavesUpdate, int LeavesID)
            {
                ActionResult result;
                try
                {
                    this.loggerManager.LogInfo(string.Format("Update Leaves called,LeaveID:{0}", LeavesUpdate.LeaveID));
                    this.leavesRepository.UpdateLeaves(LeavesUpdate, LeavesID);
                    result = Ok();
                }
                catch
                {
                    this.loggerManager.LogError(string.Format("This Leaves already exits in the Database:{0}", LeavesUpdate.LeaveID));
                    result = new StatusCodeResult(401);
                }
                return result;
            }
        }
    } }
