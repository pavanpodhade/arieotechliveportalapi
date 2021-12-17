using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int Id);
        void InsertEmployee(Employee employee);
        Employee GetEmployeeByUserId(int UserId);
        void deactivateEmployee(int Id);
        void UpdateEmployee(Employee employee, int Id);
      //void ImageUpload(int eventId, string fileName, object userId);
    }
}
