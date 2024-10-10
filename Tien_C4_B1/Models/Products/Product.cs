using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Product
    {
        private double _priceOutput;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Producer { get; set; }
        public double PriceInput { get; set; }
        public double PriceOutput
        {
            get { return (PriceInput + (PriceInput * 0.1) + (PriceInput * 0.3) + (PriceInput * (Constants.QuantityUser * 0.012))); }
            set { _priceOutput = value; }
        }

        public Product() { }

        public Product(string id, string name, string cate, string producer, double priceInput)
        {
            this.Id = id;
            this.Name = name;
            this.Category = cate;
            this.Producer = producer;
            this.PriceInput = priceInput;
        }

        public virtual double getDiscount()
        {
            return 0;
        }
    }
}
