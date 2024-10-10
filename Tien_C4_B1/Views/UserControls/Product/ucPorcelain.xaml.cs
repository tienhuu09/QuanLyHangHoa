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
    /// Interaction logic for ucPorcelain.xaml
    /// </summary>
    public partial class ucPorcelain : UserControl
    {
        public PorcelainService PorcelainService { get; set; }

        public ObservableCollection<Porcelain> porcelains { get; set; }

        public Porcelain Porcelain { get; set; }
        public ucPorcelain()
        {
            InitializeComponent();

            PorcelainService = new PorcelainService();
            porcelains = new ObservableCollection<Porcelain>(PorcelainService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Porcelain = btn.DataContext as Porcelain;
                if (Porcelain != null)
                {
                    lstView.SelectedItem = Porcelain;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Porcelain.Category);
                    productDetailView.Product = Porcelain;
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
                    productDetailView.clickBtnEdit += ProductDetailView_clickBtnEdit;
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
                Porcelain porcelain = new Porcelain(item.Id, item.Name, item.Category, item.Producer, item.PriceInput, productDetail.txbMaterial.Text);
                PorcelainService.Update(porcelain);
                MessageBox.Show("Update successfully!");
                return;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Porcelain = btn.DataContext as Porcelain;
                if (Porcelain != null)
                {
                    lstView.SelectedItem = Porcelain;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Porcelain.Category);
                    productDetailView.SetIsReadOnly(Porcelain.Category);
                    productDetailView.Product = Porcelain;
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.ShowDialog();
                }
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
