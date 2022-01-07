using ArieotechLive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPassword : ControllerBase
    {
        #region Private Vars 
        private readonly ILoggerManager loggerManager;
        private readonly IForgotPasswordRepository forgotPassword;
        private readonly IUserRepositiory userRepository;
        private readonly IConfiguration configuration;
        private readonly IUsageActivityRepository usageActivityRepository;
        #endregion

        #region ctor
        public ForgotPassword(ILoggerManager loggerManager,
                                                IForgotPasswordRepository forgotPasswordRepository,
                                                IUserRepositiory userRepository,
                                                IConfiguration configuration,
                                                IUsageActivityRepository usageActivityRepository)
        {
            this.loggerManager = loggerManager;
            this.forgotPassword = forgotPasswordRepository;
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.usageActivityRepository = usageActivityRepository;

        }
        #endregion

        #region Public Methods

        [Route("resetpassword/{fptoken}")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUserByForgotPasswordToken(string fptoken)
        {
            ActionResult result;
            ForgotPassword forgotPasswordModel = new ForgotPassword();
            try
            {
                this.loggerManager.LogInfo(string.Format("Get user by forgot password token is called,fptoken:{0}", fptoken));

                forgotPasswordHistoryModel = this.forgotPasswordHistory.GetUserByForgotPasswordToken(fptoken);
                result = Ok(forgotPasswordHistoryModel);
                this.loggerManager.LogInfo(string.Format("Get user by forgot password token is completed,fptoken:{0}", fptoken));
            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while getting user by fptoken-->{0}, Details-->{1},Error-->{2}", fptoken, ex.StackTrace + ex.Message));
                result = new StatusCodeResult(500);
            }
            return result;
        }

        [Route("email")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            UserModel user = new UserModel();
            AuthResult authResult = new AuthResult();
            ActionResult result;
            try
            {
                UsageActivityModel usageActivityModel = new UsageActivityModel();
                //Calculate Email            
                UserModel userfromdb = this.userRepository.GetUserByEmail(email);
                if (userfromdb != null)
                {
                    this.loggerManager.LogInfo(string.Format("User forgot password is called-->{0}", email));

                    ForgotPasswordHistoryModel model = new ForgotPasswordHistoryModel();

                    model.ForgotPasswordToken = Guid.NewGuid().ToString();
                    model.UserId = userfromdb.Id;
                    model.CreatedDateTime = DateTime.Now;
                    model.EndDateTime = DateTime.Now.AddDays(2);
                    model.Email = userfromdb.Email;
                    model.Active = 1;



                    this.forgotPasswordHistory.AddForgotPasswordHistory(model);
                    this.loggerManager.LogInfo(string.Format("User forgot password is completed-->{0}", email));

                    //Sending Email
                    EmailSender emailsender = new EmailSender(this.configuration, this.loggerManager);
                    emailsender.SendForgotPasswordMail(email, model.ForgotPasswordToken);
                    string url = "" + model.ForgotPasswordToken;
                    result = Ok();
                    // new ControllerHelper().UsageActivity(this.HttpContext.User.Identity, this.usageActivityRepository, ActivityTypeEnum.UserForgetPassword, userfromdb.Id);
                    new ControllerHelper().UsageActivityLogin(this.usageActivityRepository, ActivityTypeEnum.UserForgetPassword, -1, userfromdb.Id, userfromdb.OrgID, authResult.LoginSessionId = "");
                    this.loggerManager.LogInfo(string.Format("User forgot Email is sent successfully-->{0}", email));
                }
                else
                {
                    //mailhelper.
                    result = Ok();
                }

            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while sending forgot password Email:{0}, Details:{1},Error:{2}", email, ex.Message, ex.StackTrace));
                result = new StatusCodeResult(500);

            }
            return result;
        }

        [Route("resetpassword")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(int userid, string password, string fptoken)
        {

            ActionResult result;
            AuthResult authResult = new AuthResult();
            try
            {
                this.loggerManager.LogInfo(string.Format("User reset password is called-->{0}", userid));
                UserModel user = new UserModel();
                Tuple<string, string> hashTuple = new ControllerHelper().ComputeHash(password);
                user.PasswordSalt = hashTuple.Item1;
                user.PasswordHash = hashTuple.Item2;
                this.loggerManager.LogInfo(string.Format("Reseting Password of Id-->{0}", userid));
                this.userRepository.UpdateUserPassword(userid, user.PasswordSalt, user.PasswordHash);

                ForgotPasswordHistoryModel model = new ForgotPasswordHistoryModel();
                model.ForgotPasswordToken = fptoken;


                model.Active = 0;
                forgotPasswordHistory.UpdateForgotPasswordToken(model.ForgotPasswordToken);

                EmailSender emailsender = new EmailSender(this.configuration, this.loggerManager);

                user = userRepository.GetUserByID(userid);
                emailsender.ResetPassword(user.Email);
                result = Ok();
                //new ControllerHelper().UsageActivity(this.HttpContext.User.Identity, this.usageActivityRepository, ActivityTypeEnum.UserResetPassword, userid);
                new ControllerHelper().UsageActivityLogin(this.usageActivityRepository, ActivityTypeEnum.UserResetPassword, -1, userid, user.OrgID, authResult.LoginSessionId = "");
                this.loggerManager.LogInfo(string.Format("Reset password email has been sent successfully-->", user.Email));

            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while reseting user password --> {0}, details --> {1}, user-->{2}", ex.Message, ex.StackTrace, userid));
                return new StatusCodeResult(500);

            }
            return result;
        }

        #endregion
    }
}
