using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccount.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patr { get; set; }
        public string Password { get; set; }

    }
}