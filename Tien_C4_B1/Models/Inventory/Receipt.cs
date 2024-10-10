using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Receipt
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public DateTime CreateAt { get; set; }
        public List<ReceiptDetail> lstReceiptDt { get; set; }

        public Receipt()
        {
            this.CreateAt = DateTime.Now;
            lstReceiptDt = new List<ReceiptDetail>();
        }

        public Receipt(string id, string username)
        {
            this.Id = id;
            this.UserName = username;
            this.CreateAt = DateTime.Now;
            lstReceiptDt = new List<ReceiptDetail>();

        }
    }
}
