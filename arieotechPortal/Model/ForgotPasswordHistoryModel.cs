using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class ForgotPasswordHistoryModel
    {

        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string ForgotPasswordToken { get; set; }
        public int Active { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
