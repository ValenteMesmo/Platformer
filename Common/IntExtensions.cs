namespace Platformer.Desktop
{
    public static class IntExtensions
    {
        public static int IncrementUntil(this int value, int increment, int limit)
        {
            value = value + increment;

            if (value > limit)
                return limit;

            return value;
        }

        public static int DecrementUntil(this int value, int decrement, int limit)
        {
            value = value - decrement;

            if (value < limit)
                return limit;

            return value;
        }
    }
}
