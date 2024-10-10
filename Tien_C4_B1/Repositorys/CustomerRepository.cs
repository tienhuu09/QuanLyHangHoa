using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class CustomerRepository : IRepoCustomer<Customer>
    {
        private string pathData { get; } = "Data/Customers/Customers.xml";
        public List<Customer> lstCustomer { get; set; }

        public CustomerRepository()
        {
            lstCustomer = new List<Customer>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList nodeList = DataProvider.Instance.getDsNode("//Customer");

            Customer customer = null;
            foreach (XmlNode item in nodeList)
            {
                customer = new Customer();
                customer.Name = item.Attributes["Name"].Value;
                customer.PhoneNumber = item.Attributes["PhoneNumber"].Value;
                customer.IdCard = item.Attributes["IdCard"].Value;
                customer.Address = item.Attributes["Address"].Value;
                customer.TotalScore = double.Parse(item.Attributes["TotalScore"].Value);
                customer.UseScore = double.Parse(item.Attributes["UseScore"].Value);
                customer.Card = item.Attributes["Card"].Value;

                CustomerDetail customerDt = null;
                foreach (XmlNode itemCusDt in item)
                {
                    customerDt = new CustomerDetail();
                    customerDt.IdSaleSlip = itemCusDt.Attributes["IdSaleSlip"].Value;
                    customerDt.Quantity = double.Parse(itemCusDt.Attributes["Quantity"].Value);
                    customerDt.Total = double.Parse(itemCusDt.Attributes["Total"].Value);
                    customerDt.Score = double.Parse(itemCusDt.Attributes["Score"].Value);
                    customerDt.UseScore = double.Parse(itemCusDt.Attributes["UseScore"].Value);
                    customerDt.DiscountByScore = double.Parse(itemCusDt.Attributes["DiscountByScore"].Value);
                    customerDt.createAt = DateTime.ParseExact(itemCusDt.Attributes["CreateAt"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);

                    customer.lstCustomerDt.Add(customerDt);
                }

                lstCustomer.Add(customer);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Customer customer)
        {
            lstCustomer.Add(customer);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("Customer");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("Name");
            attr1.Value = customer.Name;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("PhoneNumber");
            attr2.Value = customer.PhoneNumber;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("IdCard");
            attr3.Value = customer.IdCard;
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Address");
            attr4.Value = customer.Address;
            XmlAttribute attr5 = DataProvider.Instance.createAttr("TotalScore");
            attr5.Value = customer.TotalScore.ToString();
            XmlAttribute attr6 = DataProvider.Instance.createAttr("UseScore");
            attr6.Value = customer.UseScore.ToString();
            XmlAttribute attr7 = DataProvider.Instance.createAttr("Card");
            attr7.Value = customer.Card;

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            XmlNode customerDt = null;
            foreach (var item in customer.lstCustomerDt)
            {
                customerDt = DataProvider.Instance.createNode("CustomerDetail");
                attr1 = DataProvider.Instance.createAttr("IdSaleSlip");
                attr1.Value = item.IdSaleSlip;
                attr2 = DataProvider.Instance.createAttr("Quantity");
                attr2.Value = item.Quantity.ToString();
                attr3 = DataProvider.Instance.createAttr("Total");
                attr3.Value = item.Total.ToString();
                attr4 = DataProvider.Instance.createAttr("Score");
                attr4.Value = item.Score.ToString();
                attr5 = DataProvider.Instance.createAttr("UseScore");
                attr5.Value = item.UseScore.ToString();
                attr6 = DataProvider.Instance.createAttr("DiscountByScore");
                attr6.Value = item.DiscountByScore.ToString();
                attr7 = DataProvider.Instance.createAttr("CreateAt");
                attr7.Value = item.createAt.ToString(Ulti.date);

                customerDt.Attributes.Append(attr1);
                customerDt.Attributes.Append(attr2);
                customerDt.Attributes.Append(attr3);
                customerDt.Attributes.Append(attr4);
                customerDt.Attributes.Append(attr5);
                customerDt.Attributes.Append(attr6);
                customerDt.Attributes.Append(attr7);

                DataProvider.Instance.AppendNode(newNode, customerDt);
            }

            string xPath = string.Format("//Customers");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void AddCustomerDetail(Customer customer, CustomerDetail customerDetail)
        {
            DataProvider.Instance.Open(pathData);
            XmlNode customerDt = DataProvider.Instance.createNode("CustomerDetail");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("IdSaleSlip");
            attr1.Value = customerDetail.IdSaleSlip;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Quantity");
            attr2.Value = customerDetail.Quantity.ToString();
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Total");
            attr3.Value = customerDetail.Total.ToString();
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Score");
            attr4.Value = customerDetail.Score.ToString();
            XmlAttribute attr5 = DataProvider.Instance.createAttr("UseScore");
            attr5.Value = customerDetail.UseScore.ToString();
            XmlAttribute attr6 = DataProvider.Instance.createAttr("DiscountByScore");
            attr6.Value = customerDetail.DiscountByScore.ToString();
            XmlAttribute attr7 = DataProvider.Instance.createAttr("CreateAt");
            attr7.Value = customerDetail.createAt.ToString(Ulti.date);

            customerDt.Attributes.Append(attr1);
            customerDt.Attributes.Append(attr2);
            customerDt.Attributes.Append(attr3);
            customerDt.Attributes.Append(attr4);
            customerDt.Attributes.Append(attr5);
            customerDt.Attributes.Append(attr6);
            customerDt.Attributes.Append(attr7);

            string xPath = string.Format("//Customer[@IdCard='{0}']", customer.IdCard);
            XmlNode node = DataProvider.Instance.getNode(xPath);
            node.Attributes["TotalScore"].InnerText = customer.TotalScore.ToString();
            node.Attributes["UseScore"].InnerText = customer.UseScore.ToString();
            node.Attributes["Card"].InnerText = customer.Card;

            DataProvider.Instance.AppendNode(node, customerDt);

            DataProvider.Instance.Close(pathData);
        }

        public void RepairCard(Customer customer)
        {
            DataProvider.Instance.Open(pathData);
            string xPath = string.Format("//Customer[@IdCard='{0}']", customer.IdCard);
            XmlNode node = DataProvider.Instance.getNode(xPath);
            node.Attributes["Card"].InnerText = customer.Card;

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string id)
        {
            foreach (var item in lstCustomer)
                if (string.Compare(item.IdCard, id, true) == 0)
                    return item;
            return null;
        }

        public List<Customer> Gets()
        {
            return lstCustomer;
        }
    }
}
