using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace ArieotechLive.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepositiory userRepository;
        private readonly ILoggerManager loggerManager;
        private readonly IUsageActivityRepository usageActivityRepository;
        public UserController(IUserRepositiory userRepository)
        {
            this.userRepository = userRepository;
        }
        [Route("GetAllUsers()")]
       
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
                if(userfromDB!=null)
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
        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {

            ActionResult result;
            User user = new User();
            try
            {
               // this.loggerManager.LogInfo(string.Format("Get userby id is called,id:{0}", id));
                AuthResult auth = new ControllerHelper().GetLoggedInUser(this.HttpContext.User.Identity);
                if (auth.RoleId != (int)UserRoleEnum.SuperAdmin)
                {
                    result = new StatusCodeResult(401);
                    this.loggerManager.LogInfo(string.Format("User: {0} is not allowed for this operation get user by id", id));
                    return result;

                }

                user = this.userRepository.GetUserByID(id);
                result = Ok(user);
                this.loggerManager.LogInfo(string.Format("Get userby id is completed,id:{0}", id));
            }
            catch (Exception ex)
            {
               // this.loggerManager.LogError(string.Format("Error while getting user by ID-->{0}, Details-->{1}, Error-->{2}", id, ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);
            }
            return result;
        }
        #region changepassword
        [HttpPut]
        [Route("ChangePassword")]
        public ActionResult ChangePassWord([FromBody] User user, int Id)
        {
            string pass = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = pass;
            user.PasswordSalt = pass;

            ActionResult result;
            try
            {

                this.userRepository.ChangePassWord(user, Id);
                
                    result = Ok();
                
            }
            catch (Exception ex)
            {

                result = new StatusCodeResult(401);
            }
            return result;
        }
        #endregion
    }

}
