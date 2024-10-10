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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucReceipt.xaml
    /// </summary>
    public partial class ucReceipt : UserControl
    {
        public event EventHandler ReceiptChanged;

        public FoodService FoodService { get; set; }
        public ucButtonCrud ucButtonCrud { get; set; }
        public ReceiptService ReceiptService { get; set; }
        public PorcelainService PorcelainService { get; set; }
        public InventoryService InventoryService { get; set; }
        public ElectronicService ElectronicService { get; set; }
        public FoodReceiptService FoodReceiptService { get; set; }
        public Receipt Receipt { get; set; }
        public string UserName { get; set; }

        public ObservableCollection<Receipt> lstReceipt { get; set; }

        public ucReceipt()
        {
            InitializeComponent();

            ucButtonCrud = new ucButtonCrud();
            ReceiptService = new ReceiptService();
            InventoryService = new InventoryService();
            FoodReceiptService = new FoodReceiptService();
            lstReceipt = new ObservableCollection<Receipt>(ReceiptService.Gets());

            FoodService = new FoodService();
            PorcelainService = new PorcelainService();
            ElectronicService = new ElectronicService();

            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;

            ucButtonCrud.cbCategory.Visibility = Visibility.Hidden;
            stkButton.Children.Add(ucButtonCrud);

            this.DataContext = this;
        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            Receipt receipt = new Receipt();
            receipt.Id = ReceiptService.GetId();
            receipt.UserName = UserName;
            ReceiptDetailView receiptDetailView = new ReceiptDetailView();
            receiptDetailView.Receipt = receipt;
            receiptDetailView.clickBtnAdd += ReceiptDetailView_clickBtnAdd;
            LoadComboBox(receiptDetailView);
            receiptDetailView.ShowDialog();
        }

        private void ReceiptDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            var receiptDetail = sender as ReceiptDetailView;
            Receipt receipt = receiptDetail.Receipt;
            var foodReceipt = receiptDetail.lstfoodReceipt;
            lstReceipt.Add(receipt);
            ReceiptService.Add(receipt);
            if (foodReceipt != null)
                FoodReceiptService.AddListFoodReceipt(foodReceipt);
            InventoryService.AddReceipt(receipt);
            MessageBox.Show("Added receipt successfully!");
            ReceiptChanged.Invoke(this, EventArgs.Empty);
        }

        public void LoadComboBox(ReceiptDetailView detailView)
        {
            foreach (var item in FoodService.Gets())
                detailView.cbProduct.Items.Add(item);

            foreach (var item in PorcelainService.Gets())
                detailView.cbProduct.Items.Add(item);

            foreach (var item in ElectronicService.Gets())
                detailView.cbProduct.Items.Add(item);
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn != null)
            {
                Receipt = btn.DataContext as Receipt;
                if (Receipt != null)
                {
                    lstView.SelectedItem = Receipt;
                    ReceiptDetailView receiptDetailView = new ReceiptDetailView();
                    receiptDetailView.stkAddProduct.Visibility = Visibility.Hidden;
                    receiptDetailView.btnConfirm.Visibility = Visibility.Hidden;
                    receiptDetailView.Receipt = Receipt;
                    receiptDetailView.ShowDialog();
                }
            }

        }

        private void txbId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string id = txbId.Text;
                lstReceipt.Clear();
                foreach (var item in ReceiptService.Gets())
                {
                    if (string.Compare(item.Id, id, true) == 0)
                        lstReceipt.Add(item);
                    else if (string.IsNullOrEmpty(txbId.Text))
                        lstReceipt.Add(item);
                }
            }

        }
    }
}
