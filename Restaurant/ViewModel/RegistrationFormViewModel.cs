using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using Restaurant;
using Restaurant.Annotations;

namespace RestaurantApp.ViewModel
{
    public class RegistrationFormViewModel:INotifyPropertyChanged
    {
        private User user;
        public User User
        {
            get => user;
        }
        public string Title { get; set; }
        public string Role { get; set; }
        public string Phone
        {
            get { return user.Phone;}
            set { user.Phone = value; }
        }
        public string Name
        {
            get { return user.Name;}
            set { user.Name = value;}
        }
        public string Password { 
            get=>user.Password;
            set { user.Password = value;
                
                OnPropertyChanged(); }
        }

        public RegistrationFormViewModel(User user)
        {
            this.user = user;
        }

        public void CreateUser()
        {
            if (Role == "user")
            {
                user.Role = App.dbContext.Roles.Find(1);
            }
            if (Role == "admin")
            {
                user.Role = App.dbContext.Roles.Find(2);
            }

            App.dbContext.Users.Add(user);
            //App.dbContext.SaveChanges();
        }
      
        public void GeneratePassword()
        {
            
            Random rnd = new Random();
            int length = rnd.Next(8, 12);
            const string ValidChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@$!%*?&.";
            StringBuilder result = new StringBuilder();
            Random randomChar = new Random();
            while (!Regex.IsMatch(result.ToString(), @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&.]{8,}"))
            {
                result.Append(ValidChar[randomChar.Next(ValidChar.Length)]);
            }
            Password = result.ToString();
        }
        public bool CheckPhone()
        {
            if (App.dbContext.Users.Where(x=>x.Phone==Phone).FirstOrDefault() != null)
            {
                return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
