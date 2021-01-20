using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersDetailAPI.IRepository;
using UsersDetailAPI.Models;

namespace UsersDetailAPI.Repository
{
    public class UsersDetailRepository : IUsersDetailsRepository
    {
        private readonly UsersDetailContext _context;

        public  UsersDetailRepository(UsersDetailContext context)
        {
            _context = context;
        }

        public UsersDetail AddUser(UsersDetail user)
        {
            _context.UsersDetails.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = _context.UsersDetails.FirstOrDefault(user => user.Id == id);
            _context.UsersDetails.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<UsersDetail> GetAllUsers()
        {
            return _context.UsersDetails.ToList();
        }

        public UsersDetail GetUserById(int id)
        {
            return _context.UsersDetails.FirstOrDefault(user => user.Id == id);
        }

        public UsersDetail Login(UserLogin user)
        {
            return _context.UsersDetails.Where(a => a.Email == user.email && a.Password == user.password).FirstOrDefault();
        }

        public void UpdateUser(int id, UsersDetail user)
        {
            UsersDetail userDetails = _context.UsersDetails.FirstOrDefault(user => user.Id == id);


            userDetails.Email = user.Email;
            userDetails.Firstname = user.Firstname;
            userDetails.Lastname = user.Lastname;
            userDetails.Password = user.Password;

            _context.SaveChanges();
        }
    }
}
