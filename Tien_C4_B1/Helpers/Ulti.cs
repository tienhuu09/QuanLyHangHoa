using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tien_C4_B1
{
    public class Ulti
    {
        public static string spec = "#,##0đ"; /* specifier */
        public static string spec2 = "#,##0"; /* specifier */
        public static string date = "dd/MM/yyyy HH:mm:ss";
        public static string date2 = "dd/MM/yyyy";
        public static string product = "# product";

        #region DownLine
        public static void DownLine(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine();
        }
        #endregion

        #region Vertical
        public static void VerticalBrick()
        {
            Console.Write("|");
        }
        #endregion

        #region HandleStr
        public static void HandleStr(int maxStr, string str)
        {
            int count = (maxStr - str.Length) / 2;
            Ulti.Space(count);
            Console.Write(str);
            if (str.Length % 2 == 1)
                count++;
            Ulti.Space(count);
        }
        #endregion

        #region OutputStr
        public static void OutputStr(int maxStr, string str)
        {
            Console.OutputEncoding = Encoding.UTF8;
            HandleStr(maxStr, str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|");
        }
        #endregion

        #region Option
        public static int Option(int idx)
        {
            int opt;
            bool check;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Option: ");
                check = Int32.TryParse(Console.ReadLine(), out opt);
                Console.ForegroundColor = ConsoleColor.White;
                if (opt == -1)
                    return -1;
            } while (!check || opt <= 0 || opt > idx);
            return opt;
        }
        #endregion

        #region SpaceLine
        public static void SpaceLine(int n)
        {
            for (int i = 0; i < n - 1; i++)
                Console.Write(" ");
            Console.WriteLine("|");
        }
        #endregion

        #region Space
        public static void Space(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" ");
        }
        #endregion

        #region Dash
        public static void Dash(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write("-");
            Console.WriteLine();
        }
        #endregion

        #region NotifyEnd
        public static bool NotifyEnd()
        {
            bool check;
            int key;
            do
            {
                Console.Write("Press key (1) to continue, or (0) to exit: ");
                check = Int32.TryParse(Console.ReadLine(), out key);
            } while (!check || key != 0 && key != 1);

            if (key == 1)
                return true;
            return false;
        }
        #endregion

        #region NotifyAgain
        public static bool NotifyAgain()
        {
            bool check;
            int key;
            do
            {
                Console.Write("Press key (1) to try again, or (0) to exit: ");
                check = Int32.TryParse(Console.ReadLine(), out key);
            } while (!check || key != 0 && key != 1);

            if (key == 1)
                return true;
            return false;
        }
        #endregion

        #region NotifyAgain2
        public static int NotifyAgain2()
        {
            bool check;
            int key;
            do
            {
                Console.Write("Press key (2) cancel order or (1) to continue, and (0) to pay: ");
                check = Int32.TryParse(Console.ReadLine(), out key);
            } while (!check || key != 0 && key != 1 && key != 2);
            return key;
        }
        #endregion

        #region SetArr
        public static void setArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = -1;
        }
        #endregion

        #region isExist
        public static bool isExist(int[] arr, int idx)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == idx)
                    return true;
            return false;
        }
        #endregion

        public static string getStr(string note)
        {
            string str;
            do
            {
                Console.Write("{0}: ", note);
                str = Console.ReadLine();
            } while (str.Length == 0);
            return str;
        }

        public static string getStrInputNumber(string note)
        {
            string cccd;
            bool check;
            int num;
            do
            {
                Console.Write("{0}: ", note);
                check = Int32.TryParse(Console.ReadLine(), out num);
                cccd = num.ToString();
            } while (!check || cccd.Length == 0);
            return cccd;
        }

        public static void InputDate(out int day, out int month, out int year)
        {
            bool check;
            do
            {
                Console.Write("Day: ");
                check = Int32.TryParse(Console.ReadLine(), out day);
            } while (!check || day <= 0);

            do
            {
                Console.Write("Month: ");
                check = Int32.TryParse(Console.ReadLine(), out month);
            } while (!check || month <= 0);

            do
            {
                Console.Write("Year: ");
                check = Int32.TryParse(Console.ReadLine(), out year);
            } while (!check || year <= 2000);
        }

        public static bool CheckTimeValid(DateTime date1, DateTime date2)
        {
            int result = DateTime.Compare(date1, date2);
            if (result > 0)
                return true;
            return false;
        }

        public static void MessageBoxComplete()
        {
            MessageBox.Show("Please complete all information");
        }

        public static MessageBoxResult MessageBoxYesNo(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result;
        }

        public static void MessageBoxShowInfo(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void MessageBoxShow(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Notification", MessageBoxButton.OK);
        }
    }
}
