using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucInvoice.xaml
    /// </summary>
    public partial class ucInvoice : UserControl
    {
        public ucButtonCrud ucButtonCrud { get; set; }
        public InventoryService InventoryService { get; set; }
        public InventorySaleService InventorySaleService { get; set; }
        public FoodReceiptService FoodReceiptService { get; set; }

        public InvoiceService InvoiceService { get; set; }
        public ObservableCollection<Invoice> lstInvoice { get; set; }

        public Invoice Invoice { get; set; }
        public string UserName { get; set; }

        public ucInvoice()
        {
            InitializeComponent();
            ucButtonCrud = new ucButtonCrud();
            InvoiceService = new InvoiceService();
            InventoryService = new InventoryService();
            InventorySaleService = new InventorySaleService();
            FoodReceiptService = new FoodReceiptService();
            lstInvoice = new ObservableCollection<Invoice>(InvoiceService.Gets());

            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;
            ucButtonCrud.cbCategory.Visibility = Visibility.Collapsed;
            stkButton.Children.Clear();
            stkButton.Children.Add(ucButtonCrud);

            this.DataContext = this;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            Invoice invoice = new Invoice();
            invoice.Id = InvoiceService.GetId();
            invoice.UserName = UserName;
            InvoiceDetailView invocieDetailView = new InvoiceDetailView();
            invocieDetailView.Invoice = invoice;
            invocieDetailView.clickBtnAdd += InvocieDetailView_clickBtnAdd;
            invocieDetailView.SelecedProduct += InvocieDetailView_SelecedProduct;
            invocieDetailView.ChangedQuantityProduct += InvocieDetailView_ChangedQuantityProduct;
            invocieDetailView.RemoveProduct += InvocieDetailView_RemoveProduct;
            invocieDetailView.CancelInvoice += InvocieDetailView_CancelInvoice;
            LoadComboBox(invocieDetailView);
            invocieDetailView.ShowDialog();
        }

        private void InvocieDetailView_CancelInvoice(object sender, EventArgs e)
        {
            var invoiceDetailView = sender as InvoiceDetailView;
            Invoice invoice = invoiceDetailView.Invoice;
            foreach (var invoiceDetail in invoice.lstInvoiceDt)
            {
                foreach (var remaining in InventoryService.Get().lstRemain)
                {
                    if (string.Compare(remaining.IdProduct, invoiceDetail.IdProduct, true) == 0)
                    {
                        remaining.Quantity += invoiceDetail.Quantity;
                    }
                } 
            }
        }

        private void InvocieDetailView_RemoveProduct(object sender, EventArgs e)
        {
            var invoiceDetailView = sender as InvoiceDetailView;
            var invoiceDetail = invoiceDetailView.InvoiceDetail as InvoiceDetail;
            var quantity = invoiceDetailView.InvoiceDetail.Quantity;
            Product product = invoiceDetailView.cbProduct.SelectedItem as Product;

            foreach (var item in InventoryService.Get().lstRemain)
            {
                if (string.Compare(item.IdProduct, invoiceDetail.IdProduct, true) == 0)
                {
                    item.Quantity += quantity;
                    if (string.Compare(invoiceDetail.IdProduct, product.Id, true) == 0)
                    {
                        invoiceDetailView.txbTotalProduct.Text = item.Quantity.ToString();
                        Paramenter.TotalProduct = item.Quantity;
                    }
                }
            }
            invoiceDetailView.txbQuan.Text = "0";
        }

        private void InvocieDetailView_ChangedQuantityProduct(object sender, EventArgs e)
        {
            var invoiceDetail = sender as InvoiceDetailView;
            Product product = invoiceDetail.cbProduct.SelectedItem as Product;
            var quantity = Int32.Parse(invoiceDetail.txbQuan.Text);

            foreach (var item in InventoryService.Get().lstRemain)
            {
                if (string.Compare(item.IdProduct, product.Id, true) == 0)
                {
                    item.Quantity -= quantity;
                    invoiceDetail.txbTotalProduct.Text = item.Quantity.ToString();
                    Paramenter.TotalProduct = item.Quantity;
                }
            }
        }

        private void InvocieDetailView_SelecedProduct(object sender, EventArgs e)
        {
            var invoiceDetail = sender as InvoiceDetailView;
            Product product = invoiceDetail.cbProduct.SelectedItem as Product;
            foreach (var item in InventoryService.Get().lstRemain)
            {
                if (string.Compare(item.IdProduct, product.Id, true) == 0)
                {
                    invoiceDetail.txbTotalProduct.Text = item.Quantity.ToString();
                    Paramenter.TotalProduct = item.Quantity;
                }
            }
        }

        private void InvocieDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var invoicedetail = sender as InvoiceDetailView;
            Invoice invoice = invoicedetail.Invoice;
            lstInvoice.Add(invoice);
            InvoiceService.Add(invoice);
            InventoryService.AddInvoice(invoice);
            InventorySaleService.AddProductFromStock(invoice);
            FoodReceiptService.RepairFoodReceipt(invoice);
            MessageBox.Show("Added invoice successfully!");
        }

        public void LoadComboBox(InvoiceDetailView invoiceDetail)
        {
            foreach (var item in InventoryService.Get().lstRemain)
            {
                if (item.Quantity > 0)
                {
                    invoiceDetail.cbProduct.Items.Add(item.Product);
                }
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Invoice = btn.DataContext as Invoice;
                if (Invoice != null)
                {
                    lstView.SelectedItem = Invoice;
                    InvoiceDetailView invoiceDetailView = new InvoiceDetailView();
                    invoiceDetailView.stkAddProduct.Visibility = Visibility.Hidden;
                    invoiceDetailView.btnConfirm.Visibility = Visibility.Hidden;
                    invoiceDetailView.Invoice = Invoice;
                    invoiceDetailView.ShowDialog();
                }
            }

        }

        private void txbId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string id = txbId.Text;
                lstInvoice.Clear();
                foreach (var item in InvoiceService.Gets())
                {
                    if (string.Compare(item.Id, id, true) == 0)
                        lstInvoice.Add(item);
                    else if (string.IsNullOrEmpty(txbId.Text))
                        lstInvoice.Add(item);
                }
            }
        }
    }
}
