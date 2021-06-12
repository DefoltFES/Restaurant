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
using System.Windows.Shapes;
using Restaurant.ViewModel;

namespace Restaurant.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(DialogWindowModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
