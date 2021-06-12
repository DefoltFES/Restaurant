using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RestaurantApp.service
{
    class OneRubleConvector:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int averageCheck = System.Convert.ToInt32(value);

            Color color = default;
            if (averageCheck == 1 || averageCheck == 2 || averageCheck == 3)
            {
                 color = (Color)ColorConverter.ConvertFromString("#4BB9F8");
            }
            else
            {
                color = (Color)ColorConverter.ConvertFromString("#4BB9F8");
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
