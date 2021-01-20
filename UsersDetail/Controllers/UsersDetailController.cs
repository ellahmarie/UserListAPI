using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersDetailAPI.IRepository;
using UsersDetailAPI.Models;
using UsersDetailAPI.Repository;

namespace UsersDetailAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersDetailController : ControllerBase
    {
        private readonly IUsersDetailsRepository _repository;
        private readonly IEmailRepository _email;
        private const string EMAIL_BODY = "Hello! Your registration is successful.", EMAIL_SUBJECT = "REGISTRATION SUCCESSFUL";

        public UsersDetailController(IUsersDetailsRepository repository, IEmailRepository email)
        {
            _repository = repository;
            _email = email;
        }

        //GET /usersdetail
        [HttpGet]
        public ActionResult <IEnumerable<UsersDetail>> GetAllUsers()
        {
            var users = _repository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UsersDetail>> GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
           
            if (user == null) return Ok(new UsersDetail { Email = null, Firstname = null, Id = -1, Lastname = null, Password = null });
           
            return Ok(user);
        }

        [HttpPost]
        public ActionResult AddUser( UsersDetail user)
        {
            _repository.AddUser(user);

            _email.SendEmail(user.Email, EMAIL_SUBJECT, EMAIL_BODY );

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UsersDetail user)
        {
            var users = _repository.GetUserById(id);

            if (users == null)
            {
                return NotFound();
            }

            _repository.UpdateUser(id, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var users = _repository.GetUserById(id);

            if (users == null)
            {
                return NotFound();
            }

            _repository.DeleteUser(id);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login(UserLogin login)
        {
            var user = _repository.Login(login);

            if (user == null) return Ok(new UsersDetail { Email = null, Firstname = null, Id = -1, Lastname = null, Password = null });

            return Ok(_repository.Login(login));
        }
    }
}