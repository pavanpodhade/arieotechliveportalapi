using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Web.Helpers;
using Newtonsoft.Json;
using Grpc.Core;

namespace ArieotechLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILoggerManager loggerManager;

        public DepartmentController(IDepartmentRepository departmentRepository, ILoggerManager loggerManager)
        {
            this.departmentRepository = departmentRepository;
            this.loggerManager = loggerManager;
        }
        //GET ALL DEPARTMENT 
        [HttpGet("/GetAllDepartment")]
        public ActionResult GetAllDepartment()
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all department called");
            IEnumerable<Department> department = new List<Department>();
            try
            {
                department = this.departmentRepository.GetAllDepartment();
                result = Ok(department);
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching the Department records -->{0} +, Details -->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }

        //GET DEPARTMENT BY DEPARTMENT ID
        [HttpGet("/GetDepartmentById/{DepartmentId}")]
        public ActionResult GetDepartmentById(int DepartmentId)
        {
            ActionResult result;

            try
            {
                this.loggerManager.LogInfo(string.Format("Get all department by id is called,id:{0}", DepartmentId));
                Department department = new Department();

                department = this.departmentRepository.GetDepartmentById(DepartmentId);
                result = Ok(department);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getdepartment by id", DepartmentId));
            }
            return result;
        }
        [HttpGet("/GetDepartmentByStatus")]
        public ActionResult GetDepartmentByStatus(bool ActiveStatus)
        {
            ActionResult result;
            IEnumerable<Department> department = new List<Department>();
            try
            {
               // this.loggerManager.LogInfo(string.Format("Get all department by id is called,id:{0}", ActiveStatus));
                

                department = this.departmentRepository.GetDepartmentByStatus(ActiveStatus);
                result = Ok(department);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
               // this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getdepartment by id", ActiveStatus));
            }
            return result;
        }

        //GET DEPARTMENT BY DEPARTMENT NAME
        [HttpGet("/GetDepartmentByName/{DepartmentName}")]
        public ActionResult GetDepartmentByIName(string DepartmentName)
        {
            ActionResult result;

            try
            {
                this.loggerManager.LogInfo(string.Format("Get all department by DepartmentName is called,DepartmentName:{0}", DepartmentName));
                Department department = new Department();

                department = this.departmentRepository.GetDepartmentByDepartmentName(DepartmentName);
                result = Ok(department);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("Department: {0} is not allowed for this operation get getdepartment by Name", DepartmentName));
            }
            return result;
        }

        //INSERT INTO DEPARTMENT (DUPLICATE DEPARTMENT NAME IS RESTRICTED)
        [HttpPost("/InsertIntoDepartment/")]
        public ActionResult InsertIntoDepartment(Department DepartmentInsert)
        {
            //DepartmentInsert.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Insert Department called,DepartmentName:{0}", DepartmentInsert.DepartmentName));
                Department departmentFromDB = this.departmentRepository.GetDepartmentByDepartmentName(DepartmentInsert.DepartmentName);
                if (departmentFromDB != null) //To check duplicate values
                {
                    this.loggerManager.LogInfo(string.Format("Department with DepartmentName:{0} is already exists", DepartmentInsert.DepartmentName));
                    var newresult = new
                    {
                        message = string.Format("{0} department name is already exits.", DepartmentInsert.DepartmentName)
                    };
                    result = Conflict(newresult);
                }


                else
                {
                    this.departmentRepository.InsertIntoDepartment(DepartmentInsert);
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("This department already exist in the database,DepartmentName:{0}", DepartmentInsert.DepartmentName));
                result = new StatusCodeResult(500);
                ;
            }
            return result;
        }

        //UPDATE DEPARTMENT
        [HttpPut]
        [Route("UpdatedDepartment")]
        public ActionResult UpdateDepartment([FromBody] Department DepartmentUpdate, int DepartmentID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Update Department called,DepartmentName:{0}", DepartmentUpdate.DepartmentName));
                //this.departmentRepository.UpdateDepartment(DepartmentUpdate, DepartmentID);
                Department departmentFromDB = this.departmentRepository.GetDepartmentByDepartmentName(DepartmentUpdate.DepartmentName);
               // Department departmentDesc = this.departmentRepository.GetDepartmentByDepartmentName(DepartmentUpdate.DepartmentDescription);
                if (departmentFromDB != null)
                {
                    this.loggerManager.LogInfo(string.Format("Department with DepartmentName:{0} is already exists", DepartmentUpdate.DepartmentName));
                    var newresult = new
                    {
                        message = string.Format("{0} department name is already exits.", DepartmentUpdate.DepartmentName)
                    };
                    result = Conflict(newresult);
                }
                //if (departmentDesc == null)
                //{
                //    this.loggerManager.LogInfo(string.Format("Department with DepartmentName:{0} is already exists", DepartmentUpdate.DepartmentName));
                //    var newresult = new
                //    {
                //        message = string.Format("{0} department name is already exits.", DepartmentUpdate.DepartmentName)
                //    };
                //    result = Conflict(newresult);
                //}

                else
                {
                    this.departmentRepository.UpdateDepartment(DepartmentUpdate, DepartmentID);
                    result = Ok();
                }
            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("This Department already exits in the Database:{0}", DepartmentUpdate.DepartmentName));
                result = new StatusCodeResult(401);
                ;
            }
            return result;
        }

        // DEACTIVATE DEPARTMENT
        [HttpPut]
        [Route("/Deletebydeactivate")]
        public ActionResult DeactivateDepartment(int DepartmentID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Deactivate department by id is called,id:{0}", DepartmentID));
                this.departmentRepository.DeactivateDepartment(DepartmentID);
                result = new StatusCodeResult(200);

            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("Action Sustained due to data dependency,id:{0}", DepartmentID));
                result = new StatusCodeResult(401);
            }
            return result;
        }
        #region activateEmployee

        [HttpPut]
        [Route("activateDepartment")]
        public ActionResult activateDepartment(int Id)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("activate employee by id is called,id:{0}", Id));
                this.departmentRepository.activateDepartment(Id);
                result = new StatusCodeResult(200);

            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("Action Sustained due to data dependency,id:{0}", Id));
                result = new StatusCodeResult(401);
            }
            return result;
        }
        #endregion


        //GET ALL EmployeeWithDepartment by using JOIN
        [HttpGet]
        [Route("/GetEmployeeWithDepartment")]
        public ActionResult GetEmployeeWithDepartment(int DepartmentID)
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all employee with department called");
            IEnumerable<EmployeeWithDepartment> empdept = new List<EmployeeWithDepartment>();
            try
            {
                empdept = this.departmentRepository.GetEmployeeWithDepartment(DepartmentID);
                result = Ok(empdept);
            }

            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching the employee departments records -->{0} +, Details -->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        [HttpGet]
        [Route("/CountActive")]
        public ActionResult CountActiveDepartment(bool ActiveStatus)
        {
            ActionResult result;
            try
            {
                int count;
                count=this.departmentRepository.CountActiveDepartment(ActiveStatus);
                result = Ok(count);

            }
            catch (Exception e)
            {
                //this.loggerManager.LogError(string.Format("Action Sustained due to data dependency,id:{0}", DepartmentID));
                result = new StatusCodeResult(401);
            }
            return result;
        
        }
    }
}