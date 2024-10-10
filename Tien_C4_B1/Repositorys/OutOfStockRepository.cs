using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class OutOfStockRepository : IRepository<OutOfStock>
    {
        private string pathData { get; } = "Data/Inventories/OutOfStock.xml";
        public List<OutOfStock> lstOutOfStock { get; set; }

        public OutOfStockRepository()
        {
            lstOutOfStock = new List<OutOfStock>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//OutOfStock");

            OutOfStock outStock = null;
            foreach (XmlNode item in lstNode)
            {
                outStock = new OutOfStock();
                outStock.IdProduct = item.Attributes["IdProduct"].Value;
                outStock.NameProduct = item.Attributes["NameProduct"].Value;
                outStock.Remaining = double.Parse(item.Attributes["Remaining"].Value);

                lstOutOfStock.Add(outStock);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add()
        {
            DataProvider.Instance.Open(pathData);
            DataProvider.Instance.nodeRoot.RemoveAll();

            XmlNode newNode = null;

            foreach (var item in lstOutOfStock)
            {
                newNode = DataProvider.Instance.createNode("OutOfStock");

                XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
                attr1.Value = item.IdProduct;
                XmlAttribute attr2 = DataProvider.Instance.createAttr("NameProduct");
                attr2.Value = item.NameProduct;
                XmlAttribute attr3 = DataProvider.Instance.createAttr("Remaining");
                attr3.Value = item.Remaining.ToString();

                newNode.Attributes.Append(attr1);
                newNode.Attributes.Append(attr2);
                newNode.Attributes.Append(attr3);

                string xPath = string.Format("//OutOfStocks");
                XmlNode node = DataProvider.Instance.getNode(xPath);
                DataProvider.Instance.AppendNode(node, newNode);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(OutOfStock entity)
        {
            throw new NotImplementedException();
        }

        public void Update(OutOfStock entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(OutOfStock entity)
        {
            throw new NotImplementedException();
        }

        public OutOfStock Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<OutOfStock> Gets()
        {
            return lstOutOfStock;
        }
    }
}
