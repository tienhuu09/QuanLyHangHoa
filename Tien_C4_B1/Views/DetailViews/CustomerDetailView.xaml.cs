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
using System.Windows.Shapes;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for CustomerDetailView.xaml
    /// </summary>
    public partial class CustomerDetailView : Window
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler CustomerReturn;
        public SalesSlipService SalesSlipService {  get; set; }
        public CustomerService CustomerService { get; set; }
        public Customer Customer {  get; set; }

        public CustomerDetailView()
        {
            InitializeComponent();
            SalesSlipService = new SalesSlipService();
            CustomerService = new CustomerService();
            this.DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbIdCard.Text) || string.IsNullOrEmpty(txbCustomerName.Text) 
                || string.IsNullOrEmpty(txbAddress.Text) || string.IsNullOrEmpty(txbPhoneNumber.Text))
                MessageBox.Show("Please complete all information!");
            else
            {
                if (txbIdCard.Text != null && txbCustomerName.Text != null
                && txbAddress.Text != null && txbPhoneNumber.Text != null)
                {
                    Customer = new Customer();
                    Customer.IdCard = txbIdCard.Text;
                    Customer.Name = txbCustomerName.Text;
                    Customer.PhoneNumber = txbPhoneNumber.Text;
                    Customer.Address = txbAddress.Text;
                    Customer.Card = "guest";
                }
                this.Close();
                clickBtnAdd.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                CustomerDetail customerDetail = btn.DataContext as CustomerDetail;
                if (customerDetail != null)
                {
                    lstView.SelectedItem = customerDetail;
                    SalesSlip salesSlip = SalesSlipService.Get(customerDetail.IdSaleSlip);
                    SalesSlipDetailView salesSlipDetailView = new SalesSlipDetailView();
                    salesSlipDetailView.stkAddProduct.Visibility = Visibility.Hidden;
                    salesSlipDetailView.btnConfirm.Visibility = Visibility.Hidden;
                    salesSlipDetailView.SalesSlip = salesSlip;
                    salesSlipDetailView.ShowDialog();
                }
            }
        }

        private void btnCheckId_Click(object sender, RoutedEventArgs e)
        {
            string idCard = this.txbIdCard.Text;
            var item = CustomerService.Get(idCard);
            if (item != null)
            {
                Customer = item;
                this.Close();
                CustomerReturn.Invoke(this, EventArgs.Empty);
            }
            else
            {
                txtMessage.Visibility = Visibility.Visible;
                txbCustomerName.IsReadOnly = false;
                txbPhoneNumber.IsReadOnly = false;
                txbCard.IsReadOnly = false;
                txbAddress.IsReadOnly = false;
            }
        }
    }
}
