using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsLib.Collections
{
    public class MyStack<T> : IEnumerable<T>
    {
        public MyStack()
        {
            _stack = new T[8];
            _tail = -1;
        }

        public void Push(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            ++_tail;

            if (_tail >= _stack.Length)
            {
                Array.Resize(ref _stack, _stack.Length * 2);
            }

            _stack[_tail] = item;
        }

        public T Pop()
        {
            var returningElement = Peek();

            _stack[_tail] = default;
            --_tail;

            return returningElement;
        }

        public T Peek()
        {
            if (_tail == -1)
            {
                throw new InvalidOperationException("Stack was not initialized");
            }

            return _stack[_tail];
        }

        public bool Contains(T findingItem)
        {
            if (findingItem == null)
            {
                throw new ArgumentNullException();
            }

            for (var i = 0; i <= _tail; ++i)
            {
                if (Equals(findingItem, _stack[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _stack = new T[8];
            _tail = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] GetValues()
        {
            var stack = new T[Count];

            for (var i = 0; i < Count; ++i)
            {
                stack[i] = _stack[i];
            }

            return stack;
        }

        public int Count => _tail + 1;
        private T[] _stack;
        private int _tail;
    }
}
