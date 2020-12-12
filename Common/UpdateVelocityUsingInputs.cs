namespace Platformer.Desktop
{
    public static class UpdateVelocityUsingInputs
    {
        const int move_speed = 1200;
        const int acceleration = 600;
        const int deceleration = 100;

        public static void Update(
            GameObject obj
            , GameInputs input
            , ValueKeeper<bool> grounded
            , ValueKeeper<bool> facingRight
        )
        {
            if (input.Left > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(acceleration, -move_speed);
            else if (input.Right > 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(acceleration, move_speed);
            else if (obj.Velocity.X > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(deceleration, 0);
            else if (obj.Velocity.X < 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(deceleration, 0);


            if (obj.Velocity.X > 0)
                facingRight.SetValue(true);
            else if (obj.Velocity.X < 0)
                facingRight.SetValue(false);
        }
    }
}
