using System.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenDotNetCore.Models
{
    public class User
    {
        public User(int id,
            string email,
            string password,
            string[] roles)
        {
            Id = id;
            Email = email;
            Password = password;
            Roles = roles;
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string[] Roles { get; private set; }
    }
}