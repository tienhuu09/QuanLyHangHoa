using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Porcelain : Product, IDiscount
    {
        public string Material { get; set; }

        public Porcelain() : base() { }

        public Porcelain(string id, string name, string cate, string producer, double priceInput, string material) : base(id, name, cate, producer, priceInput)
        {
            this.Material = material;
        }

        public double Discount()
        {
            throw new NotImplementedException();
        }
    }
}
