using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octopus.Client.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepositiory userRepository;  
        public UserController(IUserRepositiory userRepository)
        {
            this.userRepository = userRepository;
        }
        [Route("GetAllUsers()")]
        [Authorize]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            ActionResult result;
      
            IEnumerable<User> user = new List<User>();
            try
            {
                user = this.userRepository.GetAllUsers();
                result = Ok(user);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(500);
            }
            return result;
        }
        [Route("/getuseremail/{Email}")]
        [Authorize]
        [HttpGet]
        public ActionResult GetUserByEmail(string Email)
        {
            ActionResult result;
            try
            {
                User user = new User();
                user = this.userRepository.GetUserByEmail(Email);
                result = Ok(user);
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(401);
            }
            return result;
        }
        [Authorize]
        [HttpPost("InsertIntoUser")]
 
        public ActionResult InsertIntoUser([FromBody] User UserInsert)
        {
            string pass = BCrypt.Net.BCrypt.HashPassword(UserInsert.PasswordHash);
            UserInsert.PasswordHash = pass;
            UserInsert.PasswordSalt = pass;
            ActionResult result;
            try
            {     // for DupicateEmail
                User userfromDB = this.userRepository.GetUserByEmail(UserInsert.Email);
                if(userfromDB !=null)
                {
                    result = new StatusCodeResult(500);
                }
                else
                {
                    this.userRepository.InsertIntoUser(UserInsert);
                    result = Ok();
                }

                //this.userRepository.InsertIntoUser(UserInsert);
                //result = Ok();
            }
            catch (Exception ex)
            {
                result = new StatusCodeResult(500);               
            }
            return result;
        }
    }
    
}
