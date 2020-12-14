using System.Collections.Generic;

namespace Platformer.Desktop
{
    public static class UpdateJump
    {
        public static void Update(
           GameObject obj
           , InputController input
           , ValueKeeper<bool> grounded
        )
        {
            if (grounded && input.Jump.IsPressStaring)
            {
                obj.Velocity.Y = -Const.jumpForce;
            }

            if (!input.Jump.IsPressed  && obj.Velocity.Y < 0)
            {
                obj.Velocity.Y += Const.stoppingGravity;
            }
        }

    }
}
