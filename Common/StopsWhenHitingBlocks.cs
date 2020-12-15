namespace Platformer.Desktop
{
    public static class StopsWhenHitingBlocks
    {
        private static CollisionHandler handler = null;
        public static CollisionHandler Create()
        {
            handler = CollisionHandler.Create();

            handler.Left = Left;
            handler.Right = Right;
            handler.Bot = Bot;
            handler.Top = Top;

            return handler;
        }


        private static void Bot(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                Source.Parent.Position.Y = 
                    target.Top() 
                    - Source.Height 
                    - 1
                    - Source.Area.Y;
                Source.Parent.Velocity.Y = 0;
            }
        }

        private static void Left(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                Source.Parent.Position.X = target.Right() + 1 - Source.Area.X;
                Source.Parent.Velocity.X = 0;
            }
        }

        private static void Right(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                Source.Parent.Position.X = target.Left() - Source.Area.X - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        private static void Top(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y =
                    target.Bottom()
                    - Source.Area.Y
                    + 1;
                //Source.Parent.Velocity.Y = 0;
            }
        }
    }
}
