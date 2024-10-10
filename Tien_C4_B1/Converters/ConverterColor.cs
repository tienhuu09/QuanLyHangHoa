using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tien_C4_B1
{
    public class ConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Kiểm tra giá trị của dữ liệu binding và trả về chuỗi tùy thuộc vào điều kiện
            if (value is bool && (bool)value)
                return "Available";
            else
                return "Unavailable";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
