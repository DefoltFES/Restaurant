using RestaurantApp.ViewModel;
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

namespace RestaurantApp.Views
{
    /// <summary>
    /// Interaction logic for ListUserPage.xaml
    /// </summary>
    public partial class ListUserPage : Page
    {
        private ListUserViewModel Model { get; set; }
        public ListUserPage(User user)
        {
            InitializeComponent();
            Model = new ListUserViewModel(user);
            DataContext = Model;
        }

        private void ListUserPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void FilterSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Model = new ListUserViewModel(Model.User);
            DataContext = Model;
        }
    }
}
