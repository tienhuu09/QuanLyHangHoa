using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
    /// Interaction logic for SalesSlipDetailView.xaml
    /// </summary>
    public partial class SalesSlipDetailView : Window
    {
        public event EventHandler ChangedQuantityProduct;
        public event EventHandler CancelSalesSlip;
        public event EventHandler SelecedProduct;
        public event EventHandler RemoveProduct;
        public event EventHandler clickBtnAdd;

        public SalesSlip SalesSlip { get; set; }
        public SalesSlipDetail SalesSlipDetail { get; set; }
        public InventorySaleService InventorySaleService { get; set; }

        public SalesSlipDetailView()
        {
            InitializeComponent();
            InventorySaleService = new InventorySaleService();
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
                if (this.stkAddProduct.Visibility != Visibility.Hidden)
                    CancelSalesSlip.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.stkAddProduct.Visibility != Visibility.Hidden)
                CancelSalesSlip.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (SalesSlip.lstSalesDetail.Count == 0)
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

            InventorySale inventorySale = InventorySaleService.GetInventorySale(product);
            SalesSlipDetail salesSlipDetail = new SalesSlipDetail(SalesSlip.Id, product.Id, product.Name, product.Category, product.PriceInput, product.PriceOutput, quantity);
            salesSlipDetail.Discount = salesSlipDetail.AmountPrice * inventorySale.product.getDiscount();
            SalesSlip.Total += (salesSlipDetail.AmountPrice - salesSlipDetail.Discount);
            SalesSlip.Quantity += salesSlipDetail.Quantity;

            SalesSlip.TotalDiscount += salesSlipDetail.Discount;
            txbTotalDiscount.Text = SalesSlip.TotalDiscount.ToString(Ulti.spec);
            txbQuantity.Text = SalesSlip.Quantity.ToString();
            txbTotal.Text = SalesSlip.Total.ToString();
            ChangedQuantityProduct?.Invoke(this, EventArgs.Empty);
            SalesSlip.lstSalesDetail.Add(salesSlipDetail);
            lstView.Items.Refresh();
        }

        private void btnRomove_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                SalesSlipDetail = btn.DataContext as SalesSlipDetail;
                if (SalesSlipDetail != null)
                {
                    SalesSlip.Total -= SalesSlipDetail.AmountPrice;
                    SalesSlip.Total += SalesSlipDetail.Discount;
                    SalesSlip.TotalDiscount -= SalesSlipDetail.Discount;
                    txbTotal.Text = SalesSlip.Total.ToString(Ulti.spec);

                    SalesSlip.Quantity -= SalesSlipDetail.Quantity;
                    txbTotalDiscount.Text = SalesSlip.TotalDiscount.ToString(Ulti.spec);
                    txbQuantity.Text = SalesSlip.Quantity.ToString();

                    SalesSlip.lstSalesDetail.Remove(SalesSlipDetail);
                    lstView.Items.Refresh();
                    RemoveProduct?.Invoke(this, EventArgs.Empty);
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
