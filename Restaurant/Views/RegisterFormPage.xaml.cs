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
using Restaurant.Views;
using RestaurantApp.ViewModel;

namespace RestaurantApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterFormPage.xaml
    /// </summary>
    public partial class RegisterFormPage : Page
    {
        private RegistrationFormViewModel Context { get; set; }
        public RegisterFormPage(RegistrationFormViewModel model)
        {
            InitializeComponent();
            Context = model;
            DataContext = Context;
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

            CheckBoxPassword.Data = (PathGeometry)TryFindResource("CheckBoxChecked");
            PasswordBox.Focus();
            Context.GeneratePassword();
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


        private void CreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            PasswordBox.Focus();
            Phone.Focus();
            Name.Focus();
            if (CheckPrivacy.IsChecked == false)
            {
                    Privacy.Fill = (SolidColorBrush)TryFindResource("ErrorButtonText");
                    return;
            }
            if (!Validation.GetHasError(PasswordBox) & !Validation.GetHasError(Phone) & !Validation.GetHasError(Name))
            {
                if (Context.CheckPhone())
                {
                    Context.CreateUser();
                    (App.Current.MainWindow as MainWindow).MainFrame.Content=new SuccessRegistrationPage(new SuccessRegistrationViewModel(Context.User));

                }
                else
                {
                    ErrorPhone.Visibility = Visibility.Visible;
                    BorderPhone.BorderBrush = (SolidColorBrush)TryFindResource("ButtonOutline");
                    ErrorPhone.Text = "Аккаунт с таким номером телефона существует";
                }

            }
        }
    }
}
