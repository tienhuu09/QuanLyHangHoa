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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tien_C4_B1.Helpers;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucProductView.xaml
    /// </summary>
    public partial class ucProductView : UserControl
    {
        public InventorySaleService InventorySaleService { get; set; }
        public InventoryService InventoryService { get; set; }
        public ucElectronic ucElectronic { get; set; }
        public ucPorcelain ucPorcelain { get; set; }
        public ucFood ucFood { get; set; }

        public ucButtonCrud ucButtonCrud { get; set; }
        public Product Product { get; set; }

        public ucProductView()
        {
            InitializeComponent();

            ucFood = new ucFood();
            ucPorcelain = new ucPorcelain();
            ucElectronic = new ucElectronic();
            InventoryService = new InventoryService();
            InventorySaleService = new InventorySaleService();

            ucButtonCrud = new ucButtonCrud();
            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;
            ucButtonCrud.selectionCbCategory += UcButtonCrud_selectionCbCategory;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            var str = ucButtonCrud.cbCategory.SelectedItem as string;
            Product = new Product();
            if (str == null)
                MessageBox.Show("Please select a category!");
            else
            {
                ProductDetailView productDetailView = new ProductDetailView();
                productDetailView.SetVisibility(str);
                productDetailView.SetIsReadOnly(str);
                productDetailView.btnConfirmEdit.Visibility = Visibility.Collapsed;
                productDetailView.clickBtnAdd += ProductDetailView_clickBtnAdd;
                productDetailView.Product = Product;
                Product.Id = GetId(str);
                Product.Category = str;
                productDetailView.ShowDialog();
            }
        }

        private void ProductDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var productDetail = sender as ProductDetailView;
            var item = productDetail.Product;
            switch (Product.Category)
            {
                case "Food":
                    Food food = new Food(item.Id, item.Name, item.Category, item.Producer, item.PriceInput);
                    ucFood.FoodService.Add(food);
                    ucFood.Foods.Add(food);
                    InventoryService.AddProduct(food);
                    InventorySaleService.AddProduct(food);
                    break;
                case "Porcelain":
                    Porcelain porcelain = new Porcelain(item.Id, item.Name, item.Category, item.Producer, item.PriceInput, productDetail.txbMaterial.Text);
                    ucPorcelain.PorcelainService.Add(porcelain);
                    ucPorcelain.porcelains.Add(porcelain);
                    InventoryService.AddProduct(porcelain);
                    InventorySaleService.AddProduct(porcelain);
                    break;
                case "Electronic":
                    Electronic electric = new Electronic(item.Id, item.Name, item.Category, item.Producer, item.PriceInput, Int32.Parse(productDetail.txbWarranty.Text), Int32.Parse(productDetail.txbElectricPower.Text));
                    ucElectronic.ElectronicService.Add(electric);
                    ucElectronic.electronics.Add(electric);
                    InventoryService.AddProduct(electric);
                    InventorySaleService.AddProduct(electric);
                    break;
            }
            MessageBox.Show("Added successfulyy!");
        }

        public string GetId(string cate)
        {
            string id = null;
            Random random = new Random();
            switch (cate)
            {
                case "Food":
                    do
                    {
                        id = "F" + random.Next(1, 999).ToString();
                    } while(ucFood.FoodService.isExistId(id));
                    break;
                case "Porcelain":
                    do
                    {
                        id = "P" + random.Next(1, 999).ToString();
                    } while(ucPorcelain.PorcelainService.isExistId(id));
                    break;
                case "Electronic":
                    do
                    {
                        id = "E" + random.Next(1, 999).ToString();
                    } while (ucElectronic.ElectronicService.isExistId(id));
                    break;
            }
            return id;
        }

        private void UcButtonCrud_selectionCbCategory(object sender, EventArgs e)
        {
            var str = sender as string;
            if (str != null)
                stklstView.Children.Clear();

            switch (str)
            {
                case "Food":
                    stklstView.Children.Add(ucFood);
                    break;
                case "Porcelain":
                    stklstView.Children.Add(ucPorcelain);
                    break;
                case "Electronic":
                    stklstView.Children.Add(ucElectronic);
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            stkButton.Children.Clear();
            stklstView.Children.Clear();

            stkButton.Children.Add(ucButtonCrud);
            ucButtonCrud.cbCategory.Visibility = Visibility.Visible;

            stklstView.Children.Add(ucFood);
            stklstView.Children.Add(ucElectronic);
            stklstView.Children.Add(ucPorcelain);
        }
    }
}
