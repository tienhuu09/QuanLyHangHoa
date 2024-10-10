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
    /// Interaction logic for ProductDetailView.xaml
    /// </summary>
    public partial class ProductDetailView : Window
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler clickBtnEdit;
        //public event EventHandler clickBtnRemove;

        public Product Product { get; set; }

        public ProductDetailView()
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

        public void SetVisibility(string product)
        {
            switch (product)
            {
                case "Food":
                    this.txtWarranty.Visibility = Visibility.Collapsed;
                    this.txbWarranty.Visibility = Visibility.Collapsed;
                    this.txtElectricPower.Visibility = Visibility.Collapsed;
                    this.txbElectricPower.Visibility = Visibility.Collapsed;
                    this.txtMaterial.Visibility = Visibility.Collapsed;
                    this.txbMaterial.Visibility = Visibility.Collapsed;
                    break;
                case "Porcelain":
                    this.txtWarranty.Visibility = Visibility.Collapsed;
                    this.txbWarranty.Visibility = Visibility.Collapsed;
                    this.txtElectricPower.Visibility = Visibility.Collapsed;
                    this.txbElectricPower.Visibility = Visibility.Collapsed;
                    break;
                case "Electronic":
                    this.txtMaterial.Visibility = Visibility.Collapsed;
                    this.txbMaterial.Visibility = Visibility.Collapsed;
                    break;
            }

        }

        public void SetIsReadOnly(string cate)
        {
            txbName.IsReadOnly = false;
            txbProducer.IsReadOnly = false;
            txbPriceIn.IsReadOnly = false;
            switch (cate)
            {
                case "Food":
                    break;
                case "Porcelain":
                    txbMaterial.IsReadOnly = false;
                    break;
                case "Electronic":
                    txbWarranty.IsReadOnly = false;
                    txbElectricPower.IsReadOnly = false;
                    break;
            }
        }

        private void txbPriceIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = Product.PriceOutput.ToString();
            txbPriceOut.Text = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbName.Text) || string.IsNullOrEmpty(txbProducer.Text) || string.IsNullOrEmpty(txbPriceIn.Text))
            {
                MessageBox.Show("Please complete all information");
                return;
            }
            switch (Product.Category)
            {
                case "Porcelain":
                    if (string.IsNullOrEmpty(txbMaterial.Text))
                    {
                        MessageBox.Show("Please complete all information");
                        return;
                    }
                    break;
                case "Electronic":
                    if (string.IsNullOrEmpty(txbElectricPower.Text) || string.IsNullOrEmpty(txbWarranty.Text))
                    {
                        MessageBox.Show("Please complete all information");
                        return;
                    }
                    break;
            }
            this.Close();
            clickBtnAdd?.Invoke(this, EventArgs.Empty);
        }

        private void btnConfirmEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbName.Text) || string.IsNullOrEmpty(txbProducer.Text) || string.IsNullOrEmpty(txbPriceIn.Text))
            {
                MessageBox.Show("Please complete all information");
                return;
            }
            switch (Product.Category)
            {
                case "Porcelain":
                    if (string.IsNullOrEmpty(txbMaterial.Text))
                    {
                        MessageBox.Show("Please complete all information");
                        return;
                    }
                    break;
                case "Electronic":
                    if (string.IsNullOrEmpty(txbElectricPower.Text) || string.IsNullOrEmpty(txbWarranty.Text))
                    {
                        MessageBox.Show("Please complete all information");
                        return;
                    }
                    break;
            }
            this.Close();
            clickBtnEdit?.Invoke(this, EventArgs.Empty);
        }
    }
}
