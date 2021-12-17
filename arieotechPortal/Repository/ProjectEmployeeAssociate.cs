using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ArieotechLive.Repository
{
    public class ProjectEmployeeAssociate
    {
        public int PEAID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
    }
}