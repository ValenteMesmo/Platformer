namespace Platformer.Desktop
{
    public static class UpdatePlayerAnimation
    {
        public static void Update(GameInputs input, Animation animation, ValueKeeper<bool> facingRight, ValueKeeper<bool> grounded)
        {
            if (!grounded.GetValue())
                animation.Frame = 1;
            else if (input.Left > 0 || input.Right > 0)
                if (animation.Frame == 1)
                    animation.Frame = 0;
                else
                    animation.Frame = 1;
            else
                animation.Frame = 0;

            animation.flipped = !facingRight.GetValue();
        }
    }
}
