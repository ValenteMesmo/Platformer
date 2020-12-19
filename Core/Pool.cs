using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public class Pool<T> where T : class
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
                return (T)Activator.CreateInstance(typeof(T), true);

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
