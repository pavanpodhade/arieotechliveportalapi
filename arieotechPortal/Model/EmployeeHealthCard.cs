using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class EmployeeHealthCard
    {
        public int EmpHealthCardID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Relation { get; set; }
        public string AdharCard_No {get;set;}
        public bool ActiveStatus { get; set; }
    }
}
