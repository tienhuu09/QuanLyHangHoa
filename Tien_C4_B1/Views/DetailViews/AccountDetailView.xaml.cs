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
    /// Interaction logic for AccountDetailView.xaml
    /// </summary>
    public partial class AccountDetailView : Window
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler clickBtnEdit;
        public event EventHandler clickBtnCancel;

        public Account Account { get; set; }
        public Role Role { get; set; }

        public AccountDetailView()
        {
            InitializeComponent();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbName.Text) || string.IsNullOrEmpty(txbUsername.Text) || string.IsNullOrEmpty(txbPassword.Text))
                Ulti.MessageBoxComplete();
            else
            {
                var item = cbSelectionRole.SelectedItem as Role;
                if (item != null)
                {
                    Account.IdRole = item.IdRole;
                    Account.Role = item;
                    Account.Role.UserName = Account.Name;
                }
                this.Close();
                clickBtnAdd.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnConfirmEdit_Click(object sender, RoutedEventArgs e)
        {
            Role = new Role();
            Role.IdRole = Account.IdRole;

            var item = cbSelectionRole.SelectedItem as Role;
            Account.IdRole = item.IdRole;
            Account.Role = item;
            Account.Role.UserName = Account.Name;
            if (string.Compare(Role.IdRole, Account.IdRole, true) == 0)
                Role.IdRole = "empty";
            this.Close();
            clickBtnEdit?.Invoke(this, EventArgs.Empty);
        }

        public void SetIsReadOnly()
        {
            txbName.IsReadOnly = false;
            txbUsername.IsReadOnly = false;
            txbPassword.IsReadOnly = false;
        }

        private void cbSelectionRole_Selected(object sender, SelectionChangedEventArgs e)
        {
            var item = cbSelectionRole.SelectedItem as Role;
            Account.Role = item;
        }

        private void btnCancelRole_Click(object sender, RoutedEventArgs e)
        {
            var item = cbSelectionRole.SelectedItem as Role;
            
            this.Close();
            clickBtnCancel.Invoke(this, EventArgs.Empty);
        }
    }
}
