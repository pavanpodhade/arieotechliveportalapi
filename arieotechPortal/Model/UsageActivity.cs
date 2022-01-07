using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class UsageActivity
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string LoginSessionId { get; set; }

        
        public int ActivityId { get; set; }
        public DateTime ActivityDate { get; set; }



        public int ActivityValue { get; set; }
    }
}
