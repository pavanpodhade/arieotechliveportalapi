using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public interface IUsageActivityRepository
    {
        IEnumerable<UsageActivity> GetAllUsageActivity();
        UsageActivity GetUsageActivityByid(int id);
        void InsertUsageActivity(UsageActivity usageActivityModel);
        void UpdateUsageActivity(UsageActivity usageActivityModel, int id);
        void DeleteUsageActivity(int id);

    }
}
