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
    /// Interaction logic for ucButtonCrud.xaml
    /// </summary>
    public partial class ucButtonCrud : UserControl
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler selectionCbCategory;

        public ucButtonCrud()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            clickBtnAdd?.Invoke(this, EventArgs.Empty);
        }

        private void cbCategory_Loaded(object sender, RoutedEventArgs e)
        {
            cbCategory.Items.Clear();
            cbCategory.Items.Add("Food");
            cbCategory.Items.Add("Electronic");
            cbCategory.Items.Add("Porcelain");
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = cbCategory.SelectedItem as string;
            selectionCbCategory.Invoke(str, EventArgs.Empty);
        }
    }
}
