using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private string pathData { get; } = "Data/Invoices/Invoice.xml";

        public List<Invoice> lstInvoice { get; set; }

        public InvoiceRepository()
        {
            lstInvoice = new List<Invoice>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList nodeList = DataProvider.Instance.getDsNode("//Invoice");

            Invoice invoice = null;
            foreach (XmlNode item in nodeList)
            {
                invoice = new Invoice();
                invoice.Id = item.Attributes["Id"].Value;
                invoice.UserName = item.Attributes["UserName"].Value;
                invoice.Quantity = Int32.Parse(item.Attributes["Quantity"].Value);
                invoice.Total = double.Parse(item.Attributes["Total"].Value);
                invoice.CreateAt = DateTime.ParseExact(item.Attributes["CreateAt"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);

                InvoiceDetail invoiceDt = null;
                foreach (XmlNode item2 in item)
                {
                    invoiceDt = new InvoiceDetail();
                    invoiceDt.IdInvoice = item2.Attributes["IdInvoice"].Value;
                    invoiceDt.IdProduct = item2.Attributes["IdProduct"].Value;
                    invoiceDt.Name = item2.Attributes["Name"].Value;
                    invoiceDt.Category = item2.Attributes["Category"].Value;
                    invoiceDt.Producer = item2.Attributes["Producer"].Value;
                    invoiceDt.PriceOutput = double.Parse(item2.Attributes["PriceOutput"].Value);
                    invoiceDt.Quantity = Int32.Parse(item2.Attributes["Quantity"].Value);

                    invoice.lstInvoiceDt.Add(invoiceDt);
                }
                lstInvoice.Add(invoice);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Invoice invoice)
        {
            lstInvoice.Add(invoice);

            DataProvider.Instance.Open(pathData);

            XmlNode newNode = DataProvider.Instance.createNode("Invoice");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = invoice.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("UserName");
            attr2.Value = invoice.UserName;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Quantity");
            attr3.Value = invoice.Quantity.ToString();
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Total");
            attr4.Value = invoice.Total.ToString();
            XmlAttribute attr5 = DataProvider.Instance.createAttr("CreateAt");
            attr5.Value = invoice.CreateAt.ToString(Ulti.date);

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            XmlNode receiptDtNode = null;
            foreach (var item in invoice.lstInvoiceDt)
            {
                receiptDtNode = DataProvider.Instance.createNode("ReceiptDetail");

                attr1 = DataProvider.Instance.createAttr("IdInvoice");
                attr1.Value = item.IdInvoice;
                attr2 = DataProvider.Instance.createAttr("IdProduct");
                attr2.Value = item.IdProduct;
                attr3 = DataProvider.Instance.createAttr("Name");
                attr3.Value = item.Name;
                attr4 = DataProvider.Instance.createAttr("Category");
                attr4.Value = item.Category;
                attr5 = DataProvider.Instance.createAttr("Producer");
                attr5.Value = item.Producer;
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

            string xPath = string.Format("//Invoices");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Invoice Get(string id)
        {
            foreach (var item in lstInvoice)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Invoice> Gets()
        {
            return lstInvoice;
        }
    }
}
