namespace Platformer.Desktop
{
    public static class ChangeToJumpState
    {
        public static void Try(ValueKeeper<State> state)
        {
            state.SetValue(State.Jump);
        }
    }
}
