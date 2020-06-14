using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfChat.ViewModels;

namespace WpfChat.Converters
{
    class GonderenAdiToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String gonderenAdi = (String)value;
            if (gonderenAdi.Equals(ChatViewModel.kullaniciAdi))
            {
                return HorizontalAlignment.Right;
            }
            else
            {
                return HorizontalAlignment.Left;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
