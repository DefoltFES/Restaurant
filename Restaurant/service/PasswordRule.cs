using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.service
{
    class PasswordRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = (string) value;
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&.]{1,}"))
            {
                return new ValidationResult(false,
                    "Пароль должен содержать [A-Z], [0-9], [a-z] и спец. символ");
            }
            else
            {
                return new ValidationResult(true,
                    null);
            }
        }
    }
}
