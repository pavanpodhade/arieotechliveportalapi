using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class Leaves
    {
        public int LeaveID { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveAddress { get; set; }
        public bool IsHalfDay { get; set; }
        public DateTime ResumeDate { get; set; }
        public int DepartmentId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; internal set; }
    }
}
