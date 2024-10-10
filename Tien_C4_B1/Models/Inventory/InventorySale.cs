using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class InventorySale
    {
        public string IdProduct { get; set; }
        public double QuantityInvoice { get; set; }
        public double QuantitySold { get; set; }
        public double Remaining { get; set; }
        public double PriceOutput { get; set; }

        public Product product { get; set; }

        public InventorySale() { }

        public InventorySale(Product newProduct)
        {
            this.IdProduct = newProduct.Id;
            this.QuantityInvoice = 0;
            this.QuantitySold = 0;
            this.Remaining = 0;
            this.PriceOutput = newProduct.PriceOutput;
            this.product = newProduct;
        }
    }
}
