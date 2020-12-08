using Microsoft.Xna.Framework;
using System;

namespace Platformer.Desktop
{
    public class Collider
    {
        private static Collider currentCollider = null;
        private static readonly Pool<Collider> Pool = new Pool<Collider>();

        public GameObject Parent { get; private set; } = null;
        public Rectangle Area;

        public CollisionHandler Handler = null;

        public int RelativeX => Parent.Position.X + Area.X;
        public int RelativeY => Parent.Position.Y + Area.Y;
        public int Width => Area.Width;
        public int Height => Area.Height;
        public Rectangle RelativeArea =>
            new Rectangle(
                Area.X + Parent.Position.X
                , Area.Y + Parent.Position.Y
                , Area.Width
                , Area.Height
            );

        //public CollisionHandler Handler = CollisionHandler.Empty;

        //public Action<Collider, Collider> TopCollisionHandler;
        //public Action<Collider, Collider> LeftCollisionHandler;
        //public Action<Collider, Collider> BotCollisionHandler;
        //public Action<Collider, Collider> RightCollisionHandler;
        //public Action BeforeCollisionHandler;


        [Obsolete]
        public Collider()
        {
            Reset();
        }

        //private static readonly Action<Collider, Collider> DefaultCollision = (a, b) => { };
        //private static readonly Action DefaultBeforeCollision = () => { };

        public void Destroy()
        {
            Parent.Colliders.Remove(this);
            Reset();

            Pool.Return(this);
        }

        private void Reset()
        {
            Parent = null;
            Area = Rectangle.Empty;

            Handler = CollisionHandler.Empty;

            //BeforeCollisionHandler = DefaultBeforeCollision;

            //TopCollisionHandler =
            //    BotCollisionHandler =
            //    LeftCollisionHandler =
            //    RightCollisionHandler =
            //    DefaultCollision;
        }

        public static Collider Create(GameObject obj)
        {
            currentCollider = Pool.Get();
            currentCollider .Parent = obj;
            obj.Colliders.Add(currentCollider);
            return currentCollider;
        }
    }
}
