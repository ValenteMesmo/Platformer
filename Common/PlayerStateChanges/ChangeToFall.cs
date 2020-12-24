namespace Platformer.Desktop
{
    public static class ChangeToFall
    {
        public static void Try(
            GameObject obj
            , ValueKeeper<int> grounded
            , ValueKeeper<int> hittingHead
            , ValueKeeper<State> state)
        {
            if (grounded < Const.Grounded_Timer && obj.Velocity.Y >= 0)
                state.SetValue(State.Fall);
            else if (hittingHead > 0 && obj.Velocity.Y > -1000)
                state.SetValue(State.Fall);
        }
    }
}
