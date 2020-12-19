using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    //todo: make addobject method internal and call it here... and remove on destroy
    public class GameObject
    {
        public Point Position;
        public Point Velocity;
        int i;

        //public static readonly Action NoUpdate = () => { };
        //public static readonly Action<SpriteBatch, SpriteBatch> NoDraw = (a, b) => { };

        internal readonly List<Collider> Colliders = null;
        //internal readonly List<Renderer> Renderers = null;
        //internal readonly List<Action<GameObject>> Updates = null;

        private static Pool<GameObject> Pool = new Pool<GameObject>();
        public int Identifier;

        public Renderer RenderHandler = null;
        public Action UpdateHandler = null;
        private static GameObject current = null;
        private static readonly Action EmptyHandler = () => { };

        public Action OnDestroy  = null;

        public static GameObject Create()
        {
            current = Pool.Get();

            current.UpdateHandler = EmptyHandler;
            current.OnDestroy = EmptyHandler;
            return current;
        }

        //public Action Update = NoUpdate;
        //public Action<SpriteBatch, SpriteBatch> Draw = NoDraw;

        [Obsolete]
        public GameObject()
        {
            Colliders = new List<Collider>();
            //Renderers = new List<Renderer>();
            //Updates = new List<Action<GameObject>>();
        }

        public void Destroy()
        {
            for (i = 0; i < Colliders.Count; i++)
                Colliders[i].Destroy();
            //Colliders.Clear();

            //foreach (var renderer in Renderers)
            //    renderer.Destroy();
            //Renderers.Clear();
            RenderHandler.Destroy();

            Identifier = 0;
            UpdateHandler = null;

            Velocity = Position = Point.Zero;

            Pool.Return(this);
        }
    }
}
