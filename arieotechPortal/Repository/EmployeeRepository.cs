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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #region deactivateEmployee
        public void deactivateEmployee(int Id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute(" UPDATE [dbo].[Employee] SET Active=0 WHERE Id = @Id", new
                {
                    id = Id
                });
            }
        }
        #endregion

        ////GET ALL EMPLOYEE
        //public IEnumerable<Employee> GetAllEmployee()
        //{
        //    IEnumerable<Employee> employee = new List<Employee>();
        //    using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
        //    {
        //        employee = conn.Query<Employee>("Select * from [dbo].[Employee]");
        //    }
        //    return employee;
        //}


        #region GetEmployeeById
        public Employee GetEmployeeById(int Id)
        {
            Employee employee = new Employee();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employee = conn.Query<Employee>(" select * from  [dbo].[Employee] (nolock) where Id=@id", new { id = Id }).FirstOrDefault();
            }
            return employee;
        }
        #endregion

        #region GetEmployeeByUserId
        public Employee GetEmployeeByUserId(int UserId)
        {
            Employee employee = new Employee();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employee = conn.Query<Employee>("SELECT * FROM [dbo].[Employee] Where UserId =@UserId", new { UserId = UserId }).FirstOrDefault();
            }
            return employee;
        }
        #endregion

        #region InsertEmployee
        public void InsertEmployee(Employee employee)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("insert into [dbo].[Employee] values (@UserId,@EmployeeCode,@FirstName,@MiddleName,@LastName,@OfficeEmail,@Gender,@JoiningDate,@JoiningLocation,@PersonalEmail,@PhoneNumber,@EmergencyContactPerson,@EmergencyPhoneNumber,@BirthDate,@CurrentAddress,@PermanentAddress,@Designation,@Passport,@PassportNumber,@PANNumber,@AdharNumber,@UANNumber,@BloodGroup,@TShirtSize,@MaritalStatus,@MotherName,@MotherDOB,@FatherName,@FatherDOB,@SpouseName,@SpouseDOB,@Active,@CreateOn,@CreatedBy,@DepartmentId)", new
                    {

                        UserId = employee.UserId,
                        EmployeeCode = employee.EmployeeCode,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        OfficeEmail = employee.OfficeEmail,
                        Gender = employee.Gender,
                        JoiningDate = employee.JoiningDate,
                        JoiningLocation = employee.JoiningLocation,
                        PersonalEmail = employee.PersonalEmail,
                        PhoneNumber = employee.PhoneNumber,
                        EmergencyContactPerson = employee.EmergencyContactPerson,
                        EmergencyPhoneNumber = employee.EmergencyPhoneNumber,
                        BirthDate = employee.BirthDate,
                        CurrentAddress = employee.CurrentAddress,
                        PermanentAddress = employee.PermanentAddress,
                        Designation = employee.Designation,
                        Passport = employee.Passport,
                        PassportNumber = employee.PassportNumber,
                        PANNumber = employee.PANNumber,
                        AdharNumber = employee.AdharNumber,
                        UANNumber = employee.UANNumber,
                        BloodGroup = employee.BloodGroup,
                        TShirtSize = employee.TShirtSize,
                        MaritalStatus = employee.MaritalStatus,
                        MotherName = employee.MotherName,
                        MotherDOB = employee.MotherDOB,
                        FatherName = employee.FatherName,
                        FatherDOB = employee.FatherDOB,
                        SpouseName = employee.SpouseName,
                        SpouseDOB = employee.SpouseDOB,
                        Active = employee.Active,
                        CreateOn = employee.CreateOn,
                        CreatedBy = employee.CreatedBy,
                        DepartmentId = employee.DepartmentId,
                        //ImagesPath=employee.ImagesPath

                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region UpdateEmployee
        public void UpdateEmployee(Employee employee, int Id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[Employee] set UserId=@UserId,EmployeeCode=@EmployeeCode,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,OfficeEmail=@OfficeEmail,Gender=@Gender,JoiningDate=@JoiningDate,JoiningLocation=@JoiningLocation,PersonalEmail=@PersonalEmail,PhoneNumber=@PhoneNumber,EmergencyContactPerson=@EmergencyContactPerson,EmergencyPhoneNumber=@EmergencyPhoneNumber,BirthDate=@BirthDate,CurrentAddress=@CurrentAddress,PermanentAddress=@PermanentAddress,Designation=@Designation,Passport=@Passport,PassportNumber=@PassportNumber,PANNumber=@PANNumber,AdharNumber=@AdharNumber,UANNumber=@UANNumber,BloodGroup=@BloodGroup,TShirtSize=@TShirtSize,MaritalStatus=@MaritalStatus,MotherName=@MotherName,MotherDOB=@MotherDOB,FatherName=@FatherName,FatherDOB=@FatherDOB,SpouseName=@SpouseName,SpouseDOB=@SpouseDOB,Active=@Active,CreateOn=@CreateOn,CreatedBy=@CreatedBy where Id=@Id", new
                {
                    Id = Id,
                    UserId = employee.UserId,
                    EmployeeCode = employee.EmployeeCode,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    OfficeEmail = employee.OfficeEmail,
                    Gender = employee.Gender,
                    JoiningDate = employee.JoiningDate,
                    JoiningLocation = employee.JoiningLocation,
                    PersonalEmail = employee.PersonalEmail,
                    PhoneNumber = employee.PhoneNumber,
                    EmergencyContactPerson = employee.EmergencyContactPerson,
                    EmergencyPhoneNumber = employee.EmergencyPhoneNumber,
                    BirthDate = employee.BirthDate,
                    CurrentAddress = employee.CurrentAddress,
                    PermanentAddress = employee.PermanentAddress,
                    Designation = employee.Designation,
                    Passport = employee.Passport,
                    PassportNumber = employee.PassportNumber,
                    PANNumber = employee.PANNumber,
                    AdharNumber = employee.AdharNumber,
                    UANNumber = employee.UANNumber,
                    BloodGroup = employee.BloodGroup,
                    TShirtSize = employee.TShirtSize,
                    MaritalStatus = employee.MaritalStatus,
                    MotherName = employee.MotherName,
                    MotherDOB = employee.MotherDOB,
                    FatherName = employee.FatherName,
                    FatherDOB = employee.FatherDOB,
                    SpouseName = employee.SpouseName,
                    SpouseDOB = employee.SpouseDOB,
                    Active = employee.Active,
                    CreateOn = employee.CreateOn,
                    CreatedBy = employee.CreatedBy

                });
            }
        }



        IEnumerable<Employee> IEmployeeRepository.GetAllEmployees()
        {
            IEnumerable<Employee> employee = new List<Employee>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employee = conn.Query<Employee>("Select * from [dbo].[Employee]");
            }
            return employee;
        }
        #endregion
    }

}