using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for AccountUC.xaml
    /// </summary>
    public partial class ucAccount : UserControl
    {
        public ucButtonCrud ucButtonCrud { get; set; }
        public AccountService AccountService { get; set; }
        public RoleService RoleService { get; set; }

        public ObservableCollection<Account> accounts { get; set; }
        public ObservableCollection<Role> roles { get; set; }

        public Account Account { get; set; }

        public ucAccount()
        {
            InitializeComponent();
            RoleService = new RoleService();
            ucButtonCrud = new ucButtonCrud();
            AccountService = new AccountService();
            accounts = new ObservableCollection<Account>(AccountService.Gets());

            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;

            stkButton.Children.Clear();
            ucButtonCrud.cbCategory.Visibility = Visibility.Hidden;
            stkButton.Children.Add(ucButtonCrud);
            this.DataContext = this;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            AccountDetailView accountDetailView = new AccountDetailView();
            accountDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
            Account = new Account();
            string id = null;
            do
            {
                id = "A" + (AccountService.Gets().Count + 1).ToString();
            } while (AccountService.getAccountById(id) != null);
            Account.IdAccount = id;
            accountDetailView.Account = Account;
            Account.IdRole = "Empty";
            accountDetailView.cbSelectionRole.ItemsSource = RoleService.GetsRoleEmpty();

            accountDetailView.SetIsReadOnly();
            accountDetailView.clickBtnAdd += AccountDetailView_clickBtnAdd;
            accountDetailView.ShowDialog();
        }

        private void AccountDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var accountDetailView = sender as AccountDetailView;
            var item = accountDetailView.Account;
            if (AccountService.isExistUsername(item.Username))
            {
                MessageBox.Show("User name is existed, please try again!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AccountService.Add(item);
                accounts.Add(Account);
                if (accountDetailView.cbSelectionRole.SelectedItem != null)
                    RoleService.roleRepo.Update(Account.Role);
                MessageBox.Show("Added accout successfully!");
                return;
            }
            else
                return;
            
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Account = btn.DataContext as Account;
                if (Account != null)
                {
                    lstView.SelectedItem = Account;
                    AccountDetailView accountDetailView = new AccountDetailView();
                    accountDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
                    accountDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    accountDetailView.Account = Account;
                    accountDetailView.cbSelectionRole.Items.Add(Account.Role);
                    accountDetailView.cbSelectionRole.SelectedIndex = 0;
                    accountDetailView.cbSelectionRole.IsEnabled = false;
                    accountDetailView.ShowDialog();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Account = btn.DataContext as Account;
                if (Account != null)
                {
                    lstView.SelectedItem = Account;
                    AccountDetailView accountDetailView = new AccountDetailView();
                    accountDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    accountDetailView.clickBtnEdit += AccountDetailView_clickBtnEdit;
                    if (Account.IdRole != "empty" && Account.Role.RoleName != null)
                    {
                        accountDetailView.btnCancelRole.Visibility = Visibility.Visible;
                        accountDetailView.clickBtnCancel += AccountDetailView_clickBtnCancel;
                    }
                    accountDetailView.Account = Account;
                    if (string.Compare(Account.Username, "a", true) == 0 && string.Compare(Account.Password, "a", true) == 0)
                    {
                        roles = new ObservableCollection<Role>();
                        accountDetailView.btnCancelRole.Visibility = Visibility.Collapsed;
                    }
                    else
                        roles = new ObservableCollection<Role>(RoleService.GetsRoleEmpty());
                    roles.Add(Account.Role);
                    accountDetailView.cbSelectionRole.ItemsSource = roles;
                    accountDetailView.cbSelectionRole.SelectedItem = Account.Role;
                    accountDetailView.SetIsReadOnly();
                    accountDetailView.ShowDialog();
                }
            }
        }

        private void AccountDetailView_clickBtnCancel(object sender, EventArgs e)
        {
            var item = sender as AccountDetailView;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Account.Role.UserName = "empty";
                RoleService.Update(Account.Role);
                Account.IdRole = "empty";
                Account.Role = new Role();
                AccountService.Update(Account);
                lstView.Items.Refresh();
                MessageBox.Show("Cancel role successfully!");
            }
            else
                return;
        }

        private void AccountDetailView_clickBtnEdit(object sender, EventArgs e)
        {
            var item = sender as AccountDetailView;
            var account = item.Account;
            Role role = RoleService.Get(item.Role.IdRole);
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AccountService.Update(account);
                if (account.Role.RoleName != null)
                {
                    RoleService.Update(account.Role);
                    if (role != null)
                    {
                        role.UserName = "empty";
                        RoleService.Update(role);
                    }
                }
                lstView.Items.Refresh();
                MessageBox.Show("Update successfully!");
                return;
            }
            else
                return;
        }

        private void btnRomove_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Account = btn.DataContext as Account;
                if (Account != null)
                {
                    lstView.SelectedItem = Account;
                    if (string.Compare(Account.Username, "a", true) == 0 && string.Compare(Account.Password, "a", true) == 0)
                    {
                        MessageBox.Show("This account cannot be deleted");
                        return;
                    }
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Account.Role.UserName = "empty";
                        if (Account.Role.RoleName != null)
                            RoleService.Update(Account.Role);
                        AccountService.Remove(Account);
                        accounts.Remove(Account);
                        MessageBox.Show("Remove successfully!");
                        return;
                    }
                }
            }
        }
    }
}
