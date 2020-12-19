namespace Platformer.Desktop
{
    public static class TimerTrigger
    {
        private static CollisionHandler handler = null;
        public static CollisionHandler Create(ValueKeeper<int> keeper, int duration)
        {
            handler = CollisionHandler.Create();

            handler.Left =
            handler.Right =
            handler.Bot = 
            handler.Top = (source,target) => Bot(keeper, target, duration);

            return handler;
        }

        private static void Bot(ValueKeeper<int> grounded, Collider target, int duration)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                grounded.SetValue(duration);
            }
        }

    }
}
