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
    /// Interaction logic for ucFood.xaml
    /// </summary>
    public partial class ucFood : UserControl
    {
        public FoodService FoodService { get; set; }

        public ObservableCollection<Food> Foods { get; set; }

        public Food Food { get; set; }
        public ucFood()
        {
            InitializeComponent();
            FoodService = new FoodService();
            Foods = new ObservableCollection<Food>(FoodService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Food = btn.DataContext as Food;
                if (Food != null)
                {
                    lstView.SelectedItem = Food;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Food.Category);
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
                    productDetailView.Product = Food;
                    productDetailView.ShowDialog();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Food = btn.DataContext as Food;
                if (Food != null)
                {
                    lstView.SelectedItem = Food;
                    ProductDetailView productDetailView = new ProductDetailView();
                    productDetailView.SetVisibility(Food.Category);
                    productDetailView.SetIsReadOnly(Food.Category);
                    productDetailView.btnConfirm.Visibility = Visibility.Collapsed;
                    productDetailView.clickBtnEdit += ProductDetailView_clickBtnEdit;
                    productDetailView.Product = Food;
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
                Food food = new Food(item.Id, item.Name, item.Category, item.Producer, item.PriceInput);
                FoodService.Update(food);
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
