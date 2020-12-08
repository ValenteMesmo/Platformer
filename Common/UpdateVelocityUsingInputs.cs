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

    public static class UpdateVelocityUsingInputs
    {
        const int move_speed = 1200;
        const int acceleration = 600;
        const int deceleration = 600;
        const int jump_force = -3900;

        public static void Update(
            GameObject obj
            , GameInputs input
            , ValueKeeper<bool> grounded
            , ValueKeeper<bool> facingRight)
        {
            if (input.Left > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(acceleration, -move_speed);
            else if (input.Right > 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(acceleration, move_speed);
            else if (obj.Velocity.X > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(deceleration, 0);
            else if (obj.Velocity.X < 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(deceleration, 0);

            if (grounded.GetValue() && input.Jump > 0)
                obj.Velocity.Y = jump_force;

            if (obj.Velocity.X > 0)
                facingRight.SetValue(true);
            else if (obj.Velocity.X < 0)
                facingRight.SetValue(false);
        }
    }
}
