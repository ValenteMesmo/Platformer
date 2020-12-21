namespace Platformer.Desktop
{
    public static class ChangeToJumpStart
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (grounded > 0 && (input.Jump.IsPressStaring || input.Jump.Heat > 0))
                state.SetValue(State.JumpStart);
        }
    }
}
