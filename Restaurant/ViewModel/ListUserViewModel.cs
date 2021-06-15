using Newtonsoft.Json;
using Restaurant.Annotations;
using Restaurant.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    class ListUserViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public User User { get; private set; }
        public string Image { get; set; }
        public float Tempeture { get; set; }
        public string Name { get; set; }
        public int NumPersons { get; set; }
        public string SearchText { get; set; }
        public bool FilterTerrassa { get; set; }
        public bool FilterRightNow { get; set; }
        public bool FilterMorning{ get; set; }

        public  ListUserViewModel(User user)
        {
            Restaurants = App.dbContext.Restaurants.ToList();
            User = user;
            Name = user.Name.ToUpper();
            GetWeather();
        }

      
        public void FindRestoraunt()
        {
            List<Restaurant> SearchRestik = new List<Restaurant>();
            if (SearchText == "")
            {
                return;
            }
            else
            {
                
                if (Restaurants.Where(x=>x.Name==SearchText).ToList().Count!=0)
                {
                    SearchRestik = Restaurants.Where(x => x.Name == SearchText).ToList();
                }
                else if (App.dbContext.Kitchens.Where(x => x.Name == SearchText).Select(x => x.Restaurants).ToList().Count != 0)
                {
                    foreach (var item in App.dbContext.Kitchens.Where(x => x.Name == SearchText).Select(x => x.Restaurants).ToList())
                    {
                        SearchRestik.AddRange(item);
                    }
                }
                else if(App.dbContext.SearchTerms.Where(x=>x.Name==SearchText).ToList().Count != 0)
                {
                    var b = App.dbContext.SearchTerms.Where(x=> x.Name == SearchText).ToList();
                    foreach(var item in b)
                    {
                        SearchRestik.Add(item.Restaurant);
                    }
                    SearchRestik.Distinct();
                }
                Restaurants = SearchRestik;
            }
        }

        public void FilterList()
        {
            if (FilterRightNow)
            {
                Restaurants = Restaurants.Where(r => (r.TimeClose < new TimeSpan(6, 0, 0) ? 
                r.TimeClose < DateTime.Now.TimeOfDay : r.TimeClose > DateTime.Now.TimeOfDay) 
                && r.TimeOpen < DateTime.Now.TimeOfDay).ToList();
            }
            if (FilterTerrassa)
            {
                Restaurants = Restaurants.Where(r => r.IsTerrassa == true).ToList();
            }
            if (FilterMorning)
            {
                Restaurants = Restaurants.Where(r => r.TimeClose < new TimeSpan(6, 0, 0)
                && r.TimeClose > new TimeSpan(0, 0, 0)).ToList();
            }
        }

        public void GetWeather()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Kazan&units=metric&appid=5e9ec2f8d2c09898bc3f5efce943d7f8";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            Tempeture = weatherResponse.Main.Temp;
            switch (weatherResponse.Weather[0].Icon)
            {
                case "01d":
                    Image = "Weather/sunny.png";
                    break;
                case "02d":
                    Image = "Weather/clear-cloudy.png";
                    break;
                case "03d":
                    Image = "Weather/cloudy.png";
                    break;
                case "04d":
                    Image = "Weather/mostly-cloudy.png";
                    break;
                case "09d":
                    Image = "Weather/showers.png";
                    break;
                case "10d":
                    Image = "Weather/drizzle.png";
                    break;
                case "11d":
                    Image = "Weather/thunderstroms.png";
                    break;
                case "13d":
                    Image = "Weather/snow.png";
                    break;
                case "50d":
                    Image = "Weather/foggy.png";
                    break;

                default:
                    if (weatherResponse.Main.Temp >= 0)
                    {
                        Image = "Weather/hot.png";
                    }
                    else
                    {
                        Image = "Weather/cold.png";
                    }
                    break;
            }
      }
    }
}
