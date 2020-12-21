namespace Platformer.Desktop
{
    public static class ChangeToBumpHead
    {
        public static void Try(ValueKeeper<State> state, GameObject obj, ValueKeeper<int> hittingHead)
        {
            if (hittingHead > 0 && obj.Velocity.Y < 0)
                  state.SetValue(State.HeadBump);
        }
    }
}
