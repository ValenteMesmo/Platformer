namespace Platformer.Desktop
{
    public static class UpdateJumpBreak
    {
        public static void Update(
            GameObject obj
            , ValueKeeper<State> state
        )
        {
            if (state.Changed)
            {
                obj.Velocity.Y += Const.stoppingGravity;
                state.SetValue(State.JumpBreak);
            }
        }

    }
}
