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

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for CashierView.xaml
    /// </summary>
    public partial class CashierView : Window
    {
        public Account Account { get; set; }
        public ucProductView ucProduct {  get; set; }

        public ucCustomer ucCustomer { get; set; }
        public ucSalesSlip ucSalesSlip { get; set; }
        public ucCardMember ucCardMember { get; set; }
        public ucStatistical ucStatistical { get; set; }
        public ucInventorySaler ucInventorySaler { get; set; }

        public CashierView()
        {
            InitializeComponent();

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
                case "btnOrder":
                    ucSalesSlip = new ucSalesSlip();
                    ucSalesSlip.UserName = Account.Name;
                    stkListView.Children.Add(ucSalesSlip);
                    break;
                case "btnCardMember":
                    ucCardMember = new ucCardMember();
                    stkListView.Children.Add(ucCardMember);
                    break;
                case "btnCustomer":
                    ucCustomer = new ucCustomer();
                    stkListView.Children.Add(ucCustomer);
                    break;
                case "btnInventory":
                    ucInventorySaler = new ucInventorySaler();
                    stkListView.Children.Add(ucInventorySaler);
                    break;
                case "btnStatistical":
                    ucStatistical = new ucStatistical();
                    stkListView.Children.Add(ucStatistical);
                    break;
            }
        }
    }
}
