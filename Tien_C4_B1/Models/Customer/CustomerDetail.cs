using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class CustomerDetail
    {
        public string IdSaleSlip { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public double Score { get; set; }
        public double UseScore { get; set; } = 0;
        public double DiscountByScore { get; set; } = 0;
        public DateTime createAt { get; set; }

        public CustomerDetail()
        {
            UseScore = 0;
            DiscountByScore = 0;
        }
    }
}
