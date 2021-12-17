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
    public class LoginRepositiory : ILoginRepositiory
    {
        private readonly IConfiguration configuration;
            
        public LoginRepositiory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Login getLoginByEmail(string Email)
        {
            Login login = new Login();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                login = conn.Query<Login>(" select * from  [ArieotechLive1].[dbo].[Login] (nolock) where Email=@Email", new { Email = Email }).FirstOrDefault();
            }
            return login;
        }

        //public Login getLoginByUserName(string UserName)
        //{
        //    Login login = new Login();
        //    using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
        //    {
        //        login = conn.Query<Login>(" select * from  [arieotechlive].[dbo].[Login] (nolock) where UserName=@UserName", new { UserName = UserName }).FirstOrDefault();
        //    }
        //    return login;
        //}
        public Login getLoginByPassword(string Password)
        {
            Login login = new Login();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                login = conn.Query<Login>(" select * from  [ArieotechLive1].[dbo].[Login] (nolock) where Password=@Password", new { Password = Password }).FirstOrDefault();
            }
            return login;
            }
    }
}