using ArieotechLive.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration configuration;
        public DepartmentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
         
        //DEACTIVATE DEPARTMENT
        public void DeactivateDepartment(int DepartmentID)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("UPDATE [dbo].[Department] SET ActiveStatus=0 WHERE DepartmentID = @DepartmentID", new
                {
                    departmentID = DepartmentID
                });
            }
        }

        //GET ALL DEPARTMENT
        public IEnumerable<Department> GetAllDepartment()
        {



            IEnumerable<Department> department = new List<Department>();

            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                department = conn.Query<Department>("SELECT * FROM Department");
            }
            return department;
            //IEnumerable<Department> department = new List<Department>();
            //{  IEnumerable<Department> department = new List<Department>();
            //    //departmentID=department.DepartmentID = 1;
            //   // departmentName=department.DepartmentName = "pavn";

            //}

            //return department;



        }
        public void activateDepartment(int Id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute(" UPDATE [dbo].[Department] SET ActiveStatus=1 WHERE DepartmentId= @Id", new
                {
                    id = Id
                });
            }

        }
        //GET DEPARTMENT By DEPARTMENT NAME
        public Department GetDepartmentByDepartmentName(string DepartmentName)
        {
            Department department = new Department();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))

            {
                department = conn.Query<Department>("SELECT DepartmentName FROM [dbo].[Department] Where DepartmentName = @Firstname", new
                {
                    Firstname = DepartmentName,
                }).FirstOrDefault();
            }

            return department;
        }

        //GET DEPARTMENT By DEPARTMENTID
        public Department GetDepartmentById(int DepartmentID)
        {
            Department department = new Department();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                department = conn.Query<Department>(string.Format("SELECT * FROM [dbo].[Department] Where DepartmentID ={0}", DepartmentID)).FirstOrDefault();
            }

            return department;
        }
        public IEnumerable<Department> GetDepartmentByStatus(bool ActiveStatus)
        {
            IEnumerable<Department> department = new List<Department>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                department = conn.Query<Department>("SELECT * FROM [dbo].[Department] Where ActiveStatus=@ActiveStatus", new
                {
                    ActiveStatus = ActiveStatus,

                });
            }

            return department;
        }
        public int CountActiveDepartment(bool ActiveStatus)
        {
            int count;
            IEnumerable<Department> empdept = new List<Department>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {

                empdept = conn.Query<Department>("SELECT * from dbo.[Department] where ActiveStatus=@ActiveStatus", new
                {
                    ActiveStatus = ActiveStatus,
                }) ;

            }
         
            return empdept.Count();
        }

        //GET ALL EmployeeWithDepartment by using JOIN
        public IEnumerable<EmployeeWithDepartment> GetEmployeeWithDepartment(int DepartmentID)
        {
            IEnumerable<EmployeeWithDepartment> empdept = new List<EmployeeWithDepartment>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                empdept = conn.Query<EmployeeWithDepartment>(string.Format("SELECT Employee.*,Department.DepartmentName,Department.DepartmentDescription,Department.DepartmentHead FROM Employee INNER JOIN Department as Department ON Department.DepartmentID = Employee.DepartmentId where Employee.DepartmentID = {0}", DepartmentID));
            }


            return empdept;
        }


        //INSERT INTO DEPARTMENT
        public void InsertIntoDepartment(Department DepartmentInsert)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("INSERT INTO DEPARTMENT VALUES (@DepartmentName,@DepartmentDescription,@DepartmentHead,@CreatedBy,@ActiveStatus)", new
                    {
                        DepartmentName = DepartmentInsert.DepartmentName,
                        DepartmentDescription = DepartmentInsert.DepartmentDescription,
                        DepartmentHead = DepartmentInsert.DepartmentHead,
                        CreatedBy = DepartmentInsert.CreatedBy,
                        ActiveStatus=DepartmentInsert.ActiveStatus
                    });
                }
            }

            catch (Exception ex)
            {

            }
        }

        //UPDATE DEPARTMENT
        public void UpdateDepartment(Department DepartmentUpdate, int DepartmentID)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("update[dbo].[Department] set DepartmentName = @DepartmentName, DepartmentDescription= @DepartmentDescription, DepartmentHead= @DepartmentHead where DepartmentId=@DepartmentId", new

                    {
                        DepartmentID = DepartmentID,
                        DepartmentName = DepartmentUpdate.DepartmentName,
                        DepartmentDescription = DepartmentUpdate.DepartmentDescription,
                        DepartmentHead = DepartmentUpdate.DepartmentHead
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
