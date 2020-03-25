using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsLib.Collections
{
    public class MyQueue<T> : IEnumerable<T>
    {
        public MyQueue()
        {
            _queue = new T[8];
            _tail = -1;
        }

        public void Enqueue(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            ++_tail;

            if (_tail >= _queue.Length)
            {
                Array.Resize(ref _queue, _queue.Length * 2);
            }

            _queue[_tail] = item;
        }

        public T Dequeue()
        {
            var returningElement = Peek();

            MoveQueue();

            return returningElement;
        }

        private void MoveQueue()
        {
            for (var i = 0; i < _tail; ++i)
            {
                _queue[i] = _queue[i + 1];
            }

            --_tail;
        }

        public T Peek()
        {
            if (_tail == -1)
            {
                throw new InvalidOperationException("Queue was not initialized");
            }

            return _queue[0];
        }

        public bool Contains(T findingItem)
        {
            if (findingItem == null)
            {
                throw new ArgumentNullException();
            }

            for (var i = 0; i <= _tail; ++i)
            {
                if (Equals(findingItem, _queue[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _queue = new T[8];
            _tail = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] GetValues()
        {
            var queue = new T[Count];

            for (var i = 0; i < Count; ++i)
            {
                queue[i] = _queue[i];
            }

            return queue;
        }

        public int Count => _tail + 1;
        private int _tail;
        private T[] _queue;
    }
}
