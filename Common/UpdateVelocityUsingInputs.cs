namespace Platformer.Desktop
{
    public static class UpdateVelocityUsingInputs
    {
        public static void Update(
            GameObject obj
            , ValueKeeper<bool> facingRight
            , InputController input
        )
        {


            if (input.Left)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(
                    obj.Velocity.X > Const.acceleration ? Const.acceleration * 2 : Const.acceleration, -Const.move_speed);

            else if (input.Right)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(
                    obj.Velocity.X < -Const.acceleration ? Const.acceleration * 2 : Const.acceleration, Const.move_speed);
        }
    }
}
