using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class InvoiceDetail
    {
        public double _amount;

        public string IdInvoice { get; set; }
        public string IdProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Producer { get; set; }
        public double PriceOutput { get; set; }
        public int Quantity { get; set; }
        public double AmountPriceOutput
        {
            get { return _amount = this.PriceOutput * this.Quantity; }
            set { _amount = value; }
        }

        public InvoiceDetail() { }

        public InvoiceDetail(string idInvoice, string idProduct, string name, string cate, string producer, double priceOut, int quantity)
        {
            this.IdInvoice = idInvoice;
            this.IdProduct = idProduct;
            this.Name = name;
            this.Category = cate;
            this.Producer = producer;
            this.PriceOutput = priceOut;
            this.Quantity = quantity;
        }
    }
}
