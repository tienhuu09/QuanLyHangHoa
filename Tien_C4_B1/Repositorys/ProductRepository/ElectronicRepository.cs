using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tien_C4_B1;

namespace Tien_C4_B1
{
    public class ElectronicRepository : IRepository<Electronic>
    {
        private string pathData { get; } = "Data/Products/Electronics.xml";
        public List<Electronic> lstElectronic { get; set; }

        public ElectronicRepository()
        {
            lstElectronic = new List<Electronic>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Product");

            Electronic electronic = null;
            foreach (XmlNode item in lstNode)
            {
                electronic = new Electronic();
                electronic.Id = item.Attributes["Id"].Value;
                electronic.Name = item.Attributes["Name"].Value;
                electronic.Category = item.Attributes["Category"].Value;
                electronic.Producer = item.Attributes["Producer"].Value;
                electronic.PriceInput = double.Parse(item.Attributes["PriceInput"].Value);
                electronic.PriceOutput = double.Parse(item.Attributes["PriceOutput"].Value);
                electronic.Warranty = Int32.Parse(item.Attributes["Warranty"].Value);
                electronic.ElectricPower = Int32.Parse(item.Attributes["ElectricPower"].Value);

                lstElectronic.Add(electronic);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Electronic electric)
        {
            lstElectronic.Add(electric);

            DataProvider.Instance.Open(pathData);

            XmlNode newNode = DataProvider.Instance.createNode("Product");
            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = electric.Id;

            XmlAttribute attr2 = DataProvider.Instance.createAttr("Name");
            attr2.Value = electric.Name;

            XmlAttribute attr3 = DataProvider.Instance.createAttr("Category");
            attr3.Value = electric.Category;

            XmlAttribute attr4 = DataProvider.Instance.createAttr("Producer");
            attr4.Value = electric.Producer;

            XmlAttribute attr5 = DataProvider.Instance.createAttr("PriceInput");
            attr5.Value = electric.PriceInput.ToString();

            XmlAttribute attr6 = DataProvider.Instance.createAttr("PriceOutput");
            attr6.Value = electric.PriceOutput.ToString();

            XmlAttribute attr7 = DataProvider.Instance.createAttr("Warranty");
            attr7.Value = electric.Warranty.ToString();

            XmlAttribute attr8 = DataProvider.Instance.createAttr("ElectricPower");
            attr8.Value = electric.ElectricPower.ToString();


            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            string xPath = string.Format("//Products");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Remove(Electronic electric)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", electric.Id);
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

        public void Update(Electronic entity)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", entity.Id);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Name"].InnerText = entity.Name;
            node.Attributes["Producer"].InnerText = entity.Producer;
            node.Attributes["PriceInput"].InnerText = entity.PriceInput.ToString();
            node.Attributes["PriceOutput"].InnerText = entity.PriceOutput.ToString();
            node.Attributes["Warranty"].InnerText = entity.Warranty.ToString();
            node.Attributes["ElectricPower"].InnerText = entity.ElectricPower.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Electronic entity)
        {
            throw new NotImplementedException();
        }

        public Electronic Get(string id)
        {
            foreach (var item in lstElectronic)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Electronic> Gets()
        {
            return lstElectronic;
        }
    }
}
