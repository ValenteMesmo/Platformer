namespace Platformer.Desktop
{
    public static class ChangeToIdle
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (!input.Left.IsPressed && !input.Right.IsPressed && grounded == Const.Grounded_Timer)
                state.SetValue(State.Idle);                
        }
    }
}
