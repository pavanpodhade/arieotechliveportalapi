using ArieotechLive.Model;
using System.Collections.Generic;


namespace ArieotechLive.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartment();

        Department GetDepartmentById(int DepartmentID);

        Department GetDepartmentByDepartmentName(string DepartmentName);

        void InsertIntoDepartment(Department DepartmentInsert);

        void UpdateDepartment(Department DepartmentUpdate,int DepartmentID);

        IEnumerable<EmployeeWithDepartment> GetEmployeeWithDepartment(int DepartmentID);

        void DeactivateDepartment(int DepartmentID);
    }
}
