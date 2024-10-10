using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class SalesSlipDetail
    {
        private double _amount;

        public string IdSalesSlip { get; set; }
        public string IdProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double PriceInput { get; set; }
        public double PriceOutput { get; set; }
        public double Discount { get; set; }
        public double Quantity { get; set; }
        public double AmountPrice
        {
            get { return _amount = this.PriceOutput * this.Quantity; }
            set { _amount = value; }
        }

        public SalesSlipDetail() { }

        public SalesSlipDetail(string idSale, string idProduct, string name, string cate, double priceIn, double priceOut, double quantity)
        {
            this.IdSalesSlip = idSale;
            this.IdProduct = idProduct;
            this.Name = name;
            this.Category = cate;
            this.PriceInput = priceIn;
            this.PriceOutput = priceOut;
            this.Quantity = quantity;
        }

    }
}
