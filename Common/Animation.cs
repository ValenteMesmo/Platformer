using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public class Animation : Renderer
    {
        private static Pool<Animation> Pool = new Pool<Animation>();
        public int Frame;
        public List<SpriteRenderer> Sprites = null;
        public bool flipped;
        [Obsolete]
        public Animation()
        {
            Sprites = new List<SpriteRenderer>();
        }

        public static Animation Create()
        {
            return Pool.Get();
        }

        public override void Destroy()
        {
            Frame = 0;
            Sprites.Clear();
            Pool.Return(this);
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            //TODO: this is weird
            Sprites[Frame].flipped = flipped;
            Sprites[Frame].Draw(batchGui, batch, Parent);
        }
    }
}
