using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public class Pool<T> where T : new()
    {
        private readonly Queue<T> AvailableItems;
        public int AvailableCount => AvailableItems.Count;

        public Pool()
        {
            AvailableItems = new Queue<T>();
        }

        public T Get()
        {
            if (AvailableItems.Count == 0)
                return new T();

            return AvailableItems.Dequeue();
        }

        public void Return(T item)
        {
#if DEBUG
            if (item == null)
                throw new ArgumentNullException(nameof(item));
#endif

            AvailableItems.Enqueue(item);
        }

        public void Clear()
        {
            AvailableItems.Clear();
        }
    }
}
