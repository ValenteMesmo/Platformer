namespace Platformer.Desktop
{
    public static class Const
    {
        public const int Scale = 100;

        public const int GRAVITY_ACCELERATION = 90;
        public const int GRAVITY_MAXSPEED = 3000;

        public const int stoppingGravity = GRAVITY_MAXSPEED / 3;
        public const int jumpForce = GRAVITY_ACCELERATION * 24;
        public const int Grounded_Timer = 9;

        public const int move_speed = 1200;
        public const int acceleration = move_speed / 6;
        public const int deceleration = move_speed / 3;
    }
}
