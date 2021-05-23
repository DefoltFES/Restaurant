using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }

        private void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
          var x =  sender as TextBox;
          x.Text = string.Empty;
          x.GotFocus -= TextBoxOnGotFocus;
        }

      

        private void PasswordVisible_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Eye.Fill = (SolidColorBrush) TryFindResource("PasswordVisible");
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordText.Text = PasswordBox.Password;
            PasswordText.Visibility = Visibility.Visible;
        }
        private void PasswordVisible_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Eye.Fill = (SolidColorBrush) TryFindResource("TextPlaceHolder");
            PasswordText.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
           
        }

        private void Password_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var x = sender as TextBox;
            x.Text = string.Empty;
            x.GotFocus -= TextBoxOnGotFocus;
            x.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Focus();
        }


        private void PhoneNumberTextChanged(object sender, TextChangedEventArgs e)
        {
         
        }

        private void Register_Navigate(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }

        private void PhoneNumber_Error(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(sender as TextBox))
            {
                ErrorPhone.Text = e.Error.ErrorContent.ToString();
                ErrorPhone.Visibility = Visibility.Visible;
                BorderPhone.BorderBrush = (SolidColorBrush) TryFindResource("ErrorButtonOutline");
            }
            else
            {
                ErrorPhone.Visibility = Visibility.Hidden;
                BorderPhone.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
            }





        }

        private void ButtonLogin_Error(object sender, ValidationErrorEventArgs e)
        {
            
        }
    }
}
