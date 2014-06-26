using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Chatty.Helpers
{
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(241, 196, 15));

            if (value == null)
                return brush;

            bool isOnline = false;
            bool.TryParse(value.ToString(), out isOnline);

            if (!isOnline)
                return App.Current.Resources["Alizarin"] as SolidColorBrush as Brush;
            else
                return App.Current.Resources["Emerald"] as SolidColorBrush as Brush;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
