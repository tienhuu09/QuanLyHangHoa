using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucSalesSlip.xaml
    /// </summary>
    public partial class ucSalesSlip : UserControl
    {
        public ucButtonCrud ucButtonCrud { get; set; }

        public ucCustomer ucCustomer { get; set; }
        public ucCardMember ucCardMember { get; set; }
        public CustomerService CustomerService { get; set; }
        public SalesSlipService SalesSlipService { get; set; }
        public InventorySaleService InventorySaleService { get; set; }
        public ObservableCollection<SalesSlip> lstSalesSlip {  get; set; }
        public SalesSlip SalesSlip { get; set; }
        public string UserName { get; set; }

        public ucSalesSlip()
        {
            InitializeComponent();
            ucButtonCrud = new ucButtonCrud();

            ucCustomer = new ucCustomer();
            ucCardMember = new ucCardMember();
            CustomerService = new CustomerService();
            SalesSlipService = new SalesSlipService();
            InventorySaleService = new InventorySaleService();
            lstSalesSlip = new ObservableCollection<SalesSlip>(SalesSlipService.Gets());
            ucButtonCrud.cbCategory.Visibility = Visibility.Hidden;
            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;
            stkButton.Children.Add(ucButtonCrud);
            this.DataContext = this;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            SalesSlip = new SalesSlip();

            CustomerDetailView customerDetailView = new CustomerDetailView();
            customerDetailView.txbIdCard.IsReadOnly = false;
            customerDetailView.borderListView.Visibility = Visibility.Hidden;
            customerDetailView.CustomerReturn += CustomerDetailView_CustomerReturn;
            customerDetailView.clickBtnAdd += CustomerDetailView_clickBtnAdd;
            customerDetailView.ShowDialog();

            if (SalesSlip.customer.Name != null && SalesSlip.customer.PhoneNumber != null
                && SalesSlip.customer.Address != null)
            {
                SalesSlip.Id = SalesSlipService.GetId();
                SalesSlip.UserName = UserName;
                SalesSlipDetailView salesSlipDetailView = new SalesSlipDetailView();
                salesSlipDetailView.SalesSlip = SalesSlip;
                salesSlipDetailView.clickBtnAdd += SalesSlipDetailView_clickBtnAdd;
                salesSlipDetailView.RemoveProduct += SalesSlipDetailView_RemoveProduct;
                salesSlipDetailView.SelecedProduct += SalesSlipDetailView_SelecedProduct;
                salesSlipDetailView.CancelSalesSlip += SalesSlipDetailView_CancelSalesSlip;
                salesSlipDetailView.ChangedQuantityProduct += SalesSlipDetailView_ChangedQuantityProduct;
                LoadComboBox(salesSlipDetailView);
                salesSlipDetailView.ShowDialog();
            }

        }

        // Đặt lại số lượng trong Inventory khi hủy phiếu bán hàng
        private void SalesSlipDetailView_CancelSalesSlip(object sender, EventArgs e)
        {
            var salesSlipDetailView = sender as SalesSlipDetailView;
            SalesSlip salesSlip = salesSlipDetailView.SalesSlip;

            foreach (var salesSlipDetail in salesSlip.lstSalesDetail)
            {
                foreach (var inventorySale in InventorySaleService.Gets())
                {
                    if (string.Compare(inventorySale.IdProduct, salesSlipDetail.IdProduct, true) == 0)
                    {
                        inventorySale.Remaining += salesSlipDetail.Quantity;
                        break;
                    }
                }
            }
        }

        // Hủy 1 sản phẩm đã chọn trong phiếu bán hàng
        private void SalesSlipDetailView_RemoveProduct(object sender, EventArgs e)
        {
            var salesSlipDetailView = sender as SalesSlipDetailView;
            var salesSlipDetail = salesSlipDetailView.SalesSlipDetail as SalesSlipDetail;
            var quantity = salesSlipDetailView.SalesSlipDetail.Quantity;
            Product product = salesSlipDetailView.cbProduct.SelectedItem as Product;

            foreach (var item in InventorySaleService.Gets())
            {
                if (string.Compare(item.IdProduct, salesSlipDetail.IdProduct, true) == 0)
                {
                    item.Remaining += quantity;
                    if (string.Compare(salesSlipDetail.IdProduct, product.Id, true) == 0)
                    {
                        salesSlipDetailView.txbTotalProduct.Text = item.Remaining.ToString();
                        Paramenter.TotalProduct = item.Remaining;
                    }
                }
            }
            salesSlipDetailView.txbQuan.Text = "0";
        }

        // Change quantiry product and update remaining quantity product
        private void SalesSlipDetailView_ChangedQuantityProduct(object sender, EventArgs e)
        {
            var salesSlipDetail = sender as SalesSlipDetailView;
            Product product = salesSlipDetail.cbProduct.SelectedItem as Product;
            var quantity = Int32.Parse(salesSlipDetail.txbQuan.Text);

            foreach (var item in InventorySaleService.Gets())
            {
                if (string.Compare(item.IdProduct, product.Id, true) == 0)
                {
                    item.Remaining -= quantity;
                    salesSlipDetail.txbTotalProduct.Text = item.Remaining.ToString();
                    Paramenter.TotalProduct = item.Remaining;
                }
            }
        }

        // Selection product and get total product
        private void SalesSlipDetailView_SelecedProduct(object sender, EventArgs e)
        {
            var salesSlipDetail = sender as SalesSlipDetailView;
            Product product = salesSlipDetail.cbProduct.SelectedItem as Product;
            foreach (var item in InventorySaleService.Gets())
            {
                if (string.Compare(item.IdProduct, product.Id, true) == 0)
                {
                    salesSlipDetail.txbTotalProduct.Text = item.Remaining.ToString();
                    Paramenter.TotalProduct = item.Remaining;
                }
            }
        }

        public void LoadComboBox(SalesSlipDetailView salesSlipDetailView)
        {
            foreach (var item in InventorySaleService.Gets())
            {
                if (item.Remaining > 0)
                    salesSlipDetailView.cbProduct.Items.Add(item.product);
            }
        }

        private void CustomerDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var item = sender as CustomerDetailView;
            SalesSlip.customer = item.Customer;
        }

        private void CustomerDetailView_CustomerReturn(object sender, EventArgs e)
        {
            var item = sender as CustomerDetailView;
            SalesSlip.customer = item.Customer;
        }

        // Add SalesSlip
        private void SalesSlipDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var salesSlipDetailView = sender as SalesSlipDetailView;
            SalesSlip salesSlip = salesSlipDetailView.SalesSlip;
            Customer customer = salesSlip.customer;

            int useScore = Convert.ToInt32(customer.TotalScore);
            double totalMoneyUseScore = useScore * 10;
            bool flag = false;

            if (totalMoneyUseScore > salesSlip.Total)
                totalMoneyUseScore = salesSlip.Total;

            // Check Customer card and use points to pay
            if (string.Compare(customer.Card, "guest", true) != 0)
            {
                string totalBill = salesSlip.Total.ToString(Ulti.spec);
                string str1 = string.Format("Total bill: {0}\nThe number of points you have: {1}\nThe amount of money you save is: {2}\nDo you want to use points?", totalBill, useScore.ToString(Ulti.spec2), totalMoneyUseScore.ToString(Ulti.spec));
                MessageBoxResult result = Ulti.MessageBoxYesNo(str1);
                if (result == MessageBoxResult.Yes)
                {
                    string message = "The total score is less than 1000 so it cannot be used";
                    if (useScore < 1000)
                        Ulti.MessageBoxShowInfo(message);
                    else
                        flag = true;
                }
            }

            if (CustomerService.Get(customer.IdCard) == null)
                CustomerService.Add(customer);

            CustomerService.AddSale(salesSlip, customer, flag);
            lstSalesSlip.Add(salesSlip);
            SalesSlipService.Add(salesSlip);
            InventorySaleService.AddSaleSlip(salesSlip);
            ucCardMember.CardSevice.Update(customer, flag);
            lstView.Items.Refresh();
            string str = string.Format("Added sales slip successfully");
            Ulti.MessageBoxShow(str);
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                SalesSlip = btn.DataContext as SalesSlip;
                if (SalesSlip != null)
                {
                    lstView.SelectedItem = SalesSlip;
                    SalesSlipDetailView salesSlipDetailView = new SalesSlipDetailView();
                    salesSlipDetailView.stkAddProduct.Visibility = Visibility.Hidden;
                    salesSlipDetailView.btnConfirm.Visibility = Visibility.Hidden;
                    salesSlipDetailView.SalesSlip = SalesSlip;
                    salesSlipDetailView.ShowDialog();
                }
            }
        }

        // Filter list salesSlip by Id
        private void txbId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string str = txbId.Text;
                lstSalesSlip.Clear();
                foreach (var item in SalesSlipService.Gets())
                {
                    if (string.Compare(item.Id, str, true) == 0 || string.Compare(item.customer.Name, str, true) == 0)
                        lstSalesSlip.Add(item);
                    else if (string.IsNullOrEmpty(txbId.Text))
                        lstSalesSlip.Add(item);
                }
            }
        }
    }
}
