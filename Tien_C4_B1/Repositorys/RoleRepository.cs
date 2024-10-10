using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class RoleRepository : IRepository<Role>
    {
        private string pathData { get; } = "Data/Accounts/AccountRoles.xml";
        public List<Role> lstRole { get; set; }

        public RoleRepository()
        {
            lstRole = new List<Role>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Role");

            Role role = null;
            foreach (XmlNode item in lstNode)
            {
                role = new Role();
                role.IdRole = item.Attributes["IdRole"].Value;
                role.RoleName = item.Attributes["RoleName"].Value;
                role.RoleLv = Int32.Parse(item.Attributes["RoleLv"].Value);
                role.UserName = item.Attributes["UserName"].Value;
                lstRole.Add(role);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void EditName(Role role, string name)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Role[@IdRole='{0}']", role.IdRole);
            XmlNode node = DataProvider.Instance.getNode(xPath);
            node.Attributes["RoleName"].InnerText = name;

            DataProvider.Instance.Close(pathData);
        }

        public void SetRole(Account acc, Role role)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Role[@IdRole='{0}']", role.IdRole);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["IdAccount"].InnerText = acc.IdAccount;

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Role role)
        {
            lstRole.Add(role);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("Role");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdRole");
            attr1.Value = role.IdRole;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("RoleName");
            attr2.Value = role.RoleName;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("UserName");
            attr3.Value = role.UserName;

            XmlAttribute attr4 = DataProvider.Instance.createAttr("RoleLv");
            attr4.Value = role.RoleLv.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//AccountRoles");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Role entity)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Role[@IdRole='{0}']", entity.IdRole);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["RoleName"].InnerText = entity.RoleName;
            node.Attributes["UserName"].InnerText = entity.UserName;
            node.Attributes["RoleLv"].InnerText = entity.RoleLv.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public Role Get(string id)
        {
            foreach (var item in lstRole)
                if (string.Compare(item.IdRole, id) == 0)
                    return item;
            return null;
        }

        public List<Role> Gets()
        {
            return lstRole;
        }
    }
}
