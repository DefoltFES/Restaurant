using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.service
{
   
        public class WeatherResponse
        {
            public MainInfo Main { get; set; }
            public string Name { get; set; }
            public List<WeatherInfo> Weather { get; set; }
        }

        public class MainInfo
        {
            public float Temp { get; set; }
        }

        public class WeatherInfo
        {
            public string Icon { get; set; }
        }
    
}
