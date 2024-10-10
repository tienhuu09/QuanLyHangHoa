using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Account
    {
        public string IdAccount { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IdRole { get; set; }
        public Role Role { get; set; }

        public Account()
        {
            Role = new Role();
        }

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            Role = new Role();
        }

        public Account(string idAcc, string name, string username, string password, string idRole)
        {
            this.IdAccount = idAcc;
            this.Name = name;
            this.Username = username;
            this.Password = password;
            this.IdRole = idRole;
            Role = new Role();
        }
    }
}
