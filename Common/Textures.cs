using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Desktop
{
    public static class Textures
    {
        public static SpriteRenderer idle;
        public static SpriteRenderer walk;
        public static SpriteRenderer block;
        public static SpriteRenderer jump;
        //TODO: fix this...
        public static TextRenderer text;
        public static TextRenderer text2;

        public static void Load(ContentManager Content)
        {
            idle = SpriteRenderer.Create();
            idle.Texture = Content.Load<Texture2D>("idle");
            idle.Size = new Point(200*Const.Scale, 200* Const.Scale);

            walk = SpriteRenderer.Create();
            walk.Texture = Content.Load<Texture2D>("walk");
            walk.Size = new Point(200 * Const.Scale, 200 * Const.Scale);

            block = SpriteRenderer.Create();
            block.Texture = Content.Load<Texture2D>("block");
            block.Size = new Point(200 * Const.Scale, 200 * Const.Scale);

            jump = SpriteRenderer.Create();
            jump.Texture = Content.Load<Texture2D>("jump");
            jump.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
            jump.Offset = new Point(0,36 * Const.Scale);

            text = TextRenderer.Create();
            text.Font = Content.Load<SpriteFont>("font");

            text2 = TextRenderer.Create();
            text2.Font = Content.Load<SpriteFont>("font");
        }
    }
}
