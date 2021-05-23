using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.service
{
    class RegistrationFormRule:ValidationRule
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var phoneCheck=new PhoneNumberRule();
            var nameCheck=new FieldRule();
            var passwordCheck=new PasswordRule();
            return new ValidationResult(true, null);
        }
    }
}
