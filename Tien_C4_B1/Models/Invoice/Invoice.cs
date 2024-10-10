using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Invoice
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public double Total { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
        public List<InvoiceDetail> lstInvoiceDt { get; set; }

        public Invoice()
        {
            CreateAt = DateTime.Now;
            lstInvoiceDt = new List<InvoiceDetail>();
        }

        public Invoice(string id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
            this.CreateAt = DateTime.Now;
            lstInvoiceDt = new List<InvoiceDetail>();
        }

        public Invoice(string id, string userName, double total, int quantity)
        {
            this.Id = id;
            this.UserName = userName;
            this.Total = total;
            this.Quantity = quantity;
            this.CreateAt = DateTime.Now;
            lstInvoiceDt = new List<InvoiceDetail>();
        }
    }
}
