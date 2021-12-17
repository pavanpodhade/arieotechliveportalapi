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
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly IConfiguration configuration;
        public TimeSheetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void DeactivateTimeSheet(int Id)
        {
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("UPDATE [dbo].[TimeSheet] SET ActiveStatus=0 WHERE Id = @Id", new
                    {
                        Id = Id
                    });
                }
            }
        }

        public IEnumerable<TimeSheet> GetAllTimeSheet()
        {
            IEnumerable<TimeSheet> timeSheet = new List<TimeSheet>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                timeSheet = conn.Query<TimeSheet>("SELECT * FROM [ArieotechLive1].[dbo].[TimeSheet]");
               
            }
            return timeSheet;
        }

        public TimeSheet GetTimeSheetByEmployeeID(int EmployeeID)
        {
            TimeSheet timesheet = new TimeSheet();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                timesheet = (TimeSheet)conn.Query<TimeSheet>(string.Format(" select * from [ArieotechLive1].[dbo].[TimeSheet] where EmployeeID ={0}", EmployeeID)).FirstOrDefault();
            }
            return timesheet;
        }

        public TimeSheet GetTimeSheetByID(int Id)
        {
            TimeSheet timesheet = new TimeSheet();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                timesheet = (TimeSheet)conn.Query<TimeSheet>(string.Format(" select * from [ArieotechLive1].[dbo].[TimeSheet] where Id ={0}", Id)).FirstOrDefault();
            }
            return timesheet;
        }

        public void InsertIntoTimeSheet(TimeSheet timesheetinsert)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("INSERT INTO  [ArieotechLive1].[dbo].[TimeSheet] VALUES (@Date,@ProjectID,@EmployeeID,@Hours,@Description,@ActiveStatus)", new
                    {
               
                        Date = timesheetinsert.Date,
                        ProjectID = timesheetinsert.ProjectID,
                        EmployeeID = timesheetinsert.EmployeeID,
                        Hours = timesheetinsert.Hours,
                        Description = timesheetinsert.Description,
                        ActiveStatus = timesheetinsert.ActiveStatus
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        //updatetimesheet
        public void UpdateTimeSheet(TimeSheet timesheetupdate, int Id)
        {    
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("update [dbo].[TimeSheet] set Date=@Date,ProjectID=@ProjectID,EmployeeID=@EmployeeID,Hours=@Hours,Description=@Description,ActiveStatus=@ActiveStatus where Id=@Id", new
                    {
                        Id = Id,
                        Date = timesheetupdate.Date,
                        ProjectID = timesheetupdate.ProjectID,
                        EmployeeID = timesheetupdate.EmployeeID,
                        Hours = timesheetupdate.Hours,
                        Description = timesheetupdate.Description,
                        ActiveStatus = timesheetupdate.ActiveStatus
                    });
                }
            }
        }
    }
}
