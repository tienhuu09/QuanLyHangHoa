using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Card
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public double Score { get; set; }
        public string _Card { get; set; }
        public DateTime CreateAt { get; set; }

        public Card() { }

        public Card(string id, string customer, DateTime date)
        {
            this.Id = id;
            this.Customer = customer;
            this.Score = 0;
            this._Card = "Member";
            this.CreateAt = date;
        }

        public Card(string id, string customer, double score)
        {
            this.Id = id;
            this.Customer = customer;
            this.Score = score;
        }
    }
}
