namespace Platformer.Desktop
{
    public static class UpdateJumpBreak
    {
        public static void Update(GameObject obj)
        {
            obj.Velocity.Y += Const.stoppingGravity;
        }
    }
}
