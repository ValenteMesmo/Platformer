using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Desktop
{
    public static class Constant
    {
        public const int Scale = 100;
    }

    public static class Textures
    {
        public static SpriteRenderer idle;
        public static SpriteRenderer walk;
        public static SpriteRenderer block;
        public static TextRenderer text;

        public static void Load(ContentManager Content)
        {
            idle = SpriteRenderer.Create();
            idle.Texture = Content.Load<Texture2D>("idle");
            idle.Size = new Point(200*Constant.Scale, 200* Constant.Scale);

            walk = SpriteRenderer.Create();
            walk.Texture = Content.Load<Texture2D>("walk");
            walk.Size = new Point(200 * Constant.Scale, 200 * Constant.Scale);

            block = SpriteRenderer.Create();
            block.Texture = Content.Load<Texture2D>("block");
            block.Size = new Point(200 * Constant.Scale, 200 * Constant.Scale);

            text = TextRenderer.Create();
            text.Font = Content.Load<SpriteFont>("font");
        }
    }
}
