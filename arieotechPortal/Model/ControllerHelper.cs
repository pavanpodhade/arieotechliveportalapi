using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using ArieotechLive.Repository;
using static Dapper.SqlMapper;

namespace ArieotechLive.Model
{
    public class ControllerHelper
    {
        #region UsageActivity 
        public void UsageActivity(IIdentity user, IUsageActivityRepository usageActivityRepository, ActivityTypeEnum activityTypeEnum, int activityValue)
        {

            AuthResult authUser = this.GetLoggedInUser(user);

            UsageActivity usageActivityModel = new UsageActivity();
            usageActivityModel.UserId = authUser.UserId;
            usageActivityModel.ActivityId = (int)activityTypeEnum;
            
            usageActivityModel.LoginSessionId = authUser.LoginSessionId;
            usageActivityModel.ActivityDate = DateTime.Now;
            usageActivityModel.ActivityValue = activityValue;
            usageActivityRepository.InsertUsageActivity(usageActivityModel);

        }

        public void UsageActivityLogin(IUsageActivityRepository usageActivityRepository, ActivityTypeEnum activityTypeEnum, int activityValue, int userid, int orgid, string loginSessionId)
        {


            UsageActivity usageActivityModel = new UsageActivity();
            usageActivityModel.UserId = userid;
            usageActivityModel.ActivityId = (int)activityTypeEnum;
           
            usageActivityModel.LoginSessionId = loginSessionId;
            usageActivityModel.ActivityDate = DateTime.Now;
            usageActivityModel.ActivityValue = activityValue;
            usageActivityRepository.InsertUsageActivity(usageActivityModel);

        }
        #endregion
        #region Compute and Verify Hash
        public Tuple<string, string> ComputeHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return new Tuple<string, string>("", passwordHash);
        }

        public bool VerifyHash(string enetedPassword, string passwordhashFromDB)
        {
            bool matching = BCrypt.Net.BCrypt.Verify(enetedPassword, passwordhashFromDB);

            return matching;
        }
        #endregion
        #region GetLoggedInUser
        public AuthResult GetLoggedInUser(IIdentity user)
        {
            AuthResult result = new AuthResult();

            result.IsLoginSuccessful = ((ClaimsIdentity)user).IsAuthenticated;
            IEnumerable<Claim> claims = ((ClaimsIdentity)user).Claims;
         
            result.RoleId = Convert.ToInt32(claims.ToList()[1].Value);
            //result.UserId = Convert.ToInt32(claims.ToList()[2].Value);
            //result.LoginSessionId = Convert.ToString(claims.ToList()[3].Value);
            return result;
        }

        internal void UsageActivityLogin(IUsageActivityRepository usageActivityRepository, ActivityTypeEnum userForgetPassword, int v1, int id, string v2)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
