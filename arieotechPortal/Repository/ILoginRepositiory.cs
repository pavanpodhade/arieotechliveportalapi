
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
