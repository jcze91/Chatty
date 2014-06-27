using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chatty.Helpers
{
    public class BinaryImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is string)
            {
                string base64String = value.ToString();
                byte[] binaryData = System.Convert.FromBase64String(base64String);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();

                return bi;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
