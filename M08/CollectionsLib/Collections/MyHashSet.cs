using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsLib.Collections
{

    public class MyHashSet<T> : IEnumerable<T>
    {
        public MyHashSet()
        {
            _set = new Slot<T>[16];
            _buckets = new bool[16];
            BucketsElementsCounter = 0;
            Count = 0;
        }

        public bool Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (BucketsElementsCounter + 1 > _set.Length * 0.75)
            {
                IncreaseCapacity();
            }

            return AddIfNotPresent(value);
        }

        private void IncreaseCapacity()
        {
            var newSet = new Slot<T>[_set.Length * 2];
            var newBuckets = new bool[_buckets.Length * 2];

            for (var i = 0; i < _buckets.Length; ++i)
            {
                if (!_buckets[i])
                {
                    continue;
                }

                var slot = _set[i];
                AddSlotToNewSet(slot, ref newSet, ref newBuckets);

                while (slot.Next != null)
                {
                    slot = slot.Next;
                    AddSlotToNewSet(slot, ref newSet, ref newBuckets);
                }
            }

            _set = newSet;
            _buckets = newBuckets;
        }

        private void AddSlotToNewSet(Slot<T> slot, ref Slot<T>[] newSet, ref bool[] newBucket)
        {
            var bucket = slot.Value.GetHashCode() % newSet.Length;

            if (newBucket[bucket])
            {
                var nextSlot = newSet[bucket];

                while (nextSlot.Next != null)
                {
                    nextSlot = nextSlot.Next;
                }

                nextSlot.Next = new Slot<T>(slot.Value);
            }
            else
            {
                newSet[bucket] = new Slot<T>(slot.Value);
                newBucket[bucket] = true;
            }
        }

        private bool AddIfNotPresent(T value)
        {
            var bucket = value.GetHashCode() % _buckets.Length;

            if (!_buckets[bucket])
            {
                _set[bucket] = new Slot<T>(value);
                _buckets[bucket] = true;
                ++Count;
                ++BucketsElementsCounter;
                return true;
            }

            var slot = _set[bucket];
            if (Equals(slot.Value, value))
            {
                return false;
            }

            while (slot.Next != null)
            {
                if (Equals(slot.Next.Value, value))
                {
                    return false;
                }
            }

            slot.Next = new Slot<T>(value);
            ++Count;

            return true;
        }

        public bool Contains(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var bucket = value.GetHashCode() % _set.Length;

            if (!_buckets[bucket])
            {
                return false;
            }

            var slot = _set[bucket];
            if (Equals(slot.Value, value))
            {
                return true;
            }

            while (slot.Next != null)
            {
                if (Equals(slot.Next.Value, value))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var bucket = value.GetHashCode() % _set.Length;

            if (!_buckets[bucket])
            {
                return false;
            }

            var slot = _set[bucket];

            if (Equals(slot.Value, value))
            {
                _set[bucket] = slot.Next;

                if (_set[bucket] == null)
                {
                    _buckets[bucket] = false;
                }

                --Count;
                return true;
            }

            while (slot.Next != null)
            {
                var prevSlot = slot;
                slot = slot.Next;

                if (Equals(slot.Value, value))
                {
                    prevSlot.Next = slot.Next;
                    --Count;
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _buckets = new bool[16];
            _set = new Slot<T>[16];
            Count = 0;
            BucketsElementsCounter = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var bucket = 0; bucket < _buckets.Length; ++bucket)
            {
                if (!_buckets[bucket])
                {
                    continue;
                }

                var slot = _set[bucket];
                yield return slot.Value;

                while (slot.Next != null)
                {
                    slot = slot.Next;
                    yield return slot.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool[] _buckets;
        private Slot<T>[] _set;
        private int BucketsElementsCounter { get; set; }
        public int Count { get; private set; }
    }
}
