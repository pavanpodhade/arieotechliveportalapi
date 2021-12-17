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

namespace ArieotechLive.Controllers
{
   
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
   
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;
        private readonly ILoggerManager loggerManager;
        //private readonly IUserRepository userRepository;
        public ProjectController(IProjectRepository ProjectRepository, ILoggerManager loggerManager)
        {
            this.projectRepository = ProjectRepository;
            this.loggerManager = loggerManager;
            //this.userRepository = userRepository;
        }
        //Get all project
        [HttpGet("Getallproject")]
        public ActionResult GetAllProject()
        {
            ActionResult result;
            this.loggerManager.LogInfo("Get all Project called");
            IEnumerable<Project> Project = new List<Project>();
            try
            {
                Project = this.projectRepository.GetAllProject();
                result = Ok(Project);
            }

            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while fetching the Project records -->{0} +, Details -->{1}", ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        //Get Project by project id
        [HttpGet("/GeteProjectByID/{ProjectID}")]
        public ActionResult GetProjectById(int ProjectID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Get all project by ID is called,ID:{0}", ProjectID));
                Project project = new Project();
                project = this.projectRepository.GetProjectByID(ProjectID);
                result = Ok(project);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("User: {0} is not allowed for this operation get getproject by id", ProjectID));
            }
            return result;
        }
        //INSERT INTO PROJECT
        [HttpPost("/InsertIntoProject")]
        public ActionResult InsertIntoProject(Project Projectinsert)
        {
            Projectinsert.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Insert project called,ProjectName:{0}", Projectinsert.ProjectName));
                //Duplicate name
                Project projectFromDB = this.projectRepository.GetProjectByProjectName(Projectinsert.ProjectName);
                if (projectFromDB != null)  //1 != 0   0!=0
                {
                    this.loggerManager.LogInfo(string.Format("Project with ProjectName:{0} is already exists", Projectinsert.ProjectName));

                    var newresult = new
                    {
                        message = string.Format("{0} project name is already exits", Projectinsert.ProjectName) //employee exist,unable to delete project 
                    };

                    result = Conflict(newresult);
                }
                else
                {
                    this.projectRepository.InsertIntoProject(Projectinsert);
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("This Project already exits in the Database,name:{0}", Projectinsert.ProjectName));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        //update project by using project id
        [HttpPut("UpdateProject")]
        public ActionResult UpDateProject([FromBody] Project ProjectUpdate, int ProjectID)
        {
            ProjectUpdate.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Update project called,ProjectName:{0}", ProjectUpdate.ProjectName));
                //Duplicate
               // Project projectFromDB = this.projectRepository.GetProjectByProjectName(ProjectUpdate.ProjectName);
                //if (projectFromDB != null)
                //{
                //    this.loggerManager.LogInfo(string.Format("Project with ProjectName:{0} is already exists", ProjectUpdate.ProjectName));

                //    var newresult = new
                //    {
                //        message = string.Format("{0} project name is already exits", ProjectUpdate.ProjectName)
                //    };

                //    result = Conflict(newresult);
                //}
                //else
                //{

                    this.projectRepository.UpdateProject(ProjectUpdate, ProjectID);
                    result = Ok();
               // }
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("This Project already exits in the Database,name:{0}", ProjectUpdate.ProjectName));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        //deactivate project by using project id
        [HttpPut("Deletedeactivate")]
        public ActionResult DeactivateProject(int ProjectID)
        {
            //ProjectDelete.CreatedBy = Dns.GetHostName();
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Deactivate project by ProjectID is called,ProjectID:{0}", ProjectID));
                Employee empAvailability = this.projectRepository.GetEmployeeIDAgainstProjectID(ProjectID);
                if (empAvailability != null)
                {
                    this.loggerManager.LogInfo(string.Format("employee exist, unable to delete project:{0} is allready exits", ProjectID));
                    var newresult = new
                    {
                        message = string.Format("{0} ProjectID is having employee alloted", ProjectID)
                    };
                    this.loggerManager.LogInfo(string.Format("Deactivate project by ID is called,ID:{0}", ProjectID));
                    //this.projectRepository.DeactivateProject(ProjectID);
                    result = Conflict(newresult);
                }
                else
                {
                    this.projectRepository.DeactivateProject(ProjectID);
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("This Employee already exits in Database,ProjectId:{0}", ProjectID));
                result = new StatusCodeResult(401);
            }
            return result;
        }

        ////deactivate project by using project id
        //[HttpDelete]
        //public ActionResult DeactivateProject(int ProjectID)
        //{
        //    ActionResult result;
        //    try
        //    {
        //        this.loggerManager.LogInfo(string.Format("Deactivate project by ID is called,ID:{0}", ProjectID));
        //        this.projectRepository.DeactivateProject(ProjectID);

        //        result = new StatusCodeResult(200);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.loggerManager.LogError(string.Format("Action Sustained due to data dependency,id:{0}", ProjectID));
        //        result = new StatusCodeResult(401);
        //    }
        //    return result;
        //}

        //GetProjectbyprojectID given by user join
        //[HttpGet("/GetProjectbyprojectID/{ProjectID}")]
        //public ActionResult GetProjectByProjectID(string ProjectName)
        //{
        //    ActionResult result;
        //    try
        //    {
        //        this.loggerManager.LogInfo(string.Format("Get all project by ProjectName is called,ProjectName:{0}", ProjectName));
        //        Project project = new Project();
        //        project = this.projectRepository.GetProjectByProjectName(ProjectName);
        //        result = Ok(project);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new StatusCodeResult(401);
        //        this.loggerManager.LogError(string.Format("Department: {0} is not allowed for this operation get getdepartment by Name", ProjectName));
        //    }
        //    return result;
        //}


        //GetProjectbyprojectID given by user join
        [HttpGet("/GetEmployeeIDAgainstProjectID/{ProjectID}")]
        public ActionResult GetEmployeeIDAgainstProjectID(int ProjectID)
        {
            ActionResult result;
            try
            {
                this.loggerManager.LogInfo(string.Format("Get all project by ProjectName is called,ProjectID:{0}", ProjectID));
                Employee employee = new Employee();
                employee = this.projectRepository.GetEmployeeIDAgainstProjectID(ProjectID);
                result = Ok(employee);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
                this.loggerManager.LogError(string.Format("Department: {0} is not allowed for this operation get getdepartment by Id", ProjectID));
            }
            return result;
        }
    }
}
