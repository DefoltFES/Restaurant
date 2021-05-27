using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Restaurant;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RestaurantContext dbContext=new RestaurantContext();

        public static DateTime TodayDate { get; set; }
        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(dbContext.Database.Connection.ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException exception)
                {
                        MessageBox.Show("Нет подключения к серверу");
                        Environment.Exit(exception.ErrorCode);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

}
