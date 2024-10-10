using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class ExpDateRepository
    {
        private string pathData { get; } = "Data/Inventories/ExpDate.xml";

        public ExpDateRepository() { }

        public void SaveFile(List<FoodReceipt> lstFoodReceipt)
        {
            DataProvider.Instance.Open(pathData);
            DataProvider.Instance.nodeRoot.RemoveAll();

            XmlNode newNode = null;
            foreach (var item in lstFoodReceipt)
            {
                if (item.Status == false)
                {
                    newNode = DataProvider.Instance.createNode("ExpDate");

                    XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
                    attr1.Value = item.IdProduct;
                    XmlAttribute attr2 = DataProvider.Instance.createAttr("IdReceipt");
                    attr2.Value = item.IdReceipt;
                    XmlAttribute attr3 = DataProvider.Instance.createAttr("NameProduct");
                    attr3.Value = item.Name;
                    XmlAttribute attr4 = DataProvider.Instance.createAttr("Quantity");
                    attr4.Value = item.Quantity.ToString();
                    XmlAttribute attr5 = DataProvider.Instance.createAttr("MfgDate");
                    attr5.Value = item.MfgDate.ToString(Ulti.date);
                    XmlAttribute attr6 = DataProvider.Instance.createAttr("ExpDate");
                    attr6.Value = item.ExpDate.ToString(Ulti.date);

                    newNode.Attributes.Append(attr1);
                    newNode.Attributes.Append(attr2);
                    newNode.Attributes.Append(attr3);
                    newNode.Attributes.Append(attr4);
                    newNode.Attributes.Append(attr5);
                    newNode.Attributes.Append(attr6);

                    string xPath = string.Format("//ExpDates");
                    XmlNode node = DataProvider.Instance.getNode(xPath);
                    DataProvider.Instance.AppendNode(node, newNode);
                }
            }
            DataProvider.Instance.Close(pathData);
        }
    }
}
