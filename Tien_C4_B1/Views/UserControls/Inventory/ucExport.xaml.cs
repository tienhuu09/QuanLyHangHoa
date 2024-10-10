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
    /// Interaction logic for ucExport.xaml
    /// </summary>
    public partial class ucExport : UserControl
    {
        public ObservableCollection<ExportInventory> lstExport { get; set; }
        public ExportInventory ExportInventory { get; set; }

        public ucExport()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ucExport(List<ExportInventory> lstItem)
        {
            InitializeComponent();
            lstExport = new ObservableCollection<ExportInventory>(lstItem);
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
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
