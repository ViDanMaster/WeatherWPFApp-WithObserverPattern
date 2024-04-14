namespace WeatherApp.Interfaces
{
    public interface ISubject<T>
    {
        void Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
        void NotifyObservers(T data);
    }
}
