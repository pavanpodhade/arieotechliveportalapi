using ArieotechLive.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace ArieotechLive.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration configuration;
        public ProjectRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //Get all project
        public IEnumerable<Project> GetAllProject()
        {
            IEnumerable<Project> Project = new List<Project>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                Project = conn.Query<Project>("SELECT * FROM Project");
            }
            return Project;
        }
        //duplicate project name 
        public Project GetProjectByProjectName(string ProjectName)
        {
            Project project = new Project();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("connectionString").GetSection("DefaultConnection").Value))
            {
                project = conn.Query<Project>("SELECT * FROM [dbo].[Project] Where ProjectName = @PRJCTN", new
                {
                    PRJCTN = ProjectName,
                }).FirstOrDefault();
                return project;
            }
        }
        //Get by id
        public Project GetProjectByID(int ProjectID)
        {
            Project project = new Project();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                project = (Project)conn.Query<Project>(string.Format(" select * from [ArieotechLive1].[dbo].[Project] where ProjectID ={0}", ProjectID)).FirstOrDefault();
            }
            return project;
        }
        //INSERT INTO PROJECT
        public void InsertIntoProject(Project projectinsert)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("INSERT INTO Project VALUES (@ProjectManagerID,@DepartmentID,@ProjectName,@ProjectDescription,@ProjectStartDate,@ProjectCountry,@ProjectContactPerson,@ActiveStatus)", new
                    {
                        ProjectManagerID = projectinsert.ProjectManagerID,
                        DepartmentID = projectinsert.DepartmentID,
                        ProjectName = projectinsert.ProjectName,
                        ProjectDescription = projectinsert.ProjectDescription,
                        ProjectStartDate = projectinsert.ProjectStartDate,
                        ProjectCountry = projectinsert.ProjectCountry,
                        ProjectContactPerson = projectinsert.ProjectContactPerson,
                        ActiveStatus = projectinsert.ActiveStatus
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        //update project by using project id
        public void UpdateProject(Project projectupdate, int ProjectID)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update[dbo].[Project] set ProjectManagerID= @ProjectManagerID,DepartmentID= @DepartmentID,ProjectName= @ProjectName,ProjectDescription= @ProjectDescription,ProjectStartDate= @ProjectStartDate,ProjectCountry= @ProjectCountry,ProjectContactPerson= @ProjectContactPerson where ProjectId= @ProjectId", new
                {
                    ProjectID = ProjectID,
                    ProjectManagerID = projectupdate.ProjectManagerID,
                    DepartmentID = projectupdate.DepartmentID,
                    ProjectName = projectupdate.ProjectName,
                    ProjectDescription = projectupdate.ProjectDescription,
                    ProjectStartDate = projectupdate.ProjectStartDate,
                    ProjectCountry = projectupdate.ProjectCountry,
                    ProjectContactPerson = projectupdate.ProjectContactPerson
                });
            }
        }

        //Deactive project
        public void DeactivateProject(int ProjectID)
        {
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("UPDATE [dbo].[Project] SET ActiveStatus=0 WHERE ProjectID = @ProjectID", new
                    {
                        projectID = ProjectID
                    });
                }
            }
        }
        //Get EmployeeId agiantsProjectid
        public Employee GetEmployeeIDAgainstProjectID(int ProjectID)
        {
            Employee employee = new Employee();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employee = (Employee)conn.Query<Employee>(string.Format(" select EmployeeID from [ArieotechLive1].[dbo].[ProjectEmployeeAssociate] where ProjectID ={0}", ProjectID)).FirstOrDefault();
            }
            return employee;
        }
    }
}
