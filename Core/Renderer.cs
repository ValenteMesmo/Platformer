using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public abstract class Renderer
    {
        public abstract void Draw(SpriteBatch batch, GameObject Parent);

        public abstract void Destroy();
    }

    public class RenderGroup : Renderer
    {
        private static Pool<RenderGroup> Pool = new Pool<RenderGroup>();

        private List<Renderer> renderes = null;

        private RenderGroup()
        {
            renderes = new List<Renderer>();
            Reset();
        }

        private void Reset()
        {
            foreach (var item in renderes)
                item.Destroy();

            renderes.Clear();
        }

        public override void Destroy()
        {
            Reset();

            Pool.Return(this);
        }

        public static RenderGroup Create(params Renderer[] renderers)
        {
            var group = Pool.Get();
            group.renderes.AddRange(renderers);
            return group;
        }

        public override void Draw(SpriteBatch batch, GameObject Parent)
        {
            foreach (var item in renderes)
            {
                item.Draw(batch, Parent);
            }
        }
    }

    public class TextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public float scale;
        public Color Color;
        public Point Offset;

        private static Pool<TextRenderer> Pool = new Pool<TextRenderer>();

        public TextRenderer()
        {
            Reset();
        }

        private void Reset()
        {
            Color = Color.Red;
            scale = 10;
            Offset = Point.Zero;
            Text = "";
        }

        public override void Destroy()
        {
            Reset();
            Pool.Return(this);
        }

        public static TextRenderer Create()
        {
            return Pool.Get();
        }

        public override void Draw(SpriteBatch batch, GameObject Parent)
        {
            batch.DrawString(
                Font
                , Text
                , (Parent.Position + Offset).ToVector2()
                , Color
                , 0
                , Vector2.Zero
                , scale
                , SpriteEffects.None
                , 0
            );
        }
    }
}
