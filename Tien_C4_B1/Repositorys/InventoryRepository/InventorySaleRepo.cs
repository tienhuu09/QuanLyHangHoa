using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class InventorySaleRepo : IRepoInventorySale<InventorySale>
    {
        private string pathData { get; } = "Data/SalesSlips/InventorySale.xml";
        public List<InventorySale> lstInventorySale { get; set; }

        public InventorySaleRepo()
        {
            lstInventorySale = new List<InventorySale>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList nodeList = DataProvider.Instance.getDsNode("//InventorySale");

            InventorySale inven = null;
            foreach (XmlNode item in nodeList)
            {
                inven = new InventorySale();
                inven.IdProduct = item.Attributes["IdProduct"].Value;
                inven.QuantityInvoice = double.Parse(item.Attributes["QuantityInvoice"].Value);
                inven.PriceOutput = double.Parse(item.Attributes["PriceOutput"].Value);
                inven.QuantitySold = double.Parse(item.Attributes["QuantitySold"].Value);
                inven.Remaining = double.Parse(item.Attributes["Remaining"].Value);

                lstInventorySale.Add(inven);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void AddProduct(Product product)
        {
            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("InventorySale");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
            attr1.Value = product.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("QuantityInvoice");
            attr2.Value = "0";
            XmlAttribute attr3 = DataProvider.Instance.createAttr("PriceOutput");
            attr3.Value = product.PriceOutput.ToString();
            XmlAttribute attr4 = DataProvider.Instance.createAttr("QuantitySold");
            attr4.Value = "0";
            XmlAttribute attr5 = DataProvider.Instance.createAttr("Remaining");
            attr5.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            string xpath = string.Format("//InventorySales");
            XmlNode node = DataProvider.Instance.getNode(xpath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(InventorySale invenSale)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//InventorySale[@IdProduct='{0}']", invenSale.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["QuantityInvoice"].InnerText = invenSale.QuantityInvoice.ToString();
            node.Attributes["QuantitySold"].InnerText = invenSale.QuantitySold.ToString();
            node.Attributes["Remaining"].InnerText = invenSale.Remaining.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Add(InventorySale entity)
        {
            lstInventorySale.Add(entity);
        }

        public void Delete(InventorySale entity)
        {
            throw new NotImplementedException();
        }

        public InventorySale Get(string id)
        {
            return null;
        }

        public List<InventorySale> Gets()
        {
            return lstInventorySale;
        }
    }
}
