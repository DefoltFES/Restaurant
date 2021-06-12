using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
using Restaurant.Views;
using RestaurantApp.ViewModel;
using RestaurantApp.Views;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LoginPage(new LoginViewModel());
            App.TodayDate = GetDate();
        }

        private DateTime GetDate()
        {
            try
            {
                using (var client = new TcpClient("time.nist.gov", 13))
                {

                    using (var streamReader = new StreamReader(client.GetStream()))
                    {
                        var response = streamReader.ReadToEnd();
                        var utcDateTimeString = response.Substring(7, 17);
                        return DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal);
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
           
            return DateTime.Now;

            
            
        }
    }
}
