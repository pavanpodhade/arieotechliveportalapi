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
    public class UsageActivityRepository : IUsageActivityRepository
    {

        #region Private Variables
        private readonly IConfiguration configuration;
        #endregion

        #region ctor
        public UsageActivityRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region DeleteUsageActivity
        public void DeleteUsageActivity(int id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("delete from [dbo].[UsageActivity] where id=@id", new
                {
                    id = id
                });
            }
        }
        #endregion


        #region GetAllUsageActivity
        public IEnumerable<UsageActivity> GetAllUsageActivity()
        {
            IEnumerable<UsageActivity> usageActivityModels = new List<UsageActivity>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                usageActivityModels = conn.Query<UsageActivity>("select * from  [dbo].[UsageActivity] (nolock)");
            }
            return usageActivityModels;
        }
        #endregion

        #region GetUsageActivityByid
        public UsageActivity GetUsageActivityByid(int id)
        {
            UsageActivity usageActivityModel = new UsageActivity();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                usageActivityModel = conn.Query<UsageActivity>(" select * from [dbo].[UsageActivity] (nolock) where id=@id", new { id = id }).FirstOrDefault();
            }
            return usageActivityModel;
        }
        #endregion


        #region InsertUsageActivity
        public void InsertUsageActivity(UsageActivity usageActivityModel)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("insert into [dbo].[UsageActivity] values (@UserId,@LoginSessionId,@OrgId,@ActivityId,@ActivityDate,@ActivityValue)", new
                {

                    UserId = usageActivityModel.UserId,
                    LoginSessionId = usageActivityModel.LoginSessionId,
                    
                    ActivityId = usageActivityModel.ActivityId,
                    ActivityDate = usageActivityModel.ActivityDate,

                    ActivityValue = usageActivityModel.ActivityValue

                });
            }
        }
        #endregion


        #region UpdateUsageActivity
        public void UpdateUsageActivity(UsageActivity usageActivityModel, int id)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("update [dbo].[UsageActivity] set UserId=@UserId,LoginSessionId=@LoginSessionId,OrgId=@OrgId,ActivityId=@ActivityId,ActivityDate=@ActivityDate,ActivityValue=@ActivityValue ", new
                {
                    UserId = usageActivityModel.UserId,
                    LoginSessionId = usageActivityModel.LoginSessionId,
                    
                    ActivityId = usageActivityModel.ActivityId,
                    ActivityDate = usageActivityModel.ActivityDate,

                    ActivityValue = usageActivityModel.ActivityValue



                });
            }
        }

       
    }
}

#endregion