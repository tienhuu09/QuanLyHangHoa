using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class FoodReceipt
    {
        public string No { get; set; }
        public string IdProduct { get; set; }
        public string IdReceipt { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ExpQuan { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpDate { get; set; }
        public bool Status { get; set; }

        public FoodReceipt() { }

        public FoodReceipt(string no, string idProduct, string idReceipt, string name, int quantity, DateTime mfgDate, DateTime expDate, bool status)
        {
            this.No = no;
            this.IdProduct = idProduct;
            this.IdReceipt = idReceipt;
            this.Name = name;
            this.Quantity = quantity;
            this.ExpQuan = 0;
            this.MfgDate = mfgDate;
            this.ExpDate = expDate;
            this.Status = status;
        }
    }
}
