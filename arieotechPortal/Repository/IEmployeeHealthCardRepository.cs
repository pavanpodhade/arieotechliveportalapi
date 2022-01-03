using ArieotechLive.Model;
using System.Collections.Generic;

namespace ArieotechLive.Repository
{
    public interface IEmployeeHealthCardRepository
    {
        IEnumerable<EmployeeHealthCard> GetAllEmployeehealthcard();
        EmployeeHealthCard GetDepartmentByDepartmentName(string FName);
        EmployeeHealthCard GetEmployeeHealthCardById(int EmployeeHealthCardID);
        EmployeeHealthCard GetEmployeeHealthCardByRelation(string Relation);
        void InsertIntoEmployeeHealthCard(EmployeeHealthCard EmployeeHealthCardInsert);
        void UpdateEmpHealthCard(EmployeeHealthCard EmployeeHealthCardUpdate, int EmployeeHealthCardID);
        void DeactivateEmployeeHC(int EmployeeHealthCardID);
        EmployeeHealthCard GetEmployeeHCByAdharCard_No(string AdharCard_No);
    }
}
