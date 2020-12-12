namespace Platformer.Desktop
{
    public static class UpdateJump
    {
        const int jump_force = -3900;

        public static void Update(
           GameObject obj
           , GameInputs input
           , ValueKeeper<bool> grounded
           , ValueKeeper<bool> jumping
        )
        {
            if (grounded && input.Jump > 0)
            {
                obj.Velocity.Y = jump_force;
                jumping.SetValue(true);
            }
            //else if (grounded && obj.Velocity.Y >= 0)
            //    jumping.SetValue(false);

            if (!grounded && obj.Velocity.Y < 0 && input.Jump == 0)
                obj.Velocity.Y = 0;
        }

    }
}
