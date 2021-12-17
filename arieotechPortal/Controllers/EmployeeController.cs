using ArieotechLive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArieotechLive.Model;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace WEB_API_PRACTICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILoggerManager loggerManager;

        public EmployeeController(IEmployeeRepository employeeRepository, ILoggerManager loggerManager)

        {
            this.employeeRepository = employeeRepository;
            this.loggerManager = loggerManager;
        }

        [HttpGet("/GetAllEmployees")]

        public ActionResult GetAllEmployees()
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all employee called");
            IEnumerable<Employee> employee = new List<Employee>();
            try
            {
                employee = this.employeeRepository.GetAllEmployees();
                result = Ok(employee);
            }

            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching the Employee records -->{0} +, Details -->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }


        #region GetEmployeeById 
        [HttpGet("/getemployee/{Id}")]
        public ActionResult GetEmployeeById(int Id)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Get Employee by id is called,id:{0}", Id));
                Employee employee = new Employee();
                employee = this.employeeRepository.GetEmployeeById(Id);
                result = Ok(employee);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getEmployee by id", Id));
            }
            return result;
        }
        #endregion

        #region GetEmployeeByUserId
        [HttpGet("/getemployeebyuserid/{UserId}")]
        public ActionResult GetEmployeeByUserId(int UserId)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Get Employee by id is called,id:{0}", UserId));
                Employee employee = new Employee();
                employee = this.employeeRepository.GetEmployeeByUserId(UserId);
                result = Ok(employee);
            }
            catch (Exception e)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getEmployee by UserId", UserId));
            }
            return result;
        }
        #endregion

        #region deactivateEmployee

        [HttpPut]
        [Route("DeactivateEmployee")]
        public ActionResult DeactivateEmployee(int Id)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Deactivate employee by id is called,id:{0}", Id));
                this.employeeRepository.deactivateEmployee(Id);
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

        #region InsertEmployee
        [HttpPost("/insertEmployees")]

        public ActionResult InsertEmployee([FromBody] Employee employee)
        {
           // employee.CreatedBy = Dns.GetHostName();
            ActionResult result;

            try
            {
                this.loggerManager.LogInfo(string.Format("Insert employee called,name:{0}", employee.FirstName));
                this.employeeRepository.InsertEmployee(employee);
                result = Ok();

            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("This Employee already exits in the Database,name:{0}", employee.FirstName));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        #endregion

        #region UpdateEmployee
        [HttpPut]
        [Route("EditEmployee")]
        public ActionResult UpdateEmployee([FromBody] Employee employee, int Id)
        {

            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Update Employee called,name:{0}", employee.FirstName));
                this.employeeRepository.UpdateEmployee(employee, Id);
                result = Ok();
            }
            catch (Exception e)
            {
                this.loggerManager.LogError(string.Format("This employee already exits in the Database,name:{0}", employee.FirstName));
                result = new StatusCodeResult(401);
            }
            return result;
        }
        [HttpPost]
        [Route("Imagesupload")]
        public IActionResult Upload(IFormFile newfile)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


      /*  [HttpPut("[Action]/{id}")]
        public async Task<ActionResult> LinkItemToIcon(int id, IFormFile file)
        {
            //your operation
        }
*/




        //[Route("/api/User/upload/profile/image")]
        //[HttpPost]
        //   public async Task<IActionResult> Post(IFormFile FormFile, int EventId)
        //   {
        //       try
        //       {

        //           using (var memoryStream = new MemoryStream())
        //           {
        //               await FormFile.CopyToAsync(memoryStream); // Upload the file if less than 2 MB
        //                                                         //if (memoryStream.Length < 2097152)
        //                                                         //{
        //                                                         //string FileName = null;
        //               //var userObj = Common.GetUserObject(this.HttpContext);
        //               employeeRepository.ImageUpload(EventId, FormFile.FileName);
        //            //   await AmazonUploader.UploadFileAsync(memoryStream, "gleanpix", $"events/{EventId}", FormFile.FileName, getAwsCredDetails());

        //           }
        //           this.loggerManager.LogInfo($"EventController:Image or images Uploaded for entity {EventId} successfully"); return Ok(new { value = "success" });
        //       }
        //       catch (Exception ex)
        //       {
        //           this.loggerManager.LogError($"Fail to upload image for an event{EventId} with --{ex}");
        //           return StatusCode(StatusCodes.Status500InternalServerError);
        //       }
        //   }




    }
}
#endregion


