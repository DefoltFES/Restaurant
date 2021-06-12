using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Restaurant.Annotations;

namespace RestaurantApp.ViewModel
{
    class CreateRestorauntViewModel: INotifyPropertyChanged
    {
        private User user;
        private Restaurant restaurant;
        private string image;
        public string Name
        {
            get => restaurant.Name;
            set
            {
                restaurant.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => restaurant.Description;
            set
            {
                restaurant.Description = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => restaurant.Phone;
            set
            {
                restaurant.Phone = value;
                OnPropertyChanged();
            }
        }

        public string AverageCheck
        {
            get => restaurant.AverageCheck;
            set
            {
                restaurant.AverageCheck = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => restaurant.Address;
            set { restaurant.Address = value; }
        }

        public int? NumberCount
        {
            get => restaurant.NumberCount;
            set
            {
                restaurant.NumberCount = value;
                OnPropertyChanged();
            }
        }

        public string TimeOpen
        {
            get => restaurant.TimeOpen.ToString();
            set
            {

                restaurant.TimeOpen = TimeSpan.ParseExact(value, @"h\:m",
                    CultureInfo.InvariantCulture);
                OnPropertyChanged();
            }
        }

        public string TimeClose
        {
            get => restaurant.TimeClose.ToString();
            set
            {
                restaurant.TimeClose = TimeSpan.ParseExact(value, @"h\:m", CultureInfo.InvariantCulture);
                OnPropertyChanged();
            }
        }

        public string SearchTerm { get; set; }

        public string Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public string Kitchen { get; set; }

        public bool? isTerrassa
        {
            get=>restaurant.isTerrassa;
            set
            {
                restaurant.isTerrassa = value;
            }
        }

        public string Title { get; set; }
        public bool isEdit { get; set; }

        public CreateRestorauntViewModel(Restaurant restik,User user,bool edit=false)
        {
            restaurant = restik;
            this.user = user;
            isEdit = edit;
            if (isEdit == false)
            {
                Title = "Добавление";
            }
            else
            {
                Title = "Управление";
                var item = restik.Kitchens.ToList();
                for (int i = 0; i < item.Count; i++)
                {
                    if (i == item.Count - 1)
                    {
                        Kitchen += $"{item[i].Name}";
                    }
                    else
                    {
                        Kitchen += $"{item[i].Name}, ";
                    }
                }
                foreach (var image in restaurant.Images)
                {
                    Image += $"{Path.GetFileName(System.AppDomain.CurrentDomain.BaseDirectory + image.ImagePath)}; ";
                }
            }
        }

        public void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image File (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png",
                CheckPathExists = true,
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
              var names= openFileDialog.FileNames;
              restaurant.Images.Clear();
              foreach (var item in names)
              {
                  Image += $"{Path.GetFileName(item)}; ";
                  restaurant.Images.Add(new Image(){ImagePath = item});
              }
            }
        }

        public void SaveChanges()
        {
            SaveKitchen();
            SaveSearchTerms();
            foreach (var item in restaurant.Images)
            {
                item.ImagePath = CopyAndSaveImages(item.ImagePath);
            }

            App.dbContext.Restaurants.Add(restaurant);
            user.Restaurants.Add(restaurant);
            App.dbContext.SaveChanges();
        }

        private void DeleteImages(ICollection<Image> images)
        {
            foreach (var item in images)
            {
                File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + item.ImagePath);
            }
            App.dbContext.Images.RemoveRange(images);
        }

        private void SaveKitchen()
        {
            var array = Kitchen.Split(',',' ');
            foreach (var kitchen in array)
            {
                if (kitchen != "")
                {
                    restaurant.Kitchens.Add(new Kitchen() {Name = kitchen});
                }
            }
        }

        private void SaveSearchTerms()
        {
            if (SearchTerm != null & SearchTerm != "")
            {
                var array = SearchTerm.Split(',', ' ');
                foreach (var kitchen in array)
                {
                    if (kitchen != "")
                    {
                        restaurant.SearchTerms.Add(new SearchTerm() {Name = kitchen});
                    }
                }
            }
            else
            {
                return;
            }
        }

        public bool CheckCountImage()
        {
            if (restaurant.Images.Count < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string CopyAndSaveImages(string path)
        {
                var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"images\restoraunt";
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath);
                }
                var nameImage = Path.GetFileName(path);
                var newImageSource = Path.Combine(fullDirectoryPath, nameImage);
                File.Copy(path, newImageSource, true);
                return Path.Combine(@"images\restoraunt\", nameImage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
