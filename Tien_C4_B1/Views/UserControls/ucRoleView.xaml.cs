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
    /// Interaction logic for ucRoleView.xaml
    /// </summary>
    public partial class ucRoleView : UserControl
    {
        public ucButtonCrud ucButtonCrud { get; set; }
        public RoleService RoleService { get; set; }
        public ObservableCollection<Role> Roles { get; set; }

        public Role Role { get; set; }
        public ucRoleView()
        {
            InitializeComponent();
            ucButtonCrud = new ucButtonCrud(); 
            RoleService = new RoleService();
            Roles = new ObservableCollection<Role>(RoleService.Gets());

            ucButtonCrud.clickBtnAdd += UcButtonCrud_clickBtnAdd;

            stkButton.Children.Clear();
            ucButtonCrud.cbCategory.Visibility = Visibility.Hidden;
            stkButton.Children.Add(ucButtonCrud);
            this.DataContext = this;

        }

        private void UcButtonCrud_clickBtnAdd(object sender, EventArgs e)
        {
            RoleDetailView roleDetailView = new RoleDetailView();
            Role = new Role();
            roleDetailView.Role = Role;
            roleDetailView.txbNameRole.IsReadOnly = false;
            roleDetailView.txbId.IsReadOnly = true;
            roleDetailView.btnConfirmEdit.Visibility = Visibility.Hidden;
            Role.UserName = "empty";
            int num = RoleService.Gets().Count;
            do
            {
                Role.IdRole = "R" + num.ToString();
                num += 1;
            } while (RoleService.CheckIdRole(Role.IdRole));
           
            roleDetailView.clickBtnAdd += RoleDetailView_clickBtnAdd;
            roleDetailView.ShowDialog();
        }   

        private void RoleDetailView_clickBtnAdd(object sender, EventArgs e)
        {
            RoleDetailView item = sender as RoleDetailView;
            string str = item.cbRole.SelectedItem as string;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (string.Compare(str, "Stocker", true) == 0)
                    Role.RoleLv = 2;
                else
                    Role.RoleLv = 3;
                Role.RoleName = item.Role.RoleName;
                RoleService.Add(Role);
                Roles.Add(Role);
                MessageBox.Show("Added role successfully!");
            }
            else
                return;
           
        }
    }
}
