using System;
using System.Collections.Generic;
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
using RestaurantApp.ViewModel;

namespace RestaurantApp.Views
{
    /// <summary>
    /// Interaction logic for CreateRestaurantPage.xaml
    /// </summary>
    public partial class CreateRestaurantPage : Page
    {
        private CreateRestorauntViewModel Context { get; set;}
        public CreateRestaurantPage(Restaurant model,User user,bool isEdit=false)
        {
            InitializeComponent();
            Context=new CreateRestorauntViewModel(model,user,isEdit);
            DataContext = Context;
          
        }
        private void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            if (!Context.isEdit)
            {
                var x = sender as TextBox;
                x.Text = string.Empty;
                x.GotFocus -= TextBoxOnGotFocus;
            }
        }

      

        private void YesClick(object sender, RoutedEventArgs e)
        {
            if (Yes.IsChecked == true)
            {
                Yes.IsChecked = false;
            }
            else
            {
                Yes.IsChecked = true;
                Terrassa.Background = (SolidColorBrush) TryFindResource("DataIsCorrect");
            }
        }
        
        private void NoClick(object sender, RoutedEventArgs e)
        {
            if (No.IsChecked == true)
            {
                No.IsChecked = false;
            }
            else
            {
                No.IsChecked = true;
                Terrassa.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void Yes_OnChecked(object sender, RoutedEventArgs e)
        {
            No.IsChecked = false;
            Context.isTerrassa = true;
            YesPath.Data = (PathGeometry) TryFindResource("RadioButtonChecked");
            YesPath.Fill = (SolidColorBrush)TryFindResource("SecondColor");
        }
        private void Yes_OnUnchecked(object sender, RoutedEventArgs e)
        {
            YesPath.Data = (PathGeometry)TryFindResource("RadioButtonNotChecked");
            YesPath.Fill = (SolidColorBrush)TryFindResource("TextPlaceHolder");
        }
        private void No_OnChecked(object sender, RoutedEventArgs e)
        {
            Yes.IsChecked = false;
            Context.isTerrassa = false;
            NoPath.Data = (PathGeometry)TryFindResource("RadioButtonChecked");
            NoPath.Fill = (SolidColorBrush)TryFindResource("SecondColor");
        }
        private void No_OnUnchecked(object sender, RoutedEventArgs e)
        {
            NoPath.Data = (PathGeometry)TryFindResource("RadioButtonNotChecked");
            NoPath.Fill = (SolidColorBrush)TryFindResource("TextPlaceHolder");
        }

        private void Name_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(Name))
            {
                NameCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorName.Text = e.Error.ErrorContent.ToString();
                ErrorName.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorName.Visibility = Visibility.Hidden;
                NameCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
              
            }
        }

        private void Description_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(Description))
            {
                DescriptionCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorDescription.Text = e.Error.ErrorContent.ToString();
                ErrorDescription.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorDescription.Visibility = Visibility.Hidden;
                DescriptionCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void Address_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(Address))
            {
                AddressCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorAddress.Text = e.Error.ErrorContent.ToString();
                ErrorAddress.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorAddress.Visibility = Visibility.Hidden;
                AddressCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void NumPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch((sender as TextBox).Text);
        }

        private void NumPlace_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(NumPlace))
            {
                NumPlaceCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorNum.Text = e.Error.ErrorContent.ToString();
                ErrorNum.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorNum.Visibility = Visibility.Hidden;
                NumPlaceCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void Phone_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(Phone))
            {
                PhoneCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorPhone.Text = "Пример: +79992223344";
                ErrorPhone.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorPhone.Visibility = Visibility.Hidden;
                PhoneCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void SearchTerm_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text != "" && Context.SearchTerm!=null)
            {
                CircleSearch.Background = (SolidColorBrush) TryFindResource("DataIsCorrect");
            }
            else
            {
                CircleSearch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4DD60"));
            }
        }

        private void Kitchen_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(Kitchen))
            {
                CircleKitchen.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorKitchen.Text = e.Error.ErrorContent.ToString();
                ErrorKitchen.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorKitchen.Visibility = Visibility.Hidden;
                CircleKitchen.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void ImageClick(object sender, RoutedEventArgs e)
        {
            Context.AddImage();
            if (!Context.CheckCountImage())
            {
                ImageCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorImage.Visibility = Visibility.Visible;
            }
            else
            {
                ImageCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
            }
        }

        private void SaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            Name.Focus();
            Address.Focus();
            Phone.Focus();
            Description.Focus();
            NumPlace.Focus();
            Kitchen.Focus();
            if (!Context.CheckCountImage())
            {
                ImageCircle.Background = (SolidColorBrush)TryFindResource("ErrorButtonOutline");
                ErrorImage.Visibility = Visibility.Visible;
            }

            if (!Validation.GetHasError(Name) & !Validation.GetHasError(Address) &
                !Validation.GetHasError(Description) & !Validation.GetHasError(NumPlace) &
                !Validation.GetHasError(Kitchen) & Context.CheckCountImage() &!Validation.GetHasError(Phone))
            {
                Context.SaveChanges();
                this.NavigationService.GoBack();
            }
            
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void CreateRestaurantPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Context.isEdit)
            {
                ImageCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                CircleKitchen.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                PhoneCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                NumPlaceCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                AddressCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                DescriptionCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                NameCircle.Background = (SolidColorBrush)TryFindResource("DataIsCorrect");
                Terrassa.Background = (SolidColorBrush) TryFindResource("DataIsCorrect");
                if (Context.isTerrassa == true)
                {
                    Yes.IsChecked = true;
                }
                if (Context.isTerrassa == false)
                {
                    No.IsChecked = true;
                }
            }
        }
    }
}
