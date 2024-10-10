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
using System.Windows.Shapes;

namespace Tien_C4_B1
{
    /// <summary>
    /// Interaction logic for RoleDetailView.xaml
    /// </summary>
    public partial class RoleDetailView : Window
    {
        public event EventHandler clickBtnAdd;
        public event EventHandler clickBtnEdit;

        public Role Role { get; set; }

        public RoleDetailView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNameRole.Text) || cbRole.SelectedIndex == -1)
                MessageBox.Show("Please complete all information!");
            else
            {
                this.Close();
                clickBtnAdd.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnConfirmEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            cbRole.Items.Add("Stocker");
            cbRole.Items.Add("Cashier");
        }
    }
}
