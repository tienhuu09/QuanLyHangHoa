using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class OutOfStock
    {
        public string IdProduct { get; set; }
        public string NameProduct { get; set; }
        public double Remaining { get; set; }

        public OutOfStock() { }

        public OutOfStock(string id, string name, double remain)
        {
            this.IdProduct = id;
            this.NameProduct = name;
            this.Remaining = remain;
        }

    }
}
