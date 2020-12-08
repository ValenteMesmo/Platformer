using System;

namespace Platformer.Desktop
{
    public class CollisionHandler
    {
       public Action<Collider, Collider> Top = null;
       public Action<Collider, Collider> Bot = null;
       public Action<Collider, Collider> Left = null;
       public Action<Collider, Collider> Right = null;

        private static readonly Action<Collider, Collider> noHandler = (a, b) => { };
        private static readonly Pool<CollisionHandler> Pool = new Pool<CollisionHandler>();

        public static readonly CollisionHandler Empty = Pool.Get();

        [Obsolete]
        public CollisionHandler()
        {
            Top = Bot = Left = Right = noHandler;
        }

        public static CollisionHandler Create() => Pool.Get();

        public void Destroy()
        {
            Top = Bot = Left = Right = noHandler;

            Pool.Return(this);
        }
    }
}
