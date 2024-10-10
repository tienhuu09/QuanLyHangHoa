using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Electronic : Product, IDiscount
    {
        public int Warranty { get; set; }
        public int ElectricPower { get; set; }

        public Electronic() : base()
        {

        }

        public Electronic(string id, string name, string cate, string producer, double priceInput, int warranty, int electric) : base(id, name, cate, producer, priceInput)
        {
            this.Warranty = warranty;
            this.ElectricPower = electric;
        }

        public double Discount()
        {
            return 0.15;
        }

        public override double getDiscount()
        {
            return Discount();
        }
    }
}
