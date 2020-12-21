namespace Platformer.Desktop
{
    public static class UpdateJumpStart
    {
        public static void Update(GameObject obj)
        {
            obj.Velocity.Y = -Const.jumpForce;
        }
    }
}
