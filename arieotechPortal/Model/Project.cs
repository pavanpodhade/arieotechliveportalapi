using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class Project
    {
        internal string CreatedBy;
        public int ProjectID { get; set; }
        public int ProjectManagerID { get; set; }
        public int DepartmentID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStartDate{ get; set; }
        public string ProjectCountry { get; set; }
        public string ProjectContactPerson { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
