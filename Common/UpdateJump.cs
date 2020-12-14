namespace Platformer.Desktop
{
    public static class UpdateJump
    {
        public static void Update(
           GameObject obj
           , InputController input
           , ValueKeeper<int> grounded
        )
        {
            if (grounded > 0 && (input.Jump.IsPressStaring || input.Jump.Heat > 0))
                obj.Velocity.Y = -Const.jumpForce;

            if (!input.Jump.IsPressed  && obj.Velocity.Y < 0)
                obj.Velocity.Y += Const.stoppingGravity;
        }

    }
}
