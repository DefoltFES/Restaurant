using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class LoginViewModel
    {
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public bool isExist()
        {
            var user = App.dbContext.users.Where(x => x.phone == PhoneNumber).FirstOrDefault();
            if (user !=null)
            {
                if (user.password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public void WriteLog()
        {
            var user= App.dbContext.users.Where(x => x.phone == PhoneNumber).FirstOrDefault();
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
                writer.WriteLine(user.id_user+ " "+" "+App.TodayDate); 
                writer.Close();
            }
        }

        public bool AutoLogin()
        {
            var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"logs";
            var fullFilePath = fullDirectoryPath + @"\history.txt";
            using (StreamReader streamReader=new StreamReader(new FileStream(fullFilePath,FileMode.Open)))
            {
              var lastLogin= streamReader.ReadToEnd().Split(' ');
              DateTime dateLastLogin = DateTime.Parse(lastLogin[1]);
              var razniza = (App.TodayDate - dateLastLogin).TotalDays;
              if (razniza <= 7)
              {
                  return true;
              }
              else
              {
                  return false;
              }
            }
        }





    }
}
