using System;
using System.Collections.Generic;
using System.Linq;
using CollectionsLib;
using CollectionsLib.Collections;

namespace CollectionLibFunctionality
{
    class Program
    {
        private static void MyQueueFunctionality()
        {
            var queue = new MyQueue<int>();

            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(4);

            Console.WriteLine($"Peek result: {queue.Peek()}");
            Console.WriteLine($"Dequeue result: {queue.Dequeue()}");

            Console.WriteLine("Iteration result:");
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }

            queue.Clear();
            Console.WriteLine($"Queue was cleared and it's size: {queue.Count}");
        }

        private static void MyStackFunctionality()
        {
            var stack = new MyStack<int>();

            stack.Push(5);
            stack.Push(1);
            stack.Push(4);

            Console.WriteLine($"Peek result: {stack.Peek()}");
            Console.WriteLine($"Pop result: {stack.Pop()}");

            Console.WriteLine("Iteration result:");
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            stack.Clear();
            Console.WriteLine($"Stack was cleared and it's size: {stack.Count}");
        }

        private static void MyHashSetFunctionality()
        {
            var set = new MyHashSet<int>() { 1, 2, 5, 4};

            set.Add(64);
            set.Add(0);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            set.Remove(64);
            Console.WriteLine("64 was removed");

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            set.Clear();
            Console.WriteLine($"Set was cleared and it's size: {set.Count}");
        }

        private static void BinarySearchFunctionality()
        {
            var mass = new int[4] { 1, 2, 3, 4 };
            const int num = 2;
            var index = BinarySearch<int>.Find(mass, num, Comparer<int>.Default);
            Console.WriteLine($"The element {num} has {index} index");
        }

        private static void FibonacciFunctionality()
        {
            var fibonacci = FibonacciNumbers.GetNumbers(6).ToList();

            foreach (var num in fibonacci)
            {
                Console.Write($"{num} ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("BinarySearch: ");
                BinarySearchFunctionality();

                Console.WriteLine();

                Console.WriteLine("Fibonacci: ");
                FibonacciFunctionality();

                Console.WriteLine();

                Console.WriteLine("Queue: ");
                MyQueueFunctionality();

                Console.WriteLine();

                Console.WriteLine("Stack: ");
                MyStackFunctionality();

                Console.WriteLine();

                Console.WriteLine("Set: ");
                MyHashSetFunctionality();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
