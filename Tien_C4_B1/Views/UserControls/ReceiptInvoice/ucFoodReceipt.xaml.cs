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

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucFoodReceipt.xaml
    /// </summary>
    public partial class ucFoodReceipt : UserControl
    {
        public FoodReceiptService FoodReceiptService { get; set; }

        public ObservableCollection<FoodReceipt> foodReceipts { get; set; }

        public ucFoodReceipt()
        {
            InitializeComponent();

            FoodReceiptService = new FoodReceiptService();
            foodReceipts = new ObservableCollection<FoodReceipt>(FoodReceiptService.Gets());

            this.DataContext = this;
        }
    }
}
