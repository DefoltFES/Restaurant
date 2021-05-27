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
using Restaurant.ViewModel;

namespace Restaurant.Views
{

    public partial class SuccessRegistrationPage : Page
    {
        private SuccessRegistrationViewModel Context { get; set; }
        public SuccessRegistrationPage(SuccessRegistrationViewModel model)
        {
            InitializeComponent();
            Context = model;
            DataContext = Context;
        }

        private void SavePassword(object sender, RoutedEventArgs e)
        {
           Context.SavePassword();
        }

        private void ResumeButton(object sender, RoutedEventArgs e)
        {
            Context.WriteLog();

        }
    }
}
