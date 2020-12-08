namespace Platformer.Desktop
{
    public static class DetectsIfGrounded
    {
        private static CollisionHandler handler = null;
        public static CollisionHandler Create(ValueKeeper<bool> grounded)
        {
            handler = CollisionHandler.Create();

            handler.Left =
            handler.Right =
            handler.Bot = 
            handler.Top = (a,b) => Bot(grounded, b);

            return handler;
        }

        private static void Bot(ValueKeeper<bool> grounded, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                grounded.SetValue(true);
            }
        }

    }
}
