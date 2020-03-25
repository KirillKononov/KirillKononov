using System.Threading;

namespace SubscriptionLib
{
    public class Channel
    {
        public delegate void CountdownHandler(string message);

        public event CountdownHandler Notify;

        public Channel(string name)
        {
            Name = name;
        }

        public void AddSubscriber(Subscriber s)
        {
            Notify += s.NotifyHandler;
        }

        public void DeleteSubscriber(Subscriber s)
        {
            Notify -= s.NotifyHandler;
        }

        public void SendNotification(int delay)
        {
            Thread.Sleep(delay);
            Notify?.Invoke($"You got this message because you are a new subscriber in {Name} channel!");
        }

        public string Name { get; }
    }
}
