using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class InventoryRepository : IRepoInventory<Inventory>
    {
        private string pathData { get; } = "Data/Inventories/Inventory.xml";
        public Inventory inventory { get; set; }

        public InventoryRepository()
        {
            inventory = new Inventory();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//ImportInventory");
            XmlNodeList node = DataProvider.Instance.getDsNode(xPath);
            ImportInventory importInven = null;
            foreach (XmlNode itemSt in node)
            {
                importInven = new ImportInventory();
                importInven.IdProduct = itemSt.Attributes["IdProduct"].Value;
                importInven.Previous = double.Parse(itemSt.Attributes["Previous"].Value);
                importInven.AmountPre = double.Parse(itemSt.Attributes["AmountPre"].Value);
                importInven.Recent = double.Parse(itemSt.Attributes["Recent"].Value);
                importInven.AmountRecent = double.Parse(itemSt.Attributes["AmountRecent"].Value);
                importInven.DateReceipt = DateTime.ParseExact(itemSt.Attributes["ReceiptDate"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);
                importInven.Quantity = double.Parse(itemSt.Attributes["Quantity"].Value);
                importInven.Total = double.Parse(itemSt.Attributes["Total"].Value);
                inventory.lstImport.Add(importInven);
            }

            xPath = string.Format("//ExportInventory");
            node = DataProvider.Instance.getDsNode(xPath);
            ExportInventory exportInven = null;
            foreach (XmlNode itemOutSt in node)
            {
                exportInven = new ExportInventory();
                exportInven.IdProduct = itemOutSt.Attributes["IdProduct"].Value;
                exportInven.Previous = double.Parse(itemOutSt.Attributes["Previous"].Value);
                exportInven.AmountPre = double.Parse(itemOutSt.Attributes["AmountPre"].Value);
                exportInven.Recent = double.Parse(itemOutSt.Attributes["Recent"].Value);
                exportInven.AmountRecent = double.Parse(itemOutSt.Attributes["AmountRecent"].Value);
                string str = itemOutSt.Attributes["InvoiceDate"].Value;
                if (!(str.CompareTo("0") == 0))
                    exportInven.DateInvoice = DateTime.ParseExact(itemOutSt.Attributes["InvoiceDate"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);
                exportInven.Quantity = double.Parse(itemOutSt.Attributes["Quantity"].Value);
                exportInven.Total = double.Parse(itemOutSt.Attributes["Total"].Value);
                inventory.lstExport.Add(exportInven);
            }

            xPath = string.Format("//RemainingProduct");
            node = DataProvider.Instance.getDsNode(xPath);
            RemainingProduct remain = null;
            foreach (XmlNode itemRemain in node)
            {
                remain = new RemainingProduct();
                remain.IdProduct = itemRemain.Attributes["IdProduct"].Value;
                remain.Quantity = Int32.Parse(itemRemain.Attributes["Quantity"].Value);

                inventory.lstRemain.Add(remain);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void AddStockProduct(ImportInventory import)
        {
            inventory.lstImport.Add(import);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("ImportInventory");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
            attr1.Value = import.Product.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Previous");
            attr2.Value = "0";
            XmlAttribute attr3 = DataProvider.Instance.createAttr("AmountPre");
            attr3.Value = "0";
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Recent");
            attr4.Value = "0";
            XmlAttribute attr5 = DataProvider.Instance.createAttr("AmountRecent");
            attr5.Value = "0";
            XmlAttribute attr6 = DataProvider.Instance.createAttr("ReceiptDate");
            attr6.Value = "01/01/1900 00:00:00";
            XmlAttribute attr7 = DataProvider.Instance.createAttr("Quantity");
            attr7.Value = "0";
            XmlAttribute attr8 = DataProvider.Instance.createAttr("Total");
            attr8.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            string xpath = string.Format("//ImportInventorys");
            XmlNode node = DataProvider.Instance.getNode(xpath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void AddOutOfStock(ExportInventory export)
        {
            inventory.lstExport.Add(export);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("ExportInventory");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
            attr1.Value = export.Product.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Previous");
            attr2.Value = "0";
            XmlAttribute attr3 = DataProvider.Instance.createAttr("AmountPre");
            attr3.Value = "0";
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Recent");
            attr4.Value = "0";
            XmlAttribute attr5 = DataProvider.Instance.createAttr("AmountRecent");
            attr5.Value = "0";
            XmlAttribute attr6 = DataProvider.Instance.createAttr("InvoiceDate");
            attr6.Value = "01/01/1900 00:00:00";
            XmlAttribute attr7 = DataProvider.Instance.createAttr("Quantity");
            attr7.Value = "0";
            XmlAttribute attr8 = DataProvider.Instance.createAttr("Total");
            attr8.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);

            string xpath = string.Format("//ExportInventorys");
            XmlNode node = DataProvider.Instance.getNode(xpath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void AddRemainingProduct(RemainingProduct remaining)
        {
            inventory.lstRemain.Add(remaining);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("RemainingProduct");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdProduct");
            attr1.Value = remaining.Product.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Quantity");
            attr2.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);

            string xpath = string.Format("//RemainingProducts");
            XmlNode node = DataProvider.Instance.getNode(xpath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void RepairReceipt(Receipt receipt, ImportInventory import)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//ImportInventory[@IdProduct='{0}']", import.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Previous"].InnerText = import.Previous.ToString();
            node.Attributes["AmountPre"].InnerText = import.AmountPre.ToString();

            node.Attributes["Recent"].InnerText = import.Recent.ToString();
            node.Attributes["AmountRecent"].InnerText = import.AmountRecent.ToString();

            node.Attributes["Quantity"].InnerText = import.Quantity.ToString();
            node.Attributes["Total"].InnerText = import.Total.ToString();

            node.Attributes["ReceiptDate"].InnerText = receipt.CreateAt.ToString(Ulti.date);

            DataProvider.Instance.Close(pathData);
        }

        public void RepairInvoice(Invoice invoice, ExportInventory export)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//ExportInventory[@IdProduct='{0}']", export.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Previous"].InnerText = export.Previous.ToString();
            node.Attributes["AmountPre"].InnerText = export.AmountPre.ToString();

            node.Attributes["Recent"].InnerText = export.Recent.ToString();
            node.Attributes["AmountRecent"].InnerText = export.AmountRecent.ToString();

            node.Attributes["Quantity"].InnerText = export.Quantity.ToString();
            node.Attributes["Total"].InnerText = export.Total.ToString();

            node.Attributes["InvoiceDate"].InnerText = invoice.CreateAt.ToString(Ulti.date);

            DataProvider.Instance.Close(pathData);
        }

        public void RepairRemaining(RemainingProduct remaining)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//RemainingProduct[@IdProduct='{0}']", remaining.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Quantity"].InnerText = remaining.Quantity.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void AddFoodReceiptDetail(RemainingProduct remaining, ReceiptDetail receiptDetail)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//RemainingProduct[@IdProduct='{0}']", remaining.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Quantity"].InnerText = (double.Parse(node.Attributes["Quantity"].Value) + receiptDetail.Quantity).ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void ReduceRemainingProduct(RemainingProduct remaining, InvoiceDetail invoiceDetail)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//RemainingProduct[@IdProduct='{0}']", remaining.IdProduct);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Quantity"].InnerText = (double.Parse(node.Attributes["Quantity"].Value) - invoiceDetail.Quantity).ToString();

            DataProvider.Instance.Close(pathData);
        }

        public Inventory Get()
        {
            return inventory;
        }
    }
}
