using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
   public interface IUserRepositiory
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string Email);
        void InsertIntoUser( User userinsert);

        User GetUserByID(int id);
        void UpdateUser(User user);

        void UpdateUserPassword(int id, string passwordsalt, string passwordhash);
        void ChangePassWord(User user, int Id);
    }
}
