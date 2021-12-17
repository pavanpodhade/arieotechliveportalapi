using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public interface ILeavesRepository
    {
        IEnumerable<Leaves> GetAllLeaves();
       
        void InsertIntoLeaves(Leaves LeavesInsert);

        void UpdateLeaves(Leaves LeavesUpdate, int LeavesID);

    }
}

//Apply Levae - Insert/Create				
//Approve Leave							
//Reject Leave							
//Cancel Leave