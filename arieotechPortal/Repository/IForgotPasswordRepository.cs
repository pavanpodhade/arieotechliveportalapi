using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArieotechLive.Model;
namespace ArieotechLive.Repository
{
    public interface IForgotPasswordRepository
    {
        void AddForgotPasswordHistory(ForgotPasswordRepository forgot);
        ForgotPasswordRepository GetUserByForgotPasswordToken(string fptoken);

        void UpdateForgotPasswordToken(string fptoken);

    }
}
