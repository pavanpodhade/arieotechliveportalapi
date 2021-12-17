using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class EmployeeWithDepartment
    {
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public string DepartmentHead { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string OfficeEmail { get; set; }
        public string Gender { get; set; }
        public DateTime JoiningDate { get; set; }
        public string JoiningLocation { get; set; }
        public string PersonalEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Designation { get; set; }
        public Boolean Passport { get; set; }
        public string PassportNumber { get; set; }
        public string PANNumber { get; set; }
        public string AdharNumber { get; set; }
        public string UANNumber { get; set; }
        public string BloodGroup { get; set; }
        public string TShirtSize { get; set; }
        public string MaritalStatus { get; set; }
        public string MotherName { get; set; }
        public DateTime MotherDOB { get; set; }
        public string FatherName { get; set; }
        public DateTime FatherDOB { get; set; }
        public string SpouseName { get; set; }
        public DateTime SpouseDOB { get; set; }
        public Boolean Active { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public int DepartmentId { get; set; }
    }
}
