namespace Platformer.Desktop
{
    public static class ChangeToDash
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> cooldown
            , ValueKeeper<State> state)
        {
            if (input.Dash.IsPressStaring && cooldown == 0)
            {
                state.SetValue(State.Dash);
                cooldown.SetValue(9);
            }
        }
    }
}
