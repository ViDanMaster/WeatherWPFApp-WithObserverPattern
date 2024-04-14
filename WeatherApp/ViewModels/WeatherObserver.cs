using System.Windows.Controls;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class WeatherObserver : IObserver<WeatherInfo.Root>
    {
        private readonly Label temperatureLabel;
        private readonly Label humidityLabel;
        private readonly Label pressureLabel;

        public WeatherObserver(Label temperatureLabel, Label humidityLabel, Label pressureLabel)
        {
            this.temperatureLabel = temperatureLabel;
            this.humidityLabel = humidityLabel;
            this.pressureLabel = pressureLabel;
        }

        public void Update(WeatherInfo.Root weatherData)
        {
            temperatureLabel.Content = $"Hőmérséklet: {weatherData.main.temp} °C";
            humidityLabel.Content = $"Páratartalom: {weatherData.main.humidity}%";
            pressureLabel.Content = $"Nyomás: {weatherData.main.pressure} hPa";
        }
     }
}
