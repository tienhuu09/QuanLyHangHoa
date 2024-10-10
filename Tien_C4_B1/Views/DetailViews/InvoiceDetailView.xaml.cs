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
using Tien_C4_B1.Helpers;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for InvoiceDetailView.xaml
    /// </summary>
    public partial class InvoiceDetailView : Window
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler SelecedProduct;
        public event EventHandler ChangedQuantityProduct;
        public event EventHandler RemoveProduct;
        public event EventHandler CancelInvoice;

        public Invoice Invoice { get; set; }
        public InvoiceDetail InvoiceDetail { get; set; }
        public FoodReceiptService FoodReceiptService { get; set; }

        public InvoiceDetailView()
        {
            InitializeComponent();

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
            {
                if (stkAddProduct.Visibility == Visibility.Visible)
                    CancelInvoice.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (stkAddProduct.Visibility == Visibility.Visible)
                CancelInvoice.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Invoice.lstInvoiceDt.Count == 0)
            {
                MessageBox.Show("Please add product and quantity input!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to add a invoice?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                clickBtnAdd?.Invoke(this, EventArgs.Empty);
            }
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = cbProduct.SelectedItem as Product;
            txbPrice.Text = product.PriceOutput.ToString(Ulti.spec);
            txbQuan.Text = "0";
            SelecedProduct.Invoke(this, EventArgs.Empty);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbQuan.Text) || txbQuan.Text == "0" || cbProduct.SelectedItem == null)
            {
                MessageBox.Show("Please complete all information");
                return;
            }

            var product = cbProduct.SelectedItem as Product;
            var quantity = Int32.Parse(txbQuan.Text);
            if (quantity > Paramenter.TotalProduct)
            {
                MessageBox.Show("Quantity exceeds the limit !!!\nPlease try again");
                return;
            }
            InvoiceDetail invoiceDetail = new InvoiceDetail(Invoice.Id, product.Id, product.Name, product.Category, product.Producer, product.PriceOutput, quantity);
            Invoice.Total += invoiceDetail.AmountPriceOutput;
            Invoice.Quantity += invoiceDetail.Quantity;
            txbQuantity.Text = Invoice.Quantity.ToString();
            txbTotal.Text = Invoice.Total.ToString();
            ChangedQuantityProduct?.Invoke(this, EventArgs.Empty);
            Invoice.lstInvoiceDt.Add(invoiceDetail);
            lstView.Items.Refresh();
        }

        private void btnRomove_Loaded(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (stkAddProduct.Visibility == Visibility.Hidden)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void btnRomove_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                InvoiceDetail = btn.DataContext as InvoiceDetail;
                if (InvoiceDetail != null)
                {
                    Invoice.Total -= InvoiceDetail.AmountPriceOutput;
                    txbTotal.Text = Invoice.Total.ToString(Ulti.spec);

                    Invoice.Quantity -= InvoiceDetail.Quantity;
                    txbQuantity.Text = Invoice.Quantity.ToString();

                    Invoice.lstInvoiceDt.Remove(InvoiceDetail);
                    lstView.Items.Refresh();
                    RemoveProduct?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
