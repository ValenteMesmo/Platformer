namespace Platformer.Desktop
{
    public static class UpdateVelocityUsingInputs
    {
        const int move_speed = 1200;
        const int acceleration = move_speed / 6;
        const int deceleration = move_speed / 3;

        public static void Update(
            GameObject obj
            , InputController input
            , ValueKeeper<bool> facingRight
        )
        {
            if (input.Left.IsPressed)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(
                    obj.Velocity.X > acceleration ? acceleration * 2 : acceleration, -move_speed);

            else if (input.Right.IsPressed)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(
                    obj.Velocity.X < -acceleration ? acceleration * 2 : acceleration, move_speed);

            else if (obj.Velocity.X > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(deceleration, 0);

            else if (obj.Velocity.X < 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(deceleration, 0);

            //TODO: move to other file
            if (obj.Velocity.X > 0)
                facingRight.SetValue(true);

            else if (obj.Velocity.X < 0)
                facingRight.SetValue(false);
        }
    }
}
