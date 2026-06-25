using System;
using System.Collections.Generic;

namespace SiLadhida.App.Services.Observer
{
    public class NotificationPublisher
    {
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer)
        {
            Console.WriteLine("Observer subscribed: " + observer.GetType().Name);

            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            Console.WriteLine("Notify called: " + message);

            foreach (var observer in _observers)
            {
                observer.UpdateNotification(message);
            }
        }
    }
}
