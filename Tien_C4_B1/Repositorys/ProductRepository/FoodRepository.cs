using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class FoodRepository : IRepository<Food>
    {
        private string pathData { get; } = "Data/Products/Foods.xml";
        public List<Food> lstFood { get; set; }

        public FoodRepository()
        {
            lstFood = new List<Food>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);

            XmlNodeList lstNode = DataProvider.Instance.getDsNode("//Product");

            Food food = null;
            foreach (XmlNode item in lstNode)
            {
                food = new Food();
                food.Id = item.Attributes["Id"].Value;
                food.Name = item.Attributes["Name"].Value;
                food.Category = item.Attributes["Category"].Value;
                food.Producer = item.Attributes["Producer"].Value;
                food.PriceInput = double.Parse(item.Attributes["PriceInput"].Value);
                food.PriceOutput = double.Parse(item.Attributes["PriceOutput"].Value);
                
                lstFood.Add(food);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Food food)
        {
            lstFood.Add(food);

            DataProvider.Instance.Open(pathData);

            XmlNode newNode = DataProvider.Instance.createNode("Product");
            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = food.Id;

            XmlAttribute attr2 = DataProvider.Instance.createAttr("Name");
            attr2.Value = food.Name;

            XmlAttribute attr3 = DataProvider.Instance.createAttr("Category");
            attr3.Value = food.Category;

            XmlAttribute attr4 = DataProvider.Instance.createAttr("Producer");
            attr4.Value = food.Producer;

            XmlAttribute attr5 = DataProvider.Instance.createAttr("PriceInput");
            attr5.Value = food.PriceInput.ToString();

            XmlAttribute attr6 = DataProvider.Instance.createAttr("PriceOutput");
            attr6.Value = food.PriceOutput.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);

            string xPath = string.Format("//Products");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Remove(Food food)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", food.Id);
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

        public void Update(Food food)
        {
            DataProvider.Instance.Open(pathData);

            string xPath = string.Format("//Product[@Id='{0}']", food.Id);
            XmlNode node = DataProvider.Instance.getNode(xPath);

            node.Attributes["Name"].InnerText = food.Name;
            node.Attributes["Producer"].InnerText = food.Producer;
            node.Attributes["PriceInput"].InnerText = food.PriceInput.ToString();
            node.Attributes["PriceOutput"].InnerText = food.PriceOutput.ToString();

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Food entity)
        {
            throw new NotImplementedException();
        }

        public Food Get(string id)
        {
            foreach (var item in lstFood)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Food> Gets()
        {
            return lstFood;
        }
    }
}
