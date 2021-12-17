using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class Department
    {
        //variable must start with lower case letter
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public string DepartmentHead { get; set; }
        public string CreatedBy { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
