using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Annotations;

namespace RestaurantApp.ViewModel
{
    public class ListAdminPageViewModel:INotifyPropertyChanged
    {
        public string Name { get; set;}
        public int Id { get; set; }
        private ObservableCollection<Restaurant> restaurants;
        public ObservableCollection<Restaurant> Restaurants { 
            get=>restaurants;
            set
            {
                restaurants = value; 
                OnPropertyChanged();
            }
        }

        public ListAdminPageViewModel(User user)
        {
            Name = user.Name;
            Id = user.IdUser;
            Restaurants = new ObservableCollection<Restaurant>(user.Restaurants);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
