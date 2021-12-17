using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public string Hours { get; set; }
        public string Description { get; set; }
        public Boolean ActiveStatus { get; set; }


       
    }
}
