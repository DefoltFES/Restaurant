using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestaurantApp.service
{
    class CountRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), "^[0-9]+$"))
            {
                return new ValidationResult(false,
                    "Должно быть явно больше 0");
            }
            else
            {
                return new ValidationResult(true,null);
            }
            
        }
    }
}
