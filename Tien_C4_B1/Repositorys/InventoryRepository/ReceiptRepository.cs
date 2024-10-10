using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class ReceiptRepository : IRepository<Receipt>
    {
        private string pathData { get; } = "Data/Receipts/Receipts.xml";
        public List<Receipt> lstReceipt { get; set; }

        public ReceiptRepository()
        {
            lstReceipt = new List<Receipt>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Receipt");

            Receipt receipt = null;
            foreach (XmlNode item in lstNode)
            {
                receipt = new Receipt();
                receipt.Id = item.Attributes["Id"].Value;
                receipt.UserName = item.Attributes["UserName"].Value;
                receipt.Quantity = Int32.Parse(item.Attributes["Quantity"].Value);
                receipt.Total = double.Parse(item.Attributes["Total"].Value);
                receipt.CreateAt = DateTime.ParseExact(item.Attributes["CreateAt"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);

                ReceiptDetail receiptDt = null;
                foreach (XmlNode itemRe in item)
                {
                    receiptDt = new ReceiptDetail();
                    //receiptDt.Id = itemRe.Attributes["Id"].Value;
                    receiptDt.IdReceipt = itemRe.Attributes["IdReceipt"].Value;
                    receiptDt.IdProduct = itemRe.Attributes["IdProduct"].Value;
                    receiptDt.Name = itemRe.Attributes["Name"].Value;
                    receiptDt.Category = itemRe.Attributes["Category"].Value;
                    receiptDt.PriceInput = double.Parse(itemRe.Attributes["PriceInput"].Value);
                    receiptDt.PriceOutput = double.Parse(itemRe.Attributes["PriceOutput"].Value);
                    receiptDt.Quantity = Int32.Parse(itemRe.Attributes["Quantity"].Value);

                    receipt.lstReceiptDt.Add(receiptDt);
                }
                lstReceipt.Add(receipt);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Receipt receipt)
        {
            lstReceipt.Add(receipt);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("Receipt");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = receipt.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("UserName");
            attr2.Value = receipt.UserName;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Quantity");
            attr3.Value = receipt.Quantity.ToString();
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Total");
            attr4.Value = receipt.Total.ToString();
            XmlAttribute attr5 = DataProvider.Instance.createAttr("CreateAt");
            attr5.Value = receipt.CreateAt.ToString(Ulti.date);

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            XmlNode receiptDtNode = null;
            foreach (var item in receipt.lstReceiptDt)
            {
                receiptDtNode = DataProvider.Instance.createNode("ReceiptDetail");

                attr1 = DataProvider.Instance.createAttr("IdReceipt");
                attr1.Value = item.IdReceipt;
                attr2 = DataProvider.Instance.createAttr("IdProduct");
                attr2.Value = item.IdProduct;
                attr3 = DataProvider.Instance.createAttr("Name");
                attr3.Value = item.Name;
                attr4 = DataProvider.Instance.createAttr("Category");
                attr4.Value = item.Category;
                attr5 = DataProvider.Instance.createAttr("PriceInput");
                attr5.Value = item.PriceInput.ToString();
                XmlAttribute attr6 = DataProvider.Instance.createAttr("PriceOutput");
                attr6.Value = item.PriceOutput.ToString();
                XmlAttribute attr7 = DataProvider.Instance.createAttr("Quantity");
                attr7.Value = item.Quantity.ToString();

                receiptDtNode.Attributes.Append(attr1);
                receiptDtNode.Attributes.Append(attr2);
                receiptDtNode.Attributes.Append(attr3);
                receiptDtNode.Attributes.Append(attr4);
                receiptDtNode.Attributes.Append(attr5);
                receiptDtNode.Attributes.Append(attr6);
                receiptDtNode.Attributes.Append(attr7);

                DataProvider.Instance.AppendNode(newNode, receiptDtNode);
            }

            string xPath = string.Format("//Receipts");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Receipt entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Receipt entity)
        {
            throw new NotImplementedException();
        }

        public Receipt Get(string id)
        {
            foreach (var item in lstReceipt)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Receipt> Gets()
        {
            return lstReceipt;
        }
    }
}
