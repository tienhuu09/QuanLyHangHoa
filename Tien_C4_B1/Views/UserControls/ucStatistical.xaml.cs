using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucStatistical.xaml
    /// </summary>
    public partial class ucStatistical : UserControl
    {
        public SalesSlipService SalesSlipService { get; set; }
        public CustomerService CustomerService { get; set; }
        public double Profit { get; set; }
        public double Revenue { get; set; }
        public int SalesSlipCount { get; set; }
        public int CustomerCount { get; set; }

        public ucStatistical()
        {
            SalesSlipService = new SalesSlipService();
            CustomerService = new CustomerService();
            Statistical();
            SalesSlipCount = SalesSlipService.Gets().Count;
            CustomerCount = CustomerService.Gets().Count;

            InitializeComponent();
            this.DataContext = this;
        }

        public void Statistical()
        {

            foreach (var item in SalesSlipService.Gets())
            {
                double quantity = 0;
                double revenue = 0;
                double profit = 0;
                foreach (var itemDetail in item.lstSalesDetail)
                {
                    quantity += itemDetail.Quantity;
                    revenue += (itemDetail.Quantity * itemDetail.PriceOutput) - itemDetail.Discount;
                    profit += (itemDetail.Quantity * itemDetail.PriceOutput) - (itemDetail.Quantity * itemDetail.PriceInput);
                }
                Revenue += revenue;
                Profit += profit;
            }
        }
    }
}
