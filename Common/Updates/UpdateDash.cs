namespace Platformer.Desktop
{
    public static class UpdateDash
    {
        public static void Update(
            GameObject obj
            , ValueKeeper<bool> facingRight
        )
        {
            if (facingRight)
                obj.Velocity.X = Const.move_speed * 3;
            else
                obj.Velocity.X = -Const.move_speed * 3;

            obj.Velocity.Y = 0;
        }
    }
}
