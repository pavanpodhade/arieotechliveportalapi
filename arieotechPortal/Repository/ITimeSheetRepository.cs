using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public interface ITimeSheetRepository
    {
        IEnumerable<TimeSheet> GetAllTimeSheet();
        TimeSheet GetTimeSheetByID(int ProjectID);
        TimeSheet GetTimeSheetByEmployeeID(int EmployeeID);
        void InsertIntoTimeSheet(TimeSheet timesheetinsert);
        void UpdateTimeSheet(TimeSheet timesheetupdate, int Id);
        void DeactivateTimeSheet(int Id);
    }
}
