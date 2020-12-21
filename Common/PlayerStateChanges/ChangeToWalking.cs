namespace Platformer.Desktop
{
    public static class ChangeToWalking
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state
            , GameObject obj)
        {
            if ((input.Left || input.Right) && grounded == Const.Grounded_Timer && obj.Velocity.Y >= 0)
            {
                state.SetValue(State.Walking);
            }
        }
    }
}
