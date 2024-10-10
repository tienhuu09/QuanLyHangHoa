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
    /// Interaction logic for ucImport.xaml
    /// </summary>
    public partial class ucImport : UserControl
    {
        public ObservableCollection<ImportInventory> lstImport { get; set; }
        public ImportInventory ImportInventory { get; set; }

        public ucImport()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public ucImport(List<ImportInventory> listImport)
        {
            InitializeComponent();

            lstImport = new ObservableCollection<ImportInventory>(listImport);
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
