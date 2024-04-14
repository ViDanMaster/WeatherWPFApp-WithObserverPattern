using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WeatherViewModel _viewModel;
        private List<City> _cities;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new WeatherViewModel();
            DataContext = _viewModel;
            _cities = LoadCityData();

            _viewModel.Subscribe(new WeatherObserver(temperatureLabel, humidityLabel, pressureLabel));

            cityTextBox.TextChanged += CityTextBox_TextChanged;
        }

        private async void UpdateButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string city = cityTextBox.Text;
            await _viewModel.UpdateWeatherDataAsync(city);
        }

        private List<City> LoadCityData()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "city.list.json");
            string jsonData = File.ReadAllText(filePath);

            List<City> cities = JsonConvert.DeserializeObject<List<City>>(jsonData);
            return cities;
        }

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = cityTextBox.Text;
            if (searchText.Length > 0)
            {
                List<string> filteredCities = _cities.Where(c => c.name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)).Select(c => c.name).Distinct().ToList();

                citySuggestionsListBox.ItemsSource = filteredCities;
            }
            else
            {
                if (citySuggestionsListBox != null)
                {
                    citySuggestionsListBox.ItemsSource = null;
                }
            }
        }

        private void CitySuggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (citySuggestionsListBox.SelectedItem != null)
            {
                string selectedCity = citySuggestionsListBox.SelectedItem.ToString();
                cityTextBox.Text = selectedCity;
            }
        }
    }
}
