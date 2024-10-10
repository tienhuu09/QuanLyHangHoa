using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class RemainingProduct
    {
        public string IdProduct { get; set; }
        public double Quantity { get; set; }
        public Product Product { get; set; }

        public RemainingProduct()
        {
            this.Quantity = 0;
            Product = new Product();
        }

        public RemainingProduct(string idProduct, int quantity)
        {
            this.IdProduct = idProduct;
            this.Quantity = quantity;
            Product = new Product();
        }
    }
}
