namespace Platformer.Desktop
{
    public static class DetectsIfGrounded
    {
        private static CollisionHandler handler = null;
        public static CollisionHandler Create(ValueKeeper<int> grounded)
        {
            handler = CollisionHandler.Create();

            handler.Left =
            handler.Right =
            handler.Bot = 
            handler.Top = (a,b) => Bot(grounded, b);

            return handler;
        }

        private static void Bot(ValueKeeper<int> grounded, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                grounded.SetValue(6);
            }
        }

    }
}
