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
    class AddressRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string address = value.ToString();
            if (!Regex.IsMatch(address, @"^(ул+)(.+)\s+(\S+?)(-(\d+))?$"))
            {
                return  new ValidationResult(false,
                    "Не подходит по формату. Пример: ул. Л, 5");
            }
            else
            {
                return new ValidationResult(true,
                    null);
            }  
        }
    }
}
