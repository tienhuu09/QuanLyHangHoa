using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tien_C4_B1.Helpers;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for Electronic.xaml
    /// </summary>
    public partial class ucElectronic : UserControl
    {
        public ElectronicService ElectronicService { get; set; }

        public ObservableCollection<Electronic> electronics { get; set; }
        public Electronic Electronic { get; set; }
        public ucElectronic()
        {
            InitializeComponent();

            ElectronicService = new ElectronicService();
            electronics = new ObservableCollection<Electronic>(ElectronicService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Electronic = btn.DataContext as Electronic;
                if (Electronic != null)
                {
                    lstView.SelectedItem = Electronic;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Electronic.Category);
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
                    productDetailView.Product = Electronic;
                    productDetailView.ShowDialog();
                }
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Electronic = btn.DataContext as Electronic;
                if (Electronic != null)
                {
                    lstView.SelectedItem = Electronic;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Electronic.Category);
                    productDetailView.SetIsReadOnly(Electronic.Category);
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.clickBtnEdit += ProductDetailView_clickBtnEdit;
                    productDetailView.Product = Electronic;
                    productDetailView.ShowDialog();
                }
            }
        }

        private void ProductDetailView_clickBtnEdit(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var productDetail = sender as ProductDetailView;
                var item = productDetail.Product;
                Electronic electric = new Electronic(item.Id, item.Name, item.Category, item.Producer, item.PriceInput, Int32.Parse(productDetail.txbWarranty.Text), Int32.Parse(productDetail.txbElectricPower.Text));
                ElectronicService.Update(electric);
                MessageBox.Show("Update successfully!");
                return;
            }
        }

        private void btnEdit_Loaded(object sender, RoutedEventArgs e)
        {
            if (Paramenter.flag == true)
            {
                var btn = sender as RadioButton;
                if (btn != null)
                    btn.Visibility = Visibility.Hidden;
            }
        }
    }
}
