using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Restaurant;
using RestaurantApp.ViewModel;

namespace RestaurantApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class ChooseRegisterPage : Page
    {
        public ChooseRegisterPage()
        {
            InitializeComponent();
        }

   

        private void RadioButtonAdmin_OnClick(object sender, RoutedEventArgs e)
        {
            if (RoleB.IsChecked == true)
            {
                RoleB.IsChecked = false;

            }
            else
            {

                RoleB.IsChecked = true;

            }
           
        }

        private void RadioButtonUser_OnClick(object sender, RoutedEventArgs e)
        {
            if (RoleA.IsChecked == true)
            {
                RoleA.IsChecked = false;

            }
            else
            {
                RoleA.IsChecked = true;
            }
        }

        private void RoleB_OnChecked(object sender, RoutedEventArgs e)
        {
            RoleA.IsChecked = false;
            RadioButtonAdmin.Data = (PathGeometry)TryFindResource("RadioButtonChecked");
            RadioButtonAdmin.Fill = (SolidColorBrush)TryFindResource("SecondColor");
            this.NavigationService.Navigate(new RegisterFormPage(new RegistrationFormViewModel(new User())
            {
                Title = "Это необходимо для управления ресторанами",
                Role = "admin"
            }));


        }

        private void RoleA_OnChecked(object sender, RoutedEventArgs e)
        {
            RoleB.IsChecked = false;
            RadioButtonUser.Data = (PathGeometry)TryFindResource("RadioButtonChecked");
            RadioButtonUser.Fill = (SolidColorBrush)TryFindResource("SecondColor");
            this.NavigationService.Navigate(new RegisterFormPage(new RegistrationFormViewModel(new User())
            {
                Title = "Это необходимо для бронирования столиков",
                Role = "user"
            }));

        }

        private void RoleB_OnUnchecked(object sender, RoutedEventArgs e)
        {
            RadioButtonAdmin.Data = (PathGeometry)TryFindResource("RadioButtonNotChecked");
            RadioButtonAdmin.Fill = (SolidColorBrush)TryFindResource("ContentText");
        }

        private void RoleA_OnUnchecked(object sender, RoutedEventArgs e)
        {

            RadioButtonUser.Data = (PathGeometry)TryFindResource("RadioButtonNotChecked");
            RadioButtonUser.Fill = (SolidColorBrush)TryFindResource("ContentText");
        }
    }
}
