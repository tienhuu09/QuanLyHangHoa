using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Food : Product, IDiscount
    {
        public bool Status { get; set; } = true;
        public Food() : base() { }

        public Food(string id, string name, string cate, string producer, double priceInput) : base(id, name, cate, producer, priceInput)
        {
            
        }

        public double Discount()
        {
            return 0.25;
        }

        public override double getDiscount()
        {
            return Discount();
        }
    }
}
