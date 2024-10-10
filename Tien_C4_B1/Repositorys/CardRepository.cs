using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class CardRepository : IRepository<Card>
    {
        private string pathData { get; } = "Data/Customers/Cards.xml";

        public List<Card> lstCard { get; set; }

        public CardRepository()
        {
            lstCard = new List<Card>();
            Load();
        }

        public void Load()
        {
            DataProvider.Instance.Open(pathData);
            XmlNodeList nodeList = DataProvider.Instance.getDsNode("//Card");

            Card card = null;
            foreach (XmlNode item in nodeList)
            {
                card = new Card();
                card.Id = item.Attributes["Id"].Value;
                card.Customer = item.Attributes["Customer"].Value;
                card.Score = double.Parse(item.Attributes["Score"].Value);
                card._Card = item.Attributes["Card"].Value;
                card.CreateAt = DateTime.ParseExact(item.Attributes["CreateAt"].Value, Ulti.date, System.Globalization.CultureInfo.InvariantCulture);
                lstCard.Add(card);
            }

            DataProvider.Instance.Close(pathData);
        }

        public void Add(Card card)
        {
            lstCard.Add(card);

            DataProvider.Instance.Open(pathData);
            XmlNode newNode = DataProvider.Instance.createNode("Card");

            XmlAttribute attr1 = DataProvider.Instance.createAttr("Id");
            attr1.Value = card.Id;
            XmlAttribute attr2 = DataProvider.Instance.createAttr("Customer");
            attr2.Value = card.Customer;
            XmlAttribute attr3 = DataProvider.Instance.createAttr("Score");
            attr3.Value = card.Score.ToString();
            XmlAttribute attr4 = DataProvider.Instance.createAttr("Card");
            attr4.Value = card._Card;
            XmlAttribute attr5 = DataProvider.Instance.createAttr("CreateAt");
            attr5.Value = card.CreateAt.ToString(Ulti.date);

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            string xPath = string.Format("//Cards");
            XmlNode node = DataProvider.Instance.getNode(xPath);
            DataProvider.Instance.AppendNode(node, newNode);

            DataProvider.Instance.Close(pathData);
        }

        public void Update(Card entity)
        {
            DataProvider.Instance.Open(pathData);
            string xPath = string.Format("//Card[@Id='{0}']", entity.Id);
            XmlNode node = DataProvider.Instance.getNode(xPath);
            node.Attributes["Score"].InnerText = entity.Score.ToString();
            node.Attributes["Card"].InnerText = entity._Card;

            DataProvider.Instance.Close(pathData);
        }

        public void Delete(Card entity)
        {
            throw new NotImplementedException();
        }

        public Card Get(string id)
        {
            foreach (var item in lstCard)
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Card> Gets()
        {
            return lstCard;
        }
    }
}
