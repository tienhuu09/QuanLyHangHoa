using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class AccountRepository : IRepository<Account>
    {
        private string pathData { get; } = "Data/Accounts/Accounts.xml";
        public List<Account> lstAccount { get; set; }

        public AccountRepository()
        {
            lstAccount = new List<Account>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Account");

            Account account = null;
            foreach (XmlNode item in lstNode)
            {
                account = new Account();
                account.IdAccount = item.Attributes["IdAccount"].Value;
                account.Name = item.Attributes["Name"].Value;
                account.Username = item.Attributes["Username"].Value;
                account.Password = item.Attributes["Password"].Value;
                account.IdRole = item.Attributes["IdRole"].Value;

                lstAccount.Add(account);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Account account)
        {
            lstAccount.Add(account);

            DataProvider.Instance.Open(pathData);

            XmlNode newNode = DataProvider.Instance.createNode("Account");
            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdAccount");
            attr1.Value = account.IdAccount;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Name");
            attr2.Value = account.Name;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Username");
            attr3.Value = account.Username;
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Password");
            attr4.Value = account.Password;
            XmlAttribute attr5 = DataProvider.Instance.createAttr("IdRole");
            attr5.Value = account.IdRole;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            string xPath = string.Format("//Accounts");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Remove(Account account)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Account[@Username='{0}']", account.Username);
            XmlNode refNode = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.RemoveNode(refNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Edit(Account account, string firstName)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Account[@Username='{0}']", firstName);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Name"].InnerText = account.Name;
            node.Attributes["Username"].InnerText = account.Username;
            node.Attributes["Password"].InnerText = account.Password;

            DataProvider.Instance.Close(pathData);
        }

        public void SetRole(Account acc, Role role)
        {
            DataProvider.Instance.Open(pathData);

            string xpath = string.Format("//Account[@IdAccount='{0}']", acc.IdAccount);
            XmlNode node = DataProvider.Instance.getNode(xpath);

            node.Attributes["IdRole"].InnerText = role.IdRole;
            DataProvider.Instance.Close(pathData);
        }

        public void CancelRole(string id)
        {
            DataProvider.Instance.Open(pathData);

            string xpath = string.Format("//Account[@IdAccount='{0}']", id);
            XmlNode node = DataProvider.Instance.getNode(xpath);

            node.Attributes["IdRole"].InnerText = "empty";
            DataProvider.Instance.Close(pathData);
        }

        public void Update(Account entity)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Account[@IdAccount='{0}']", entity.IdAccount);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Name"].InnerText = entity.Name;
            node.Attributes["Username"].InnerText = entity.Username;
            node.Attributes["Password"].InnerText = entity.Password;
            node.Attributes["IdRole"].InnerText = entity.IdRole;

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Account account)
        {
            lstAccount.Remove(account);

            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Account[@IdAccount='{0}']", account.IdAccount);
            XmlNode refNode = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.RemoveNode(refNode);

            DataProvider.Instance.Close(pathData);
        }

        public Account Get(string id)
        {
            foreach (var item in lstAccount)
                if (string.Compare(item.IdAccount, id, true) == 0)
                    return item;
            return null;
        }

        public List<Account> Gets()
        {
            return lstAccount;
        }
    }
}
