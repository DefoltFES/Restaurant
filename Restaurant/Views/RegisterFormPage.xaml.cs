using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Restaurant.ViewModel;

namespace Restaurant.Views
{
    /// <summary>
    /// Interaction logic for RegisterFormPage.xaml
    /// </summary>
    public partial class RegisterFormPage : Page
    {
        public RegisterFormPage(RegistrationFormViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://holdbetter.dev");
        }

        private void GeneratePaswword_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckGenerationPassword.IsChecked == true)
            {
                CheckGenerationPassword.IsChecked = false;
            }
            else
            {
                CheckGenerationPassword.IsChecked = true;
            }
        }


        private void CheckGenerationPassword_OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBoxPassword.Data = (PathGeometry) TryFindResource("CheckBoxChecked");
            Random rnd = new Random();
            int length = rnd.Next(8, 12);
            const string ValidChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@$!%*?&.";
            StringBuilder result = new StringBuilder();
            Random randomChar = new Random();
            while (0 < length--)
            {
                result.Append(ValidChar[randomChar.Next(ValidChar.Length)]);
            }

            PasswordBox.Text = result.ToString();
        }

        private void CheckGenerationPassword_OnUnchecked(object sender, RoutedEventArgs e)
        {
           
            CheckBoxPassword.Data = (PathGeometry)TryFindResource("CheckBoxUnchecked");
         
        }

        private void CheckPrivacy_OnChecked(object sender, RoutedEventArgs e)
        {
            Privacy.Data = (PathGeometry)TryFindResource("CheckBoxChecked");
           
        }

        private void CheckPrivacy_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Privacy.Data = (PathGeometry)TryFindResource("CheckBoxUnchecked");
            
        }
        private void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            var x = sender as TextBox;
            x.Text = string.Empty;
            x.GotFocus -= TextBoxOnGotFocus;
        }
        private void Privacy_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckPrivacy.IsChecked == true)
            {
                CheckPrivacy.IsChecked = false;
            }
            else
            {
                CheckPrivacy.IsChecked = true;
            }
        }

        

        private void Password_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&.]{1,}") && input != "")
            {
                if (ErrorPassword is null)
                {
                    return;
                }
                ErrorPassword.Visibility = Visibility.Visible;
                BorderPassword.BorderBrush = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
            }
            else
            {
                BorderPassword.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
                ErrorPassword.Visibility = Visibility.Hidden;

            }
        }


        private void Phone_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(sender as TextBox))
            {
                ErrorPhone.Text = e.Error.ErrorContent.ToString();
                ErrorPhone.Visibility = Visibility.Visible;
                BorderPhone.BorderBrush = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
            }
            else
            {
                ErrorPhone.Visibility = Visibility.Hidden;
                BorderPhone.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
            }
        }

        private void Name_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(sender as TextBox))
            {
                ErrorName.Text = e.Error.ErrorContent.ToString();
                ErrorName.Visibility = Visibility.Visible;
                BorderName.BorderBrush = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
            }
            else
            {
                ErrorName.Visibility = Visibility.Hidden;
                BorderName.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
            }
        }

        private void Password_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(sender as TextBox))
            {
                ErrorPassword.Text = e.Error.ErrorContent.ToString();
                ErrorPassword.Visibility = Visibility.Visible;
                BorderPassword.BorderBrush = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
            }
            else
            {
                ErrorPassword.Visibility = Visibility.Hidden;
                BorderPassword.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
            }
        }

        private void ConfirmRegistration_OnError(object sender, ValidationErrorEventArgs e)
        {
           
        }
    }
}
