using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class FoodReceiptRepository : IRepository<FoodReceipt>
    {
        private string pathData { get; } = "Data/Receipts/FoodReceipt.xml";
        public List<FoodReceipt> lstFoodReceipt { get; set; }

        public FoodReceiptRepository()
        {
            lstFoodReceipt = new List<FoodReceipt>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            string xpath = string.Format("//FoodReceipt");

            XmlNodeList nodeList = DataProvider.Instance.getDsNode(xpath);

            FoodReceipt foodR = null;
            foreach (XmlNode item in nodeList)
            {
                foodR = new FoodReceipt();
                foodR.No = item.Attributes["No"].Value;
                foodR.IdProduct = item.Attributes["IdProduct"].Value;
                foodR.IdReceipt = item.Attributes["IdReceipt"].Value;
                foodR.Name = item.Attributes["Name"].Value;
                foodR.Quantity = Int32.Parse(item.Attributes["Quantity"].Value);
                foodR.ExpQuan = Int32.Parse(item.Attributes["ExpQuan"].Value);
                foodR.MfgDate = DateTime.ParseExact(item.Attributes["MfgDate"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);
                foodR.ExpDate = DateTime.ParseExact(item.Attributes["ExpDate"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);
                if (Ulti.CheckTimeValid(DateTime.Now, foodR.ExpDate))
                {
                    foodR.Status = false;
                    item.Attributes["Status"].Value = "false";
                }
                else
                {
                    foodR.Status = true;
                    item.Attributes["Status"].Value = "true";
                }

                lstFoodReceipt.Add(foodR);
            }
            DataProvider.Instance.Close(pathData);
        }

        public void Add(FoodReceipt foodReceipt)
        {
            lstFoodReceipt.Add(foodReceipt);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("FoodReceipt");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("No");
            attr1.Value = foodReceipt.No;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("IdProduct");
            attr2.Value = foodReceipt.IdProduct;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("IdReceipt");
            attr3.Value = foodReceipt.IdReceipt;
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Name");
            attr4.Value = foodReceipt.Name;
            XmlAttribute attr5 = DataProvider.Instance.createAttr("Quantity");
            attr5.Value = foodReceipt.Quantity.ToString();
            XmlAttribute attr6 = DataProvider.Instance.createAttr("ExpQuan");
            attr6.Value = foodReceipt.ExpQuan.ToString();
            XmlAttribute attr7 = DataProvider.Instance.createAttr("MfgDate");
            attr7.Value = foodReceipt.MfgDate.ToString(Ulti.date);
            XmlAttribute attr8 = DataProvider.Instance.createAttr("ExpDate");
            attr8.Value = foodReceipt.ExpDate.ToString(Ulti.date);
            XmlAttribute attr9 = DataProvider.Instance.createAttr("Status");
            attr9.Value = foodReceipt.Status.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);
            newNode.Attributes.Append(attr9);

            string xpath = string.Format("//FoodReceipts");
            XmlNode node = DataProvider.Instance.getNode(xpath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(FoodReceipt entity)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//FoodReceipt[@No='{0}']", entity.No);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["ExpQuan"].InnerText = entity.ExpQuan.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(FoodReceipt entity)
        {
            throw new NotImplementedException();
        }

        public FoodReceipt Get(string id)
        {
            foreach (var item in lstFoodReceipt)
                if (string.Compare(item.IdProduct, id, true) == 0)
                    return item;
            return null;
        }

        public List<FoodReceipt> Gets()
        {
            return lstFoodReceipt;
        }
    }
}
