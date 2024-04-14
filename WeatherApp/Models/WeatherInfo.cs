using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class WeatherInfo
    {

        public class Main
        {
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }

        public class Root
        {
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }


    }
}
