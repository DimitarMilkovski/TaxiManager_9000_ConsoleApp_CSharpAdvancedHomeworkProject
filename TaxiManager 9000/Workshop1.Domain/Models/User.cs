using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Domain.Enums;

namespace Workshop1.Domain.Models
{
    public class User : BaseEntity
    {
        private static int _userCounter = 1;
        public User(string username, string password, Role role)
        {
            Id = _userCounter;
            Username = username;
            Password = password;
            Role = role;
            _userCounter++;

        }

        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override void GetInfo()
        {
            Console.WriteLine($"ID:{Id} Username: {Username}, Role: {Role} ");
        }
    }
}
