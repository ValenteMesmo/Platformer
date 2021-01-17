namespace Platformer.Desktop
{
    public static class ChangeToIdle
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (!input.Down.IsPressed
                && !input.Up.IsPressed
                && !input.Left.IsPressed
                && !input.Right.IsPressed
                && grounded == Const.Grounded_Timer)
                state.SetValue(State.Idle);
        }
    }

    public static class ChangeToCrouch
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (input.Down.IsPressed && grounded == Const.Grounded_Timer)
                state.SetValue(State.Crouch);
        }
    }

    public static class ChangeToLookup
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (input.Up.IsPressed && grounded == Const.Grounded_Timer)
                state.SetValue(State.LookUp);
        }
    }
}
