using ArieotechLive.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public class ForgotPasswordHistoryRepository : IForgotPasswordHistory
    {
        #region Private Variables
        private IConfiguration configuration;
        #endregion

        #region ctor
        public ForgotPasswordHistoryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region AddForgotPasswordHistory
        public void AddForgotPasswordHistory(ForgotPasswordHistoryModel forgot)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("insert into  [dbo].[ForgotPasswordHistory] (CreatedDateTime,UserId,Email,ForgotPasswordToken,Active,EndDateTime) values (@CreatedDateTime,@UserId,@Email,@ForgotPasswordToken,@Active,@EndDateTime)", new
                {

                    CreatedDateTime = forgot.CreatedDateTime,
                    UserId = forgot.UserId,
                    Email = forgot.Email,
                    ForgotPasswordToken = forgot.ForgotPasswordToken,
                    Active = forgot.Active,
                    EndDateTime = forgot.EndDateTime
                });

            }
        }
        #endregion

        #region GetUserByForgotPasswordToken
        public ForgotPasswordHistoryModel GetUserByForgotPasswordToken(string fptoken)
        {
            ForgotPasswordHistoryModel forgot = new ForgotPasswordHistoryModel();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                forgot = conn.Query<ForgotPasswordHistoryModel>("select * from  [dbo].[ForgotPasswordHistory] where ForgotPasswordToken=@fptoken AND Active=1 ",
                    new { fptoken = fptoken }).FirstOrDefault();
            }
            return forgot;
        }
        #endregion
        #region UpdateForgotPasswordToken
        public void UpdateForgotPasswordToken(string fptoken)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[ForgotPasswordHistory] set Active=0 where ForgotPasswordToken=@fptoken", new
                {
                    fptoken = fptoken

                });


            }
        }
        #endregion
    }
}
