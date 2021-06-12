using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    public class LoginViewModel
    {
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get; set; }
        public string Role { get; set; }

        public bool isExist()
        {
            var user = App.dbContext.Users.Where(x => x.Phone == PhoneNumber).FirstOrDefault();
            if (user !=null)
            {
                if (user.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public void WriteLog()
        {
            var user= App.dbContext.Users.Where(x => x.Phone == PhoneNumber).FirstOrDefault();
            var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"logs";
            var fullFilePath = fullDirectoryPath + @"\history.txt";
            if (!Directory.Exists(fullDirectoryPath))
            {
                Directory.CreateDirectory(fullDirectoryPath);
            }
            if (!File.Exists(fullFilePath))
            {
                FileStream fileStream=new FileStream(fullFilePath,FileMode.Create);
                fileStream.Close();
            }
            using (StreamWriter writer=new StreamWriter(new FileStream(fullFilePath,FileMode.Append,FileAccess.Write)))
            {
                writer.WriteLine(user.IdUser+ " "+" "+App.TodayDate); 
                writer.Close();
            }
        }

        public bool AutoLogin()
        {
            var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"logs";
            var fullFilePath = fullDirectoryPath + @"\history.txt";
            if (File.Exists(fullFilePath))
            {
                using (StreamReader streamReader = new StreamReader(new FileStream(fullFilePath, FileMode.Open)))
                {
                    var lastLogin = streamReader.ReadToEnd().Split(' ');
                    var user = App.dbContext.Users.Find(Convert.ToInt32(lastLogin[0]));
                    this.Role = user.Role.NameRole;
                    this.Id = user.IdUser;
                    if (user.Role.IdRole == 1)
                    {
                        DateTime dateLastLogin = DateTime.Parse(lastLogin[2]);
                        var razniza = (App.TodayDate - dateLastLogin).TotalDays;
                        if (razniza <= 7)
                        {
                            Password = user.Password;
                            PhoneNumber = user.Phone;
                            return true;
                        }
                    }
                }
            }

            return false;
        }





    }
}
