using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Constants
    {
        // Số lượng nhân viên
        public static int QuantityUser { get; set; }

        // Add, Edit, Remove, ChangeStt
        public const string ChangeStt = "Status change successfully!!!";
        public const string AddSuccess = "Add successfully!!!";
        public const string RemoveSuccsess = "Remove Successfully";
        public const string EditSuccess = "Edited successfully!!!";
        public const string FeatureDev = "Feature under development";

        // Are you sure, Continue
        public const string AreYouSureStt = "Are you sure you want to change the status ?";
        public const string AYSAdd = "Are you sure added to list?";
        public const string AYSContinue = "Are you sure you want to continue?";

        // Ticket
        public const string PrintTicketSuc = "Print ticket successfully";
        public const string BuyTicketSuc = "Buy ticket successfully!!!";
        public const string BookingTicket = "Do you want to continue booking tickets ?";

        // Role
        public const string NotPermissionRole = "You do not have permission to change role (Admin)";
        public const string NotRoleEmpty = "There is no empty role, please (create) a new role";
        public const string UserHasARole = "User already has a role, please (cancel) to select a new Role";
        public const string RoleAlreadyHasUsers = "This role you are using";
        public const string GrantTheRoleSuc = "Grant the role successfully";
        public const string YouCanNotUse = "You cannot use this role";

        // Product
        public const string DateTimeInvalid = "Datetime invalid. Please try again!";
        public const string AddProductSucess = "Add product successfully!";
        public const string CannotPriceChange = "There is still this item in stock, connot be (price) chaged!";
    }

}
