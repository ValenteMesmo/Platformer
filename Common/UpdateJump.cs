﻿namespace Platformer.Desktop
{
    public static class UpdateJump
    {
        public static void Update(
            GameObject obj
            , ValueKeeper<State> state
        )
        {
            if (state.Changed)
            {
                obj.Velocity.Y = -Const.jumpForce;
                state.SetValue(State.Jump);
            }
        }

    }
}
