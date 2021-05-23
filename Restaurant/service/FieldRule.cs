using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.service
{
    class FieldRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string field = (string) value;
            if (field == "")
            {
                return new ValidationResult(false,
                    "Поле не может быть пустым");
            }
            return new ValidationResult(true,
                null);

        }
    }
}
