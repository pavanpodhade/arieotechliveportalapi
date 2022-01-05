using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArieotechLive.Model
{
    public class AuthResult
    {

        public bool IsLoginSuccessful { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public string LoginSessionId { get; set; }

        
    

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public int RoleId { get; set; }

        public string email { get; set; }
        
    }
}
