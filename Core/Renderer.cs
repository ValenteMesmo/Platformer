using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Desktop
{
    public abstract class Renderer
    {
        public abstract void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent);

        public abstract void Destroy();
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

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
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
