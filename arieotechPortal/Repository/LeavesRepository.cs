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
    public class LeavesRepository : ILeavesRepository
    {
        private readonly IConfiguration configuration;
        public LeavesRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //GET ALL LEAVES
        public IEnumerable<Leaves> GetAllLeaves()
        {
            IEnumerable<Leaves> leaves = new List<Leaves>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                leaves = conn.Query<Leaves>("SELECT * FROM Leaves;");
            }
            return leaves;
        }


        //Insert Into Leaves
        //public void InsertIntoLeaves(Leaves LeaveInsert)
        //{
        //    try
        //    {
        //        //string status = LeavesStatus.Equals.Not_Approved;
        //        using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
        //        {
        //            conn.Execute("INSERT INTO Leaves VALUES (@EmployeeId,@StartDate,@LeaveReason,@LeaveAddress,@IsHalfDay,@ResumeDate,@DepartmentId,@Status)", new
        //            {

        //                LeaveID=LeaveInsert.LeaveID,
        //                EmployeeId = LeaveInsert.EmployeeId,
        //                StartDate = LeaveInsert.StartDate,
        //                LeaveReason = LeaveInsert.LeaveReason,
        //                LeaveAddress = LeaveInsert.LeaveAddress,
        //                IsHalfDay = LeaveInsert.IsHalfDay,
        //                ResumeDate = LeaveInsert.ResumeDate,
        //                DepartmentId = LeaveInsert.DepartmentId,
        //                Status = LeaveInsert.Status,
        //            });
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}

        //INSERT INTO LEAVES
        //public void InsertIntoLeave(Leaves LeaveInsert, int LeavesStatus)
        //{
        //    try
        //    {
        //        //string status = LeavesStatus.Equals.Not_Approved;
        //        using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
        //        {
        //            conn.Execute("INSERT INTO Leaves VALUES (@EmployeeId,@StartDate,@LeaveReason,@LeaveAddress,@IsHalfDay,@ResumeDate,@DepartmentId,@Status)", new
        //            {

        //                LeaveID = LeaveInsert.LeaveID,
        //                EmployeeId = LeaveInsert.EmployeeId,
        //                StartDate = LeaveInsert.StartDate,
        //                LeaveReason = LeaveInsert.LeaveReason,
        //                LeaveAddress = LeaveInsert.LeaveAddress,
        //                IsHalfDay = LeaveInsert.IsHalfDay,
        //                ResumeDate = LeaveInsert.ResumeDate,
        //                DepartmentId = LeaveInsert.DepartmentId,
        //                Status = LeaveInsert.Status,
        //            });
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void InsertIntoLeaves(Leaves LeaveInsert)
        {
            try
            {
                //string status = LeavesStatus.Equals.Not_Approved;
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("INSERT INTO Leaves VALUES (@EmployeeId,@StartDate,@LeaveReason,@LeaveAddress,@IsHalfDay,@ResumeDate,@DepartmentId,@Status)", new
                    {

                        LeaveID = LeaveInsert.LeaveID,
                        EmployeeId = LeaveInsert.EmployeeId,
                        StartDate = LeaveInsert.StartDate,
                        LeaveReason = LeaveInsert.LeaveReason,
                        LeaveAddress = LeaveInsert.LeaveAddress,
                        IsHalfDay = LeaveInsert.IsHalfDay,
                        ResumeDate = LeaveInsert.ResumeDate,
                        DepartmentId = LeaveInsert.DepartmentId,
                        Status = LeaveInsert.Status,
                    });
                }
            }

            catch (Exception ex)
            {

            }
        }

        //UPDATE LEAVES
        public void UpdateLeaves(Leaves LeavesUpdate, int LeavesID)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("update[dbo].[Leaves] set EmployeeId = @EmployeeId, StartDate= @StartDate, LeaveReason= @LeaveReason,LeaveAddress=@LeaveAddress,IsHalfDay=@IsHalfDay,ResumeDate=@ResumeDate,DepartmentId=@DepartmentId,Status=@Status where LeaveID=@LeaveID", new

                    {
                        LeaveID = LeavesUpdate.LeaveID,
                        EmployeeId = LeavesUpdate.EmployeeId,
                        StartDate = LeavesUpdate.StartDate,
                        LeaveReason = LeavesUpdate.LeaveReason,
                        LeaveAddress = LeavesUpdate.LeaveAddress,
                        IsHalfDay = LeavesUpdate.IsHalfDay,
                        ResumeDate = LeavesUpdate.ResumeDate,
                        DepartmentId = LeavesUpdate.DepartmentId,
                        Status = LeavesUpdate.Status,
                    });
                }
            }

            catch (Exception ex)
            {

            }
        }
    }
}
