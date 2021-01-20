using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersDetailAPI.IRepository
{
    public interface IEmailRepository
    {
        Task SendEmail(string email, string subject, string body);
    }
}
