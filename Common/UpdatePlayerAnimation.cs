﻿namespace Platformer.Desktop
{
    public static class UpdatePlayerAnimation
    {
        public static void Update(InputController input, Animation animation, ValueKeeper<bool> facingRight, ValueKeeper<bool> grounded)
        {
            if (!grounded.GetValue())
                animation.Frame = 1;
            else if (input.Left.IsPressed || input.Right.IsPressed)
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