namespace Platformer.Desktop
{
    public static class Friction
    {
        public static void Apply(
            GameObject obj
            , InputController input
        )
        {
            if(!input.Left && !input.Right)
            if (obj.Velocity.X > 0)
                obj.Velocity.X = obj.Velocity.X.DecrementUntil(Const.deceleration, 0);

            else if (obj.Velocity.X < 0)
                obj.Velocity.X = obj.Velocity.X.IncrementUntil(Const.deceleration, 0);
        }
    }
}
