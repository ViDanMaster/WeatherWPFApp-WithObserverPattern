using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using dotenv;
using dotenv.net;
using dotenv.net.Utilities;

namespace WeatherApp
{
    public class WeatherViewModel : ISubject<WeatherInfo.Root>
    {
        private WeatherInfo.Root _currentWeather;
        private List<Interfaces.IObserver<WeatherInfo.Root>> _observers = new List<Interfaces.IObserver<WeatherInfo.Root>>();

        public WeatherInfo.Root CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                NotifyObservers(value);
            }
        }

        public void Subscribe(Interfaces.IObserver<WeatherInfo.Root> observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(Interfaces.IObserver<WeatherInfo.Root> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(WeatherInfo.Root data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(data);
            }
        }

        public async Task UpdateWeatherDataAsync(string city)
        {
            using (var client = new HttpClient())
            {
                var apiKey = Environment.GetEnvironmentVariable("API_KEY");
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=hu";

                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherInfo.Root>(jsonString);

                    CurrentWeather = new WeatherInfo.Root
                    {
                        name = weatherData.name,
                        main = weatherData.main,
                    };
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error fetching weather data: {e.Message}");
                }
            }
        }
    }
}
