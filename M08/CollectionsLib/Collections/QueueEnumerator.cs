using System.Collections;
using System.Collections.Generic;

namespace CollectionsLib.Collections
{
    public class QueueEnumerator<T> : IEnumerator<T>
    {

        public QueueEnumerator (MyQueue<T> collection)
        {
            _collection = collection.GetValues();
            _position = -1;
        }

        public bool MoveNext()
        {
            if (_position < _collection.Length - 1)
            {
                ++_position;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _position = 0;
        }

        public void Dispose() { }

        public T Current => _collection[_position];

        object IEnumerator.Current => Current;

        private int _position;
        private readonly T[] _collection;
    }
}
