using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class SalesSlipRepository : IRepository<SalesSlip>
    {
        private string pathData { get; } = "Data/SalesSlips/SalesSlip.xml";
        public List<SalesSlip> lstSalesSlip { get; set; }

        public SalesSlipRepository()
        {
            lstSalesSlip = new List<SalesSlip>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList nodeList = DataProvider.Instance.getDsNode("//SalesSlip");

            SalesSlip sales = null;
            foreach (XmlNode item in nodeList)
            {
                sales = new SalesSlip();
                sales.Id = item.Attributes["Id"].Value;
                sales.UserName = item.Attributes["UserName"].Value;
                sales.customer.Name = item.Attributes["Customer"].Value;
                sales.customer.PhoneNumber = item.Attributes["PhoneNumber"].Value;
                sales.customer.IdCard = item.Attributes["IdCard"].Value;
                sales.customer.Address = item.Attributes["Address"].Value;
                sales.Quantity = double.Parse(item.Attributes["Quantity"].Value);
                sales.Total = double.Parse(item.Attributes["Total"].Value);
                sales.TotalDiscount = double.Parse(item.Attributes["TotalDiscount"].Value);
                sales.CreateAt = DateTime.ParseExact(item.Attributes["CreateAt"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);

                SalesSlipDetail salesDetail = null;
                foreach (XmlNode itemDetail in item)
                {
                    salesDetail = new SalesSlipDetail();
                    salesDetail.IdSalesSlip = itemDetail.Attributes["IdSalesSlip"].Value;
                    salesDetail.IdProduct = itemDetail.Attributes["IdProduct"].Value;
                    salesDetail.Name = itemDetail.Attributes["Name"].Value;
                    salesDetail.Category = itemDetail.Attributes["Category"].Value;
                    salesDetail.PriceInput = double.Parse(itemDetail.Attributes["PriceInput"].Value);
                    salesDetail.PriceOutput = double.Parse(itemDetail.Attributes["PriceOutput"].Value);
                    salesDetail.Discount = double.Parse(itemDetail.Attributes["Discount"].Value);
                    salesDetail.Quantity = double.Parse(itemDetail.Attributes["Quantity"].Value);

                    sales.lstSalesDetail.Add(salesDetail);
                }
                lstSalesSlip.Add(sales);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(SalesSlip saleSlip)
        {
            lstSalesSlip.Add(saleSlip);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("SalesSlip");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = saleSlip.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("UserName");
            attr2.Value = saleSlip.UserName;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Customer");
            attr3.Value = saleSlip.customer.Name;
            XmlAttribute attr4 = DataProvider.Instance.createAttr("PhoneNumber");
            attr4.Value = saleSlip.customer.PhoneNumber;
            XmlAttribute attr5 = DataProvider.Instance.createAttr("IdCard");
            attr5.Value = saleSlip.customer.IdCard;
            XmlAttribute attr6 = DataProvider.Instance.createAttr("Address");
            attr6.Value = saleSlip.customer.Address;
            XmlAttribute attr7 = DataProvider.Instance.createAttr("Quantity");
            attr7.Value = saleSlip.Quantity.ToString();
            XmlAttribute attr8 = DataProvider.Instance.createAttr("Total");
            attr8.Value = saleSlip.Total.ToString();
            XmlAttribute attr9 = DataProvider.Instance.createAttr("TotalDiscount");
            attr9.Value = saleSlip.TotalDiscount.ToString();
            XmlAttribute attr10 = DataProvider.Instance.createAttr("CreateAt");
            attr10.Value = saleSlip.CreateAt.ToString(Ulti.date);

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);
            newNode.Attributes.Append(attr8);
            newNode.Attributes.Append(attr9);
            newNode.Attributes.Append(attr10);

            XmlNode saleDt = null;
            foreach (var item in saleSlip.lstSalesDetail)
            {
                saleDt = DataProvider.Instance.createNode("SalesSlipDetail");
                attr1 = DataProvider.Instance.createAttr("IdSalesSlip");
                attr1.Value = item.IdSalesSlip;
                attr2 = DataProvider.Instance.createAttr("IdProduct");
                attr2.Value = item.IdProduct;
                attr3 = DataProvider.Instance.createAttr("Name");
                attr3.Value = item.Name;
                attr4 = DataProvider.Instance.createAttr("Category");
                attr4.Value = item.Category;
                attr5 = DataProvider.Instance.createAttr("PriceInput");
                attr5.Value = item.PriceInput.ToString();
                attr6 = DataProvider.Instance.createAttr("PriceOutput");
                attr6.Value = item.PriceOutput.ToString();
                attr7 = DataProvider.Instance.createAttr("Quantity");
                attr7.Value = item.Quantity.ToString();
                attr8 = DataProvider.Instance.createAttr("Discount");
                attr8.Value = item.Discount.ToString();

                saleDt.Attributes.Append(attr1);
                saleDt.Attributes.Append(attr2);
                saleDt.Attributes.Append(attr3);
                saleDt.Attributes.Append(attr4);
                saleDt.Attributes.Append(attr5);
                saleDt.Attributes.Append(attr6);
                saleDt.Attributes.Append(attr7);
                saleDt.Attributes.Append(attr8);

                DataProvider.Instance.AppendNode(newNode, saleDt);
            }

            string xPath = string.Format("//SalesSlips");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(SalesSlip entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SalesSlip entity)
        {
            throw new NotImplementedException();
        }

        public SalesSlip Get(string id)
        {
            foreach (var item in lstSalesSlip)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<SalesSlip> Gets()
        {
            return lstSalesSlip;
        }
    }
}
