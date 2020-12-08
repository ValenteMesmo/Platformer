using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer.Desktop
{
    public class SpriteRenderer : Renderer
    {
        public Texture2D Texture;
        public Color Color;
        public Point Size;
        public Point Offset;
        public Rectangle? Source;
        public Vector2 RotationCenter;
        public float Rotation;

        private Rectangle Target;

        private static Pool<SpriteRenderer> Pool = new Pool<SpriteRenderer>();
        public bool flipped;

        [Obsolete]
        public SpriteRenderer()
        {
            Reset();
        }

        public override void Destroy()
        {
            Reset();
            Pool.Return(this);
        }

        private void Reset()
        {
            Color = Color.White;
            Source = null;
            Target = Rectangle.Empty;
            Texture = null;
            Offset = Size = Point.Zero;
        }

        public static SpriteRenderer Create()
        {
            return Pool.Get();
        }

        public override void Draw(SpriteBatch batchUi, SpriteBatch batch, GameObject Parent)
        {
            Target.Location = Offset + Parent.Position;
            Target.Size = Size;

            batch.Draw(
                Texture
                , Target
                , Source
                , Color
                , Rotation
                , RotationCenter
                , flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                , 0
            );
        }
    }
}
