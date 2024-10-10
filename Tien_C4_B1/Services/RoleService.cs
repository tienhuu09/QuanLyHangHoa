using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Tien_C4_B1
{
    public class RoleService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Role> roleRepo { get; set; }

        public RoleService()
        {
            roleRepo = unitOfWork.RoleRepository;
        }

        public void Add(Role role)
        {
            roleRepo.Add(role);
        }

        public bool CheckRoleAvailable(Account acc, Role role)
        {
            if (string.Compare(acc.Role.RoleName, role.RoleName, true) == 0)
                return true;
            return false;
        }

        public bool CheckIdRole(string id)
        {
            foreach (var item in roleRepo.Gets())
                if (string.Compare(item.IdRole, id, true) == 0)
                    return true;
            return false;
        }

        public void EditName(int opt, string name)
        {
            //roleRepo.EditName(roleRepo.lstRole[opt], name);
            //roleRepo.Gets()[opt].RoleName = name;
        }

        public Role Get(string id)
        {
            return roleRepo.Get(id);
        }

        public List<Role> Gets()
        {
            return roleRepo.Gets();
        }

        public List<Role> GetsRoleEmpty()
        {
            List<Role> roles = new List<Role>();

            foreach (var item in roleRepo.Gets())
                if (string.Compare(item.UserName, "empty", true) == 0)
                    roles.Add(item);

            return roles;
        }

        public void Update(Role role)
        {
            roleRepo.Update(role);
        }
    }
}
