using System;

namespace Platformer.Desktop
{
    public class ValueKeeper<T>
    {
        private static Pool<ValueKeeper<T>> Pool = new Pool<ValueKeeper<T>>();
        private T value;

        [Obsolete]
        public ValueKeeper()
        {

        }

        public static ValueKeeper<T> Create()
        {
            var current = Pool.Get();
            current.value = default;

            return current;
        }

        public void SetValue(T newValue) => value = newValue;
        public T GetValue() => value;

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
