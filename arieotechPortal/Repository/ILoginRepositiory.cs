using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Smo;
using Octopus.Client.Repositories.Async;

using Login = ArieotechLive.Model.Login;

namespace ArieotechLive.Repository
{
    public interface ILoginRepositiory
    {
        Login getLoginByEmail(string Email);
        Login getLoginByPassword(string Password);
        

        //void Login(string UserName);

    }
}
