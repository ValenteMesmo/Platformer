namespace Platformer.Desktop
{
    public static class ChangeToWalking
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if ((input.Left || input.Right) && grounded == Const.Grounded_Timer)
            {
                state.SetValue(State.Walking);
            }
        }
    }
}
