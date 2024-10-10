using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class AccountService
    {
        private static readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public IRepository<Account> AccountRepo { get; set; }

        public AccountService()
        {
            AccountRepo = _unitOfWork.AccountRepository;
        }

        public bool isExistId(string id)
        {
            foreach (var item in AccountRepo.Gets())
                if (string.Compare(id, item.IdAccount, true) == 0)
                    return true;
            return false;
        }

        public Account getAccount(string username, string password)
        {
            foreach (var item in AccountRepo.Gets())
                if (string.Compare(item.Username, username, true) == 0 &&
                    string.Compare(item.Password, password, true) == 0)
                    return item;
            return null;
        }

        public void Add(Account account)
        {
            AccountRepo.Add(account);
        }

        public bool isExistUsername(string username)
        {
            foreach (var item in AccountRepo.Gets())
                if (string.Compare(item.Username, username, true) == 0)
                    return true;
            return false;
        }

        public void Remove(Account account)
        {
            AccountRepo.Delete(account);
        }

        public void Edit(Account account, string firstName)
        {
            //accRepo.Edit(account, firstName);
        }

        public void SetRole(Account acc, Role role)
        {
            acc.IdRole = role.IdRole;
            acc.Role = role;
            //accRepo.SetRole(acc, role);
        }

        public Account getAccountById(string id)
        {
            return AccountRepo.Get(id);
        }

        public void CancelRole(string id)
        {
            Account acc = getAccountById(id);
            acc.Role = new Role();
            acc.IdRole = "empty";
            //accRepo.CancelRole(id);
        }

        public List<Account> Gets()
        {
            return AccountRepo.Gets();
        }

        public void Update(Account account)
        {
            AccountRepo.Update(account);
        }
    }
}
