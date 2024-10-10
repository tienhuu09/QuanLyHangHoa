using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tien_C4_B1;
using Tien_C4_B1.Helpers;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AccountService AccountService { get; set; }
        public RoleService RoleService { get; set; }
        public ucAccount ucAccount { get; set; }
        public ucOverView ucOverView { get; set; }

        public ucRoleView ucRoleView { get; set; }

        public ucProductView ucProductView { get; set; }
        public Account Account { get; set; }

        public AdminView()
        {
            InitializeComponent();

            ucAccount = new ucAccount();
            ucRoleView = new ucRoleView();
            ucOverView = new ucOverView();
            ucProductView = new ucProductView();

            this.DataContext = this;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Hide();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            stkListView.Children.Clear();
            RadioButton btn = sender as RadioButton;
            string str = btn.Name;
            //selectedBtn = str;
            
            switch (str)
            {
                case "btnOverView":
                    stkListView.Children.Add(ucOverView);
                    break;
                case "btnAccount":
                    ucAccount.lstView.Items.Refresh();
                    stkListView.Children.Add(ucAccount);
                    break;
                case "btnRole":
                    ucRoleView.lstView.Items.Refresh();
                    stkListView.Children.Add(ucRoleView);
                    break;
                case "btnProduct":
                    Paramenter.flag = false;
                    stkListView.Children.Add(ucProductView);
                    break;
            }
        }
    }
}
