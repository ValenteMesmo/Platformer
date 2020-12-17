namespace Platformer.Desktop
{
    public static class ChangeToJumpState
    {
        public static void Try(
            GameObject obj
            , InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            state.SetValue(State.Jump);
        }
    }
}
