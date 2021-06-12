using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Restaurant.service
{
    class PathToImageConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri fileUri;
            fileUri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + value.ToString(), UriKind.Absolute);
            if (!File.Exists(fileUri.OriginalString))
            {
                return null;
            }
            var b1 = new BitmapImage();
            b1.BeginInit();
            b1.UriSource = fileUri;
            b1.CacheOption = BitmapCacheOption.OnLoad;
            b1.EndInit();
            return b1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
