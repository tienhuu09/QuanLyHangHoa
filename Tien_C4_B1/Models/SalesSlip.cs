using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class SalesSlip
    {
        private double _score;

        public string Id { get; set; }
        public string UserName { get; set; }
        public double Quantity { get; set; }
        public DateTime CreateAt { get; set; }
        public double TotalDiscount { get; set; }
        public double Total { get; set; }
        public double getScore
        {
            get { return _score = this.Total * 0.001; }
            set { _score = value; }
        }

        public Customer customer { get; set; }
        public List<SalesSlipDetail> lstSalesDetail { get; set; }

        public SalesSlip()
        {
            CreateAt = DateTime.Now;
            customer = new Customer();
            customer.Card = "guest";
            lstSalesDetail = new List<SalesSlipDetail>();
        }

        public SalesSlip(string id, string userName, DateTime date, Customer customer)
        {
            this.Id = id;
            this.UserName = userName;
            this.customer = customer;
            this.TotalDiscount = 0;
            this.Quantity = 0;
            this.Total = 0;
            this.CreateAt = date;
            customer = new Customer();
            lstSalesDetail = new List<SalesSlipDetail>();
        }

    }
}
