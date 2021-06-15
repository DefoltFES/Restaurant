using System;
using System.Collections.Generic;
using System.IO;
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
using Restaurant.ViewModel;
using Restaurant.Views;
using RestaurantApp.ViewModel;

namespace RestaurantApp.Views
{
    /// <summary>
    /// Interaction logic for ListAdminPage.xaml
    /// </summary>
    public partial class ListAdminPage : Page
    {
        private ListAdminPageViewModel Context { get; set; }
        public ListAdminPage(ListAdminPageViewModel model)
        {

            InitializeComponent();
            Context = model;
            DataContext = Context;

        }

        private void ListAdminPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Context = new ListAdminPageViewModel(App.dbContext.Users.Find(Context.Id));
            DataContext = Context;
            CheckList();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            if (!this.NavigationService.CanGoBack && !this.NavigationService.CanGoForward)
            {
                return;
            }

            var entry = this.NavigationService.RemoveBackEntry();
            while (entry != null)
            {
                entry = this.NavigationService.RemoveBackEntry();
            }
            (App.Current.MainWindow as MainWindow).MainFrame.NavigationService.Navigate(new PageFunction<string>() { RemoveFromJournal = true });
            (App.Current.MainWindow as MainWindow).MainFrame.Content=new LoginPage(new LoginViewModel());
        }

        private void AddClick_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateRestaurantPage(new Restaurant(),App.dbContext.Users.Find(Context.Id)));
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if(ListRestoraunts.SelectedItem is Restaurant restaurant)
            {
                DialogWindow dialog = new DialogWindow(new DialogWindowModel()
                {
                    Accept = "УДАЛИТЬ",
                    Description = "Удалить ресторан?"
                });
                if (dialog.ShowDialog() == true)
                {
                    App.dbContext.SearchTerms.RemoveRange(restaurant.SearchTerms);
                    restaurant.Kitchens.Clear();
                    DeleteImages(restaurant.Images);
                    Context.Restaurants.Remove(restaurant);
                    App.dbContext.Restaurants.Remove(restaurant);
                    App.dbContext.SaveChanges();
                    CheckList();
                }
            }
        }

        private void CheckList()
        {
            if (Context.Restaurants.Count != 0)
            {
                HasData.Visibility = Visibility.Visible;
                NoData.Visibility = Visibility.Collapsed;
            }
            else
            {
                HasData.Visibility = Visibility.Collapsed;
                NoData.Visibility = Visibility.Visible;
            }
        }
        private void DeleteImages(ICollection<Image> images)
        {
            foreach (var item in images)
            {
                File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + item.ImagePath);
            }
            App.dbContext.Images.RemoveRange(images);
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            if(ListRestoraunts.SelectedItem is Restaurant restaurant)
            {
                this.NavigationService.Navigate(new CreateRestaurantPage(restaurant,App.dbContext.Users.Find(Context.Id),true));
            }
        }
    }
}
