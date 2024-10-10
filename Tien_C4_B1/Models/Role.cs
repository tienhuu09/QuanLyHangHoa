using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Role
    {
        public string IdRole { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public int RoleLv { get; set; }

        public Role()
        {
            this.IdRole = "empty";
            this.UserName = "empty";
            this.RoleLv = 0;
        }

        public Role(string idRole, string username, string roleName, int roleLv)
        {
            this.IdRole = idRole;
            this.RoleName = roleName;
            this.UserName = username;
            this.RoleLv = roleLv;
        }

    }
}
