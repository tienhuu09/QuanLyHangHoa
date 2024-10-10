using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucOverView.xaml
    /// </summary>
    public partial class ucOverView : System.Windows.Controls.UserControl
    {
        public AccountService AccountService { get; set; }
        public CustomerService CustomerService { get; set; }
        public ucProductView ProductView { get; set; }
        public ReceiptService ReceiptService { get; set; }
        public InvoiceService InvoiceService { get; set; }
        public SalesSlipService SalesSlipService { get; set; }

        public int CountCustomer { get; set; }
        public int CountAccount { get; set; }
        public int CountReceipt { get; set; }
        public int CountInvoice { get; set; }
        public int CountProduct { get; set; }
        public int CountSalesSlip { get; set; }
        public double Profit { get; set; }
        public double Revenue { get; set; }
        
        public ucOverView()
        {
            SalesSlipService = new SalesSlipService();
            CustomerService = new CustomerService();
            AccountService = new AccountService();
            ReceiptService = new ReceiptService();
            InvoiceService = new InvoiceService();
            ProductView = new ucProductView();

            LoadCount();
            Statistical();
            InitializeComponent();
            this.DataContext = this;
        }

        public void LoadCount()
        {
            CountCustomer = CustomerService.Gets().Count;
            CountAccount = AccountService.Gets().Count;
            CountReceipt = ReceiptService.Gets().Count;
            CountInvoice = InvoiceService.Gets().Count;
            CountSalesSlip = SalesSlipService.Gets().Count;

            CountProduct += ProductView.ucFood.FoodService.Gets().Count;
            CountProduct += ProductView.ucPorcelain.PorcelainService.Gets().Count;
            CountProduct += ProductView.ucElectronic.ElectronicService.Gets().Count;
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
