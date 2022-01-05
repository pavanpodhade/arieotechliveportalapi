using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
   public interface IForgotPasswordHistory
    {
        void AddForgotPasswordHistory(ForgotPasswordHistoryModel forgot);
        ForgotPasswordHistoryModel GetUserByForgotPasswordToken(string fptoken);

        void UpdateForgotPasswordToken(string fptoken);


    }
}
