﻿namespace Platformer.Desktop
{
    public static class UpdateGravity
    {
        public static void Update(GameObject obj)
        {
            obj.Velocity.Y = 
                obj.Velocity.Y.IncrementUntil(
                    Const.GRAVITY_ACCELERATION, Const.GRAVITY_MAXSPEED
                );
        }
    }
}
