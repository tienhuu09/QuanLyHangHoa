using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public AccountService accService { get; set; }

        AdminView AdminView { get; set; }
        StockerView StockerView { get; set; }
        CashierView CashierView { get; set; }

        public LoginView()
        {
            InitializeComponent();

            accService = new AccountService();

            this.DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                loginEvent();
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }

        private void loginEvent()
        {
            string strName = txtUser.Text;
            string strPass = txtPass.Password.ToString();

            Account account = accService.getAccount(strName, strPass);
            if (account == null)
            {
                MessageBox.Show("Account invalid");
                return;
            }
            
            switch (account.Role.RoleLv)
            {
                case 1:
                    this.Hide();
                    AdminView = new AdminView();
                    AdminView.Account = account;
                    AdminView.ShowDialog();
                    ResetUsernamePassword();
                    this.ShowDialog();
                        break;
                case 2:
                    this.Hide();
                    StockerView = new StockerView();
                    StockerView.Account = account;
                    StockerView.ShowDialog();
                    ResetUsernamePassword();
                    this.ShowDialog();
                    break;
                case 3:
                    this.Hide();
                    CashierView = new CashierView();
                    CashierView.Account = account;
                    CashierView.ShowDialog();
                    ResetUsernamePassword();
                    this.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Account does not have a role yet");
                    break;
            }
        }

        public void ResetUsernamePassword()
        {
            txtUser.Clear();
            txtPass.Clear();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginEvent();
        }
    }

}
