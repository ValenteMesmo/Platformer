using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public class ValueKeeper<T>
    {
        private static Pool<ValueKeeper<T>> Pool = new Pool<ValueKeeper<T>>();
        private T previousValue;
        private T value;
        [Obsolete("Remover")]
        public bool Changed { get; private set; }

        private ValueKeeper()
        {

        }

        public static ValueKeeper<T> Create()
        {
            var current = Pool.Get();
            current.previousValue = default;
            current.value = default;
            current.Changed = false;
            return current;
        }

        public void SetValue(T newValue)
        {
            Changed = !EqualityComparer<T>.Default.Equals(value, newValue);
            previousValue = value;
            value = newValue;
        }

        public T GetValue() => value;
        public T GetPreviousValue() => previousValue;

        public void Destroy()
        {
            Pool.Return(this);
        }

        public static implicit operator T(ValueKeeper<T> keeper)
        {
            return keeper.value;
        }

        public override string ToString()
        {
            return value?.ToString();
        }

    }
}
