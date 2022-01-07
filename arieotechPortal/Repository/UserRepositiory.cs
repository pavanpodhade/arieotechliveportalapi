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
    public class UserRepositiory : IUserRepositiory
    {
        private readonly IConfiguration configuration;
        public UserRepositiory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ChangePassWord(User user, int Id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [ArieotechLive].[dbo].[User] set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt where Id=@Id", new
                {
                    Id = Id,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                });

            }
        } 
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> user = new List<User>();
            string val=this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                user = conn.Query<User>("SELECT * FROM [dbo].[User]");
            }
            return user;
        }

        public User GetUserByEmail(string Email)
        {
            User user = new User();
           
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                user = conn.Query<User>(" select * from [ArieotechLive].[dbo].[User] where Email=@Email", new { Email = Email }).FirstOrDefault();
            }
            return user;
        }

        public void InsertIntoUser(User userinsert)
        {
                try
                {
                    using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                    {
                        conn.Execute("INSERT INTO [dbo].[User] VALUES (@Email,@PasswordHash,@PasswordSalt,@FirstName,@LastName,@Active,@RoleId)", new
                        {
                            Email = userinsert.Email,
                            PasswordHash = userinsert.PasswordHash,
                            PasswordSalt= userinsert.PasswordSalt,
                            FirstName = userinsert.FirstName,
                            LastName = userinsert.LastName,
                            Active = userinsert.Active,
                            RoleId = userinsert.RoleId,
                           
                        });
                    }
                }
                catch (Exception ex)
                {

                }
         }
        #region GetUserByID
        public User GetUserByID(int id)
        {

            User user = new User();

            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                user = conn.Query<User>(" select * from [ArieotechLive].[dbo].[User] where Id=@id", new { Id = id }).FirstOrDefault();
            }
            return user;
        }
        #endregion
        #region UpdateUser
        public void UpdateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[User] set Email=@Email,FirstName=@FirstName,LastName=@LastName,Active=@Active,RoleId=@RoleId where Id=@Id", new
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                   
                    Active = user.Active,
                    RoleId = user.RoleId

                }); ;

            }
        }
        #endregion
        #region UpdateUserPassword
        public void UpdateUserPassword(int id, string passwordsalt, string passwordhash)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[User] set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt where Id=@id", new
                {
                    passwordsalt = passwordsalt,
                    PasswordHash = passwordhash,
                    id = id

                });

            }
        }

        #endregion
    }
}
