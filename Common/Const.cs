using Microsoft.Xna.Framework;

namespace Platformer.Desktop
{
    public static class Const
    {
        public const int Scale = 100;

        public const int GRAVITY_ACCELERATION = 90;
        public const int GRAVITY_MAXSPEED = 3000;

        public const int stoppingGravity = GRAVITY_MAXSPEED / 9;
        public const int jumpForce = GRAVITY_ACCELERATION * 30;
        public const int Grounded_Timer = 9;

        public const int move_speed = 1200;
        public const int acceleration = move_speed / 6;
        public const int deceleration = move_speed / 3;

    }
    //public static class Const
    //{
    //    public const int Scale = 100;

    //    private const int _GRAVITY_MAXSPEED = (int)(1800 * GameWrapper.frameRate / GameWrapper.frameRate);
    //    private const int _GRAVITY_ACCELERATION = (int)(54 * GameWrapper.frameRate / GameWrapper.frameRate);
    //    private const int _move_speed = (int)(720 * GameWrapper.frameRate / GameWrapper.frameRate);

    //    public const int GRAVITY_ACCELERATION = (int)(_GRAVITY_ACCELERATION);
    //    public const int GRAVITY_MAXSPEED = (int)(_GRAVITY_MAXSPEED);

    //    public const int stoppingGravity = _GRAVITY_MAXSPEED / 9;
    //    public const int jumpForce = _GRAVITY_ACCELERATION * 30;
    //    public const int Grounded_Timer = 9;

    //    public const int move_speed = (int)(_move_speed);
    //    public const int acceleration = _move_speed / 6;
    //    public const int deceleration = _move_speed / 3;
    //}
}