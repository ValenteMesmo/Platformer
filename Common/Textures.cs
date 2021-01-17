using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer.Desktop
{
    public static class Textures
    {
        public static Func<SpriteRenderer> create_dpad;
        public static Func<SpriteRenderer> create_bpad;
        private static Texture2D dpad_texture;
        private static Texture2D bpad_texture;
        public static SpriteRenderer idle;
        public static SpriteRenderer crouch;
        public static SpriteRenderer lookUp;
        public static SpriteRenderer walk;
        public static Func<SpriteRenderer> create_block;
        private static Texture2D block;
        public static SpriteRenderer jump;
        public static SpriteRenderer head_bump;
        //TODO: fix this...
        public static TextRenderer text;
        public static TextRenderer text2;

        public static void Load(ContentManager Content)
        {
            dpad_texture = Content.Load<Texture2D>("dpad");
            create_dpad = () =>
            {
                var dpad = SpriteRenderer.Create();
                dpad.Texture = dpad_texture;
                dpad.Size = TouchPadController.TouchArea.Size;
                return dpad;
            };
            bpad_texture = Content.Load<Texture2D>("bpad");
            create_bpad = () =>
            {
                var bpad = SpriteRenderer.Create();
                bpad.Texture = bpad_texture;
                bpad.Size = TouchPadController.TouchArea2.Size;
                return bpad;
            };


            idle = SpriteRenderer.Create();
            idle.Texture = Content.Load<Texture2D>("idle");
            idle.Size = new Point(200 * Const.Scale, 200 * Const.Scale);

            crouch = SpriteRenderer.Create();
            crouch.Texture = Content.Load<Texture2D>("idle");
            crouch.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
            crouch.Color = Color.Blue;

            lookUp = SpriteRenderer.Create();
            lookUp.Texture = Content.Load<Texture2D>("idle");
            lookUp.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
            lookUp.Color = Color.Yellow;

            walk = SpriteRenderer.Create();
            walk.Texture = Content.Load<Texture2D>("walk");
            walk.Size = new Point(200 * Const.Scale, 200 * Const.Scale);

            block = Content.Load<Texture2D>("block");

            create_block = () =>
            {
                var result = SpriteRenderer.Create();
                result.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
                result.Texture = block;
                return result;
            };

            jump = SpriteRenderer.Create();
            jump.Texture = Content.Load<Texture2D>("jump");
            jump.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
            jump.Offset = new Point(0, 36 * Const.Scale);

            head_bump = SpriteRenderer.Create();
            head_bump.Texture = Content.Load<Texture2D>("head_bump");
            head_bump.Size = new Point(200 * Const.Scale, 200 * Const.Scale);
            head_bump.Offset = new Point(0, 12 * Const.Scale);


            text = TextRenderer.Create();
            text.Font = Content.Load<SpriteFont>("font");

            text2 = TextRenderer.Create();
            text2.Font = Content.Load<SpriteFont>("font");
        }
    }
}
