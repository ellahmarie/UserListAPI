using System.Collections.Generic;
using UsersDetailAPI.Models;

namespace UsersDetailAPI.IRepository
{
    public interface IUsersDetailsRepository
    {
        IEnumerable<UsersDetail> GetAllUsers();
        UsersDetail GetUserById(int id);
        void UpdateUser(int id, UsersDetail user);
        void DeleteUser(int id);
        UsersDetail AddUser(UsersDetail user);
        UsersDetail Login(UserLogin userLogin);
    }
}
