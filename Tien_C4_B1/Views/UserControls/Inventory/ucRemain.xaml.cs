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
    /// Interaction logic for ucRemain.xaml
    /// </summary>
    public partial class ucRemain : UserControl
    {
        public ObservableCollection<RemainingProduct> lstRemaining { get; set; }
        public RemainingProduct RemainingProduct { get; set; }

        public ucRemain()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ucRemain(List<RemainingProduct> lstItem)
        {
            InitializeComponent();
            lstRemaining = new ObservableCollection<RemainingProduct>(lstItem);
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
