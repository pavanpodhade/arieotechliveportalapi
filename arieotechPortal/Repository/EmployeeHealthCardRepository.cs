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
    public class EmployeeHealthCardRepository: IEmployeeHealthCardRepository
    {
        private readonly IConfiguration configuration;
        public EmployeeHealthCardRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //getallEmphealthcard
        public IEnumerable<EmployeeHealthCard> GetAllEmployeehealthcard()
        {
            IEnumerable<EmployeeHealthCard> EmployeeHealthCard = new List<EmployeeHealthCard>();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                EmployeeHealthCard = conn.Query<EmployeeHealthCard>("SELECT * FROM EmployeeHealthCard");
            }
            return EmployeeHealthCard;
        }



        //GET EmployeeHealthCard By EmployeeHealthCardID
        public EmployeeHealthCard GetEmployeeHealthCardById(int EmployeeHealthCardID)
        {
            EmployeeHealthCard employeeHealthCard = new EmployeeHealthCard();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employeeHealthCard = conn.Query<EmployeeHealthCard>(string.Format("SELECT * FROM EmployeeHealthCard Where EmpHealthCardID ={0}", EmployeeHealthCardID)).FirstOrDefault();
            }

            return employeeHealthCard;
        }
        // GET EmployeeHealthCard By Name
        public EmployeeHealthCard GetDepartmentByDepartmentName(string EmployeeHealthCardtName)
        {
            EmployeeHealthCard employeehealthcard = new EmployeeHealthCard();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))

            {
                employeehealthcard = conn.Query<EmployeeHealthCard>("SELECT * FROM EmployeeHealthCard Where First_Name = @DEPTN", new
                {
                    DEPTN = EmployeeHealthCardtName,
                }).FirstOrDefault();
            }

            return employeehealthcard;
        }


        //INSERT INTO EmployeeHealthCard
        public void InsertIntoEmployeeHealthCard(EmployeeHealthCard EmployeeHealthCardInsert)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("INSERT INTO EmployeeHealthCard VALUES (@First_Name,@Last_Name,@Relation,@AdharCard_No,@ActiveStatus,@id)", new
                    {
                        First_Name = EmployeeHealthCardInsert.First_Name,
                        Last_Name = EmployeeHealthCardInsert.Last_Name,
                        Relation = EmployeeHealthCardInsert.Relation,
                        AdharCard_No = EmployeeHealthCardInsert.AdharCard_No,
                        ActiveStatus = EmployeeHealthCardInsert.ActiveStatus,
                        id = EmployeeHealthCardInsert.id

                    });
                }
            }

            catch (Exception ex)
            {

            }
        }
        //UPDATE EmployeeHealthCard
        public void UpdateEmpHealthCard(EmployeeHealthCard EmployeeHealthCardUpdate, int EmployeeHealthCardID)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
                {
                    conn.Execute("update EmployeeHealthCard set First_Name=@First_Name, Last_Name= @Last_Name, Relation= @Relation, AdharCard_No= @AdharCard_No, id= @id, ActiveStatus= @ActiveStatus where EmpHealthCardID=@EmployeeHealthCardID", new

                    {
                        EmployeeHealthCardID = EmployeeHealthCardID,
                        First_Name = EmployeeHealthCardUpdate.First_Name,
                        Last_Name = EmployeeHealthCardUpdate.Last_Name,
                        Relation = EmployeeHealthCardUpdate.Relation,
                        AdharCard_No = EmployeeHealthCardUpdate.AdharCard_No,
                        id = EmployeeHealthCardUpdate.id,
                        ActiveStatus = EmployeeHealthCardUpdate.ActiveStatus
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        //DEACTIVATE Employeehealthcard
        public void DeactivateEmployeeHC(int EmployeeHealthCardID)
        {
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                conn.Execute("UPDATE EmployeeHealthCard SET ActiveStatus=0 WHERE EmpHealthCardID = @EmployeeHCID", new
                {
                    EmployeeHCID = EmployeeHealthCardID
                });
            }
        }

        public EmployeeHealthCard GetEmployeeHealthCardByRelation(string Relation)
        {

            EmployeeHealthCard employeeHealthCard = new EmployeeHealthCard();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))
            {
                employeeHealthCard = conn.Query<EmployeeHealthCard>("select * from [dbo].[EmployeeHealthCard] Where Relation = @relation", new
                {
                    relation = Relation,

                }).FirstOrDefault();
            }

            return employeeHealthCard;

        }

        //public EmployeeHealthCard GetDepartmentByDepartmentName(int AdharCard_No)
        //{
        //    EmployeeHealthCard employeehealthcard = new EmployeeHealthCard();
        //    using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))

        //    {
        //        employeehealthcard = conn.Query<EmployeeHealthCard>("SELECT * FROM EmployeeHealthCard Where AdharCard_No = @DEPTN", new
        //        {
        //            DEPTN = AdharCard_No,
        //        }).FirstOrDefault();
        //    }

        //    return employeehealthcard;
        //}

        //public EmployeeHealthCard GetEmployeeHCByAdharCard_No(int AdharCard_No)
        //{
        //    EmployeeHealthCard employeehealthcard = new EmployeeHealthCard();
        //    using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))

        //    {
        //        employeehealthcard = conn.Query<EmployeeHealthCard>("SELECT * FROM EmployeeHealthCard Where AdharCard_No = @DPTN", new
        //        {
        //            DPTN = AdharCard_No,
        //        }).FirstOrDefault();
        //    }

        //    return employeehealthcard;

        //}

        public EmployeeHealthCard GetEmployeeHCByAdharCard_No(string AdharCard_No)
        {
            EmployeeHealthCard employeehealthcard = new EmployeeHealthCard();
            using (IDbConnection conn = new SqlConnection(this.configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value))

            {
                employeehealthcard = conn.Query<EmployeeHealthCard>("SELECT * FROM EmployeeHealthCard Where AdharCard_No = @DPTN", new
                {
                    DPTN = AdharCard_No,
                }).FirstOrDefault();
            }

            return employeehealthcard;


        }

    }
}
