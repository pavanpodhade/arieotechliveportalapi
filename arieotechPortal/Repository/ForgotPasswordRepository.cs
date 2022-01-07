using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ArieotechLive.Model;
namespace ArieotechLive.Repository
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {

        #region Private Variables
        private IConfiguration configuration;
        #region
        public void AddForgotPasswordHistory(ForgotPassword forgot)
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
                }); ;

            }
        }


        #region
        public ForgotPassword GetUserByForgotPasswordToken(string fptoken)
        {
            ForgotPassword forgot = new ForgotPassword();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                forgot = conn.Query<ForgotPassword>("select * from  [dbo].[ForgotPassword] where ForgotPasswordToken=@fptoken AND Active=1 ",
                    new { fptoken = fptoken }).FirstOrDefault();
            }
            return forgot;
        }
        #endregion
        #region
        public void UpdateForgotPasswordToken(string fptoken)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[ForgotPassword] set Active=0 where ForgotPasswordToken=@fptoken", new
                {
                    fptoken = fptoken

                });


            }
        }

        void IForgotPasswordRepository.AddForgotPasswordHistory(ForgotPasswordRepository forgot)
        {
            throw new NotImplementedException();
        }


        ForgotPasswordRepository IForgotPasswordRepository.GetUserByForgotPasswordToken(string fptoken)
        {
            throw new NotImplementedException();
        }

        void IForgotPasswordRepository.UpdateForgotPasswordToken(string fptoken)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion

