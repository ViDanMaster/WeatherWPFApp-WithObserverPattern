using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface IObserver<T>
    {
        void Update(T weatherData);
    }
}
