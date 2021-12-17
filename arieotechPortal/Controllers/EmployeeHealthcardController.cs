using Microsoft.AspNetCore.Mvc;
using ArieotechLive.Model;
using ArieotechLive.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class EmployeeHealthcardController : Controller
    {
        private readonly IEmployeeHealthCardRepository employeeHealthCardRepository;
        private readonly ILoggerManager loggerManager;
        private string message;

        public EmployeeHealthcardController(IEmployeeHealthCardRepository employeeHealthCardRepository,ILoggerManager loggerManager)
        {
            this.employeeHealthCardRepository = employeeHealthCardRepository;
            this.loggerManager = loggerManager;
        }
        //getallEmployeehealthcard
        [HttpGet("/GetallEmployeeHealthCardRepository")]

        public IActionResult GetAllEmployeeHealthCardRepository()
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all EmployeeHealthCardRepository called ");
            IEnumerable<EmployeeHealthCard> employeeHealthCards = new List<EmployeeHealthCard>();

            try
            {
                employeeHealthCards = this.employeeHealthCardRepository.GetAllEmployeehealthcard();

                result = Ok(employeeHealthCards);
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching the Employeehealthcard records -->{0} +, Details -->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;

        
        }
        //GET EmployeeHealthCard BY EmployeeHealthCard ID
        [HttpGet("/GetEmployeeHealthCardById/{EmployeeHealthCardId}")]
        public ActionResult GetEmployeeHealthCardById(int EmployeeHealthCardId)
        {
            ActionResult result;

            try
            {
                this.loggerManager.LogInfo(string.Format("Get all department by id is called,id:{0}", EmployeeHealthCardId));
                EmployeeHealthCard employeeHealthCard = new EmployeeHealthCard();

                employeeHealthCard = this.employeeHealthCardRepository.GetEmployeeHealthCardById(EmployeeHealthCardId);
                result = Ok(employeeHealthCard);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("EmployeeHealthCard: {0} is not allowed for this operation get EmployeeHealthCardId by id", EmployeeHealthCardId));
            }
            return result;
        }
        //GET EmployeeHealthCard By Name
        [HttpGet("/GetEmployeeHealthCardByName")]
        public ActionResult GetEmployeeHealthCardByIName(string EmployeeHealthCardtName)
        {
            ActionResult result;

            try
            {
                this.loggerManager.LogInfo(string.Format("Get all Employeehealthcard by EmployeehealthcardName is called,EmployeehealthcardName:{0}", EmployeeHealthCardtName));
                EmployeeHealthCard employeeHealthCard = new EmployeeHealthCard();

                employeeHealthCard = this.employeeHealthCardRepository.GetDepartmentByDepartmentName(EmployeeHealthCardtName);
                result = Ok(employeeHealthCard);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("EmployeeHealthCard: {0} is not allowed for this operation get EmployeeHealthCard by Name", EmployeeHealthCardtName));
            }
            return result;
        }

        //INSERT INTO EmployeeHealthCard 
        [HttpPost("/InsertIntoEmployeeHealthCard/")]
        public ActionResult InsertIntoEmployeeHealthCard(EmployeeHealthCard EmployeeHealthCardInsert)
        {
            //DepartmentInsert.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Insert employeehealthcard called,employeehealthcardName:{0}", EmployeeHealthCardInsert.First_Name));
                
                    this.employeeHealthCardRepository.InsertIntoEmployeeHealthCard(EmployeeHealthCardInsert);
                    result = Ok();
               
            }
            catch (Exception ex)
            {
               
                this.loggerManager.LogError(string.Format("This employeehealthcard already exist in the database,employeehealthcard:{0}", EmployeeHealthCardInsert.First_Name));
                result = new StatusCodeResult(500);
                ;
            }
           // message = string.Format("{1} save the record", EmployeeHealthCardInsert.First_Name);
            return result;
        }
        //UPDATE EmployeeHealthCard
        [HttpPut]
        [Route("UpdatedEmployeeHealthCard")]
        public ActionResult UpdateEmployeeHealthCard([FromBody] EmployeeHealthCard EmployeeHealthCardUpdate, int EmployeeHealthCardID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Update EmployeeHealthCard called,EmployeeHealthCardName:{0}", EmployeeHealthCardUpdate.First_Name));
                this.employeeHealthCardRepository.UpdateEmpHealthCard(EmployeeHealthCardUpdate, EmployeeHealthCardID);
                //if (departmentFromDB != null)
                //{
                //    this.loggerManager.LogInfo(string.Format("Department with DepartmentName:{0} is already exists", DepartmentUpdate.DepartmentName));
                //    var newresult = new
                //    {
                //        message = string.Format("{0} department name is already exits.", DepartmentUpdate.DepartmentName)
                //    };
                //    result = Conflict(newresult);
                //}
                //else
                //{
                this.employeeHealthCardRepository.UpdateEmpHealthCard(EmployeeHealthCardUpdate, EmployeeHealthCardID);
                result = Ok();
                //}
            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("This Department already exits in the Database:{0}", EmployeeHealthCardUpdate.First_Name));
                result = new StatusCodeResult(401);
                ;
            }
            return result;
        }
        // DEACTIVATE DEPARTMENT
        [HttpPut]
        [Route("/Deletebydeactivate1")]
        public ActionResult DeactivateEmployeeHC(int EmployeeHealthCardID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Deactivate EmployeeHC by id is called,id:{0}", EmployeeHealthCardID));
                this.employeeHealthCardRepository.DeactivateEmployeeHC(EmployeeHealthCardID);
                result = new StatusCodeResult(200);

            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("Action Sustained due to data dependency,id:{0}", EmployeeHealthCardID));
                result = new StatusCodeResult(401);
            }
            return result;
        }


    }
}
