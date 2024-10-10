using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for ReceiptDetailView.xaml
    /// </summary>
    public partial class ReceiptDetailView : Window
    {
        public event EventHandler clickBtnAdd;

        public Receipt Receipt { get; set; }
        public FoodReceiptService FoodReceiptService { get; set; }
        public List<FoodReceipt> lstfoodReceipt { get; set; }

        public ReceiptDetailView()
        {
            InitializeComponent();
            lstfoodReceipt = new List<FoodReceipt>();
            FoodReceiptService = new FoodReceiptService();

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
            if (Receipt.lstReceiptDt.Count == 0)
            {
                MessageBox.Show("Please add product and quantity input!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to add a receipt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                clickBtnAdd?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbQuan.Text) || txbQuan.Text == "0" || cbProduct.SelectedItem == null)
            {
                Ulti.MessageBoxShow("Please complete all information");
                return;
            }

            DateTime startDate = new DateTime();
            DateTime exportDate = new DateTime();
            var product = cbProduct.SelectedItem as Product;
            if (string.Compare(product.Category, "Food", true) == 0)
            {
                if (StartDate.SelectedDate == null || EndDate.SelectedDate == null)
                {
                    Ulti.MessageBoxShow("Please select production date and expiration date!");
                    return;
                }
                else
                {
                    startDate = StartDate.SelectedDate.Value;
                    exportDate = EndDate.SelectedDate.Value;
                    var result = DateTime.Compare(exportDate, startDate);
                    if (result < 0)
                    {
                        Ulti.MessageBoxShow("The selected date is invalid");
                        return;
                    }
                }
            }

            var quantity = Int32.Parse(txbQuan.Text);
            ReceiptDetail receiptDetail = new ReceiptDetail(Receipt.Id, product.Id, product.Name, product.Category, product.PriceInput, product.PriceOutput, quantity);
            Receipt.Total += receiptDetail.AmountPriceInput;
            Receipt.Quantity += receiptDetail.Quantity;
            txbQuantity.Text = Receipt.Quantity.ToString();
            txbTotal.Text = Receipt.Total.ToString();
            Receipt.lstReceiptDt.Add(receiptDetail);
            lstView.Items.Refresh();

            if (string.Compare(product.Category, "Food", true) == 0)
            {
                FoodReceipt foodReceipt = new FoodReceipt((lstfoodReceipt.Count + FoodReceiptService.Gets().Count).ToString(), product.Id, Receipt.Id, product.Name, quantity, startDate, exportDate, true);
                lstfoodReceipt.Add(foodReceipt);
            }
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = cbProduct.SelectedItem as Product;
            this.txbQuan.Text = "0";
            txbPrice.Text = product.PriceInput.ToString(Ulti.spec);
            if (string.Compare(product.Category, "Food", true) == 0)
            {
                txtStartDate.Visibility = Visibility.Visible;
                StartDate.Visibility = Visibility.Visible;
                txtEndDate.Visibility = Visibility.Visible;
                EndDate.Visibility = Visibility.Visible;
            }
            else
            {
                txtStartDate.Visibility = Visibility.Collapsed;
                StartDate.Visibility = Visibility.Collapsed;
                txtEndDate.Visibility = Visibility.Collapsed;
                EndDate.Visibility = Visibility.Collapsed;
            }
        }

        private void btnRomove_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                ReceiptDetail receiptDetail = btn.DataContext as ReceiptDetail;
                if (receiptDetail != null)
                {
                    Receipt.Total -= receiptDetail.AmountPriceInput;
                    txbTotal.Text = Receipt.Total.ToString(Ulti.spec);

                    Receipt.Quantity -= receiptDetail.Quantity;
                    txbQuantity.Text = Receipt.Quantity.ToString();

                    Receipt.lstReceiptDt.Remove(receiptDetail);
                    lstView.Items.Refresh();
                }
            }
        }

        private void btnRomove_Loaded(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (stkAddProduct.Visibility == Visibility.Hidden)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }
    }
}
