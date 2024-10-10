using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Customer
    {
        public string Name { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalScore { get; set; }
        public double UseScore { get; set; }
        public string Card { get; set; }

        public List<CustomerDetail> lstCustomerDt { get; set; }

        public string CalcuScore
        {
            get
            {
                if (this.TotalScore < 10000 && this.TotalScore > 5000)
                    this.Card = "Gold";
                else if (this.TotalScore > 10000)
                    this.Card = "Platinum";
                else if (this.TotalScore > 0 && this.TotalScore < 5000)
                    this.Card = "Member";
                else
                    this.Card = "guest";
                return this.Card;
            }
        }

        public Customer()
        {
            this.Card = "guest";
            lstCustomerDt = new List<CustomerDetail>();
        }

        public Customer(string name, string idCard, string address, string phoneN)
        {
            this.Name = name;
            this.IdCard = idCard;
            this.Address = address;
            this.PhoneNumber = phoneN;
            this.TotalScore = 0;
            this.UseScore = 0;
            this.Card = "guest";
            lstCustomerDt = new List<CustomerDetail>();
        }
    }
}
