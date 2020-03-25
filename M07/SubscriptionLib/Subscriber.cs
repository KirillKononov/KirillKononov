using System;

namespace SubscriptionLib
{
    public class Subscriber
    {
        public Subscriber(string name)
        {
            Name = name;
        }

        public void NotifyHandler(string message)
        {
            Console.WriteLine($"Hello, {Name}!\n{message}");
        }

        public string Name { get; }
    }
}
