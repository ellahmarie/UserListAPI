using System;
using System.Collections.Generic;

#nullable disable

namespace UsersDetailAPI.Models
{
    public partial class UsersDetail
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
