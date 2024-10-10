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
using Tien_C4_B1.Helpers;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for StockerView.xaml
    /// </summary>
    public partial class StockerView : Window
    {
        public Account Account { get; set; }
        public ucProductView ucProductView { get; set; }
        public ucInventory ucInventory { get; set; }
        public ucReceipt ucReceipt { get; set; }
        public ucInvoice ucInvoice { get; set; }
        public ucFoodReceipt ucFoodReceipt { get; set; }

        public StockerView()
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
                case "btnInventory":
                    ucInventory = new ucInventory();
                    stkListView.Children.Add(ucInventory);
                    break;
                case "btnReceipt":
                    ucReceipt = new ucReceipt();
                    ucReceipt.UserName = Account.Name;
                    ucReceipt.ReceiptChanged += UcReceipt_ReceiptChanged;
                    stkListView.Children.Add(ucReceipt);
                    break;
                case "btnInvoice":
                    ucInvoice = new ucInvoice();
                    ucInvoice.UserName = Account.Name;
                    stkListView.Children.Add(ucInvoice);
                    break;
                case "btnProduct":
                    ucProductView = new ucProductView();
                    ucProductView.ucButtonCrud.buttonAdd.Visibility = Visibility.Collapsed;
                    Paramenter.flag = true;
                    stkListView.Children.Add(ucProductView);
                    break;
                case "btnFoodReceipt":
                    ucFoodReceipt = new ucFoodReceipt();
                    stkListView.Children.Add(ucFoodReceipt);
                    ucFoodReceipt.FoodReceiptService.SaveProductExpDate();
                    break;
            }
        }

        private void UcReceipt_ReceiptChanged(object sender, EventArgs e)
        {
            if (ucFoodReceipt != null)
                ucFoodReceipt.lstView.Items.Refresh();
        }
    }
}
