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
    /// Interaction logic for ucCardMember.xaml
    /// </summary>
    public partial class ucCardMember : UserControl
    {
        public CardSevice CardSevice { get; set; }
        public ucButtonCrud ucButtonCrud { get; set; }
        public CustomerService CustomerService { get; set; }
        public ObservableCollection<Card> lstCard { get; set; }

        public ucCardMember()
        {
            InitializeComponent();
            CardSevice = new CardSevice();
            ucButtonCrud = new ucButtonCrud();
            CustomerService = new CustomerService();
            lstCard = new ObservableCollection<Card>(CardSevice.Gets());
            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;
            ucButtonCrud.cbCategory.Visibility = Visibility.Hidden;
            stkButton.Children.Add(ucButtonCrud);
            this.DataContext = this;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            CustomerDetailView customerDetailView = new CustomerDetailView();
            customerDetailView.txbIdCard.IsReadOnly = false;
            customerDetailView.CustomerReturn += CustomerDetailView_CustomerReturn;
            customerDetailView.clickBtnAdd += CustomerDetailView_clickBtnAdd;
            customerDetailView.borderListView.Visibility = Visibility.Hidden;
            customerDetailView.ShowDialog();
        }

        private void CustomerDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var customerDetailView = sender as CustomerDetailView;
            Customer customer = customerDetailView.Customer;
            string str1 = string.Format("Id: {0}\nName: {1}\nAre you sure you want to create this customer ?", customer.IdCard, customer.Name);
            MessageBoxResult result = MessageBox.Show(str1, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Card card = new Card(customer.IdCard, customer.Name, DateTime.Now);
                lstCard.Add(card);
                customer.Card = "Member";
                CustomerService.Add(customer);
                CardSevice.AddCard(card);
                MessageBox.Show("Created card successfully!");
            }
        }

        private void CustomerDetailView_CustomerReturn(object sender, EventArgs e)
        {
            var customerDetailView = sender as CustomerDetailView;
            string id = customerDetailView.txbIdCard.Text;
            Customer customer = CustomerService.Get(id);
            if (customer != null)
            {
                string str1 = string.Format("Id: {0}\nName: {1}\nThis card already exists!", customer.IdCard, customer.Name);
                if (CardSevice.Get(customer.IdCard) != null)
                {
                    MessageBox.Show(str1);
                }
                else
                {
                    string str2 = string.Format("Id: {0} is existed\nName: {1}\nDo you want to create a card for this customer?", customer.IdCard, customer.Name);
                    MessageBoxResult result = MessageBox.Show(str2, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        customer.Card = "Member";
                        Card card = new Card(customer.IdCard, customer.Name, DateTime.Now);
                        card._Card = "Member";
                        lstCard.Add(card);
                        CardSevice.AddCard(card);
                        CustomerService.customerRepo.RepairCard(customer);
                        MessageBox.Show("Created card successfully!");
                    }
                }

            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
