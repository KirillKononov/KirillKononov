namespace CollectionsLib.Collections
{
    internal class Slot<T>
    {
        public Slot(T value)
        {
            Value = value;
            Next = null;
        }

        internal Slot<T> Next { get; set; }
        internal T Value { get; set; }
    }
}
