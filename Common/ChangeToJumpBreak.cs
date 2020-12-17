namespace Platformer.Desktop
{
    public static class ChangeToJumpBreak
    {
        public static void Try(
            GameObject obj
            , InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (!input.Jump.IsPressed && obj.Velocity.Y < 0 && obj.Velocity.Y < -Const.stoppingGravity)
                state.SetValue(State.JumpBreak);
        }
    }
}
