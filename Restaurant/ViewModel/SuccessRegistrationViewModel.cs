using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Restaurant;
using RestaurantApp;

namespace RestaurantApp.ViewModel
{
    public class SuccessRegistrationViewModel
    {
        public string Phone { get; set; }
        public string Password { get; }
        public int Id { get; set; }
        public string Role { get; }
        public SuccessRegistrationViewModel(User user)
        {
            Phone = user.Phone;
            Password = user.Password;
            Role = user.Role.NameRole;
            Id = user.IdUser;
        }

        public void SavePassword()
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                return;
            }
            else
            {
                string filename = saveFileDialog.FileName;
                System.IO.File.WriteAllText(filename, Password);
            }
          
            
        }

        public void WriteLog()
        {
            var user = App.dbContext.Users.Where(x => x.Phone == Phone).FirstOrDefault();
            var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"logs";
            var fullFilePath = fullDirectoryPath + @"\history.txt";
            if (!Directory.Exists(fullDirectoryPath))
            {
                Directory.CreateDirectory(fullDirectoryPath);
            }
            if (!File.Exists(fullFilePath))
            {
                FileStream fileStream = new FileStream(fullFilePath, FileMode.Create);
                fileStream.Close();
            }
            using (StreamWriter writer = new StreamWriter(new FileStream(fullFilePath, FileMode.Append, FileAccess.Write)))
            {
                writer.WriteLine(user.IdUser + " " + " " + App.TodayDate);
                writer.Close();
            }
        }
    }
}
