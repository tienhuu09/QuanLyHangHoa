using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class PorcelainRepository : IRepository<Porcelain>
    {
        private string pathData { get; } = "Data/Products/Porcelains.xml";
        public List<Porcelain> lstPorcelain { get; set; }

        public PorcelainRepository()
        {
            lstPorcelain = new List<Porcelain>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Product");

            Porcelain porcelain = null;
            foreach (XmlNode item in lstNode)
            {
                porcelain = new Porcelain();
                porcelain.Id = item.Attributes["Id"].Value;
                porcelain.Name = item.Attributes["Name"].Value;
                porcelain.Category = item.Attributes["Category"].Value;
                porcelain.Producer = item.Attributes["Producer"].Value;
                porcelain.PriceInput = double.Parse(item.Attributes["PriceInput"].Value);
                porcelain.PriceOutput = double.Parse(item.Attributes["PriceOutput"].Value);
                porcelain.Material = item.Attributes["Material"].Value;

                lstPorcelain.Add(porcelain);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Porcelain porcelain)
        {
            lstPorcelain.Add(porcelain);

            DataProvider.Instance.Open(pathData);

            XmlNode newNode = DataProvider.Instance.createNode("Product");
            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = porcelain.Id;

            XmlAttribute attr2 = DataProvider.Instance.createAttr("Name");
            attr2.Value = porcelain.Name;

            XmlAttribute attr3 = DataProvider.Instance.createAttr("Category");
            attr3.Value = porcelain.Category;

            XmlAttribute attr4 = DataProvider.Instance.createAttr("Producer");
            attr4.Value = porcelain.Producer;

            XmlAttribute attr5 = DataProvider.Instance.createAttr("PriceInput");
            attr5.Value = porcelain.PriceInput.ToString();

            XmlAttribute attr6 = DataProvider.Instance.createAttr("PriceOutput");
            attr6.Value = porcelain.PriceOutput.ToString();

            XmlAttribute attr7 = DataProvider.Instance.createAttr("Material");
            attr7.Value = porcelain.Material;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            string xPath = string.Format("//Products");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Remove(Porcelain porcelain)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", porcelain.Id);
            XmlNode refNode = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.RemoveNode(refNode);

            DataProvider.Instance.Close(pathData);
        }

        public void UpdatePrice(string id, double price)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", id);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["PriceInput"].InnerText = price.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Porcelain entity)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", entity.Id);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Name"].InnerText = entity.Name;
            node.Attributes["Producer"].InnerText = entity.Producer;
            node.Attributes["PriceInput"].InnerText = entity.PriceInput.ToString();
            node.Attributes["PriceOutput"].InnerText = entity.PriceOutput.ToString();
            node.Attributes["Material"].InnerText = entity.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Porcelain entity)
        {
            throw new NotImplementedException();
        }

        public Porcelain Get(string id)
        {
            foreach (var item in lstPorcelain)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Porcelain> Gets()
        {
            return lstPorcelain;
        }
    }
}
