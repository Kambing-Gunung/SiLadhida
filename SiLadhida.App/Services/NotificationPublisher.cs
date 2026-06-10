using System.Collections.Generic;

namespace SiLadhida.App.Services
{
    public class NotificationPublisher
    {
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.UpdateNotification(message);
            }
        }
    }
}
