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

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for ucInventory.xaml
    /// </summary>
    public partial class ucInventory : UserControl
    {
        public InventoryService InventoryService { get; set; }
        public ucButtonCrud ucButtonCrud { get; set; }

        public ucImport ucImport { get; set; }
        public ucExport ucExport { get; set; }
        public ucRemain ucRemain { get; set; }

        public ucInventory()
        {
            InitializeComponent();
            ucButtonCrud = new ucButtonCrud();
            InventoryService = new InventoryService();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            stkButton.Children.Clear();
            stklstView.Children.Clear();
            ucImport = new ucImport(InventoryService.Get().lstImport);
            stklstView.Children.Add(ucImport);
            ucExport = new ucExport(InventoryService.Get().lstExport);
            stklstView.Children.Add(ucExport);
            ucRemain = new ucRemain(InventoryService.Get().lstRemain);
            stklstView.Children.Add(ucRemain);
        }
    }
}
