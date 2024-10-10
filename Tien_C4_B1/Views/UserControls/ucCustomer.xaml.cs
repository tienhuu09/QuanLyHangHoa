using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ucCustomer.xaml
    /// </summary>
    public partial class ucCustomer : UserControl
    {
        public ucButtonCrud ucButtonCrud { get; set; }

        public CustomerService CustomerService { get; set; }
        public ObservableCollection<Customer> lstCustomer { get; set; }
        public Customer Customer { get; set; }

        public ucCustomer()
        {
            InitializeComponent();

            ucButtonCrud = new ucButtonCrud();
            CustomerService = new CustomerService();
            lstCustomer = new ObservableCollection<Customer>(CustomerService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Customer = btn.DataContext as Customer;
                if (Customer != null)
                {
                    lstView.SelectedItem = Customer;
                    CustomerDetailView customerDetailView = new CustomerDetailView();
                    customerDetailView.btnConfirm.Visibility = Visibility.Hidden;
                    customerDetailView.txtCard.Visibility = Visibility.Visible;
                    customerDetailView.txbCard.Visibility = Visibility.Visible;
                    customerDetailView.txtTotalScore.Visibility = Visibility.Visible;
                    customerDetailView.txbTotalScore.Visibility = Visibility.Visible;
                    customerDetailView.btnCheckId.Visibility = Visibility.Hidden;
                    customerDetailView.Customer = Customer;
                    customerDetailView.ShowDialog();
                }
            }
        }

        private void txbId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = txbId.Text;
                lstCustomer.Clear();
                foreach (var item in CustomerService.Gets())
                {
                    if (string.Compare(item.Name, str, true) == 0 || string.Compare(item.IdCard, str, true) == 0)
                        lstCustomer.Add(item);
                    else if (string.IsNullOrEmpty(txbId.Text))
                        lstCustomer.Add(item);
                }
            }
        }
    }
}
