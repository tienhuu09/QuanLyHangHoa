using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class ImportInventory
    {
        public string IdProduct { get; set; }
        public double Previous { get; set; }
        public double AmountPre { get; set; }
        public double Recent { get; set; }
        public double AmountRecent { get; set; }
        public DateTime DateReceipt { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }

        public Product Product { get; set; }

        public ImportInventory()
        {
            this.Previous = 0;
            this.AmountPre = 0;
            this.Recent = 0;
            this.AmountPre = 0;
            this.Quantity = 0;
            this.Total = 0;
            DateReceipt = new DateTime(1900, 1, 1);
            Product = new Product();
        }

    }
}
