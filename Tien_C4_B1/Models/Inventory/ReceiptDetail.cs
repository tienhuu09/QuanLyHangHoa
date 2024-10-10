using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tien_C4_B1
{
    public class ReceiptDetail
    {
        public double _amount;

        //public string Id { get; set; }
        public string IdReceipt { get; set; }
        public string IdProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double PriceInput { get; set; }
        public double PriceOutput { get; set; }
        public int Quantity { get; set; }
        public double AmountPriceInput
        {
            get { return _amount = this.PriceInput * this.Quantity; }
            set { _amount = value; }
        }

        public ReceiptDetail() { }

        public ReceiptDetail(string idReceipt, string idProduct, string name, string cate, double priceIn, double priceOut, int quantity)
        {
            this.IdReceipt = idReceipt;
            this.IdProduct = idProduct;
            this.Name = name;
            this.Category = cate;
            this.PriceInput = priceIn;
            this.PriceOutput = priceOut;
            this.Quantity = quantity;
        }
    }
}
