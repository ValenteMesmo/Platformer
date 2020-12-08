using Microsoft.Xna.Framework.Content;
using System;

namespace Platformer.Desktop
{
    public class Game1 : Game
    {
        public override void LoadContent(ContentManager Content)
        {
            Camera.Zoom = 0.006f;
            Camera.Position.X = 500 * Constant.Scale;

            Textures.Load(Content);

            for (var i = 0; i < 7; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = 200  *Constant.Scale;
                currentObj.Position.X = -200 * Constant.Scale + (200 * Constant.Scale * i);
                AddObject(currentObj);
            }

            for (var i = 0; i < 7; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Constant.Scale;
                currentObj.Position.X = -200 * Constant.Scale + (200 * Constant.Scale * i);
                AddObject(currentObj);
            }

            for (var i = 0; i < 5; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Constant.Scale + (200 * Constant.Scale * i);
                currentObj.Position.X = -400 * Constant.Scale;
                AddObject(currentObj);
            }

            for (var i = 0; i < 5; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Constant.Scale + (200 * Constant.Scale * i);
                currentObj.Position.X = -400 * Constant.Scale + (200 * Constant.Scale * 8);
                AddObject(currentObj);
            }

            AddActiveObjects(Player.Create(Player1Inputs));

            var fps = GameObject.Create();
            var text = Textures.text;
            text.scale = 1000;
            fps.RenderHandler = text;
            text.Text = this.CurrentFramesPerSecond.ToString();
            AddObject(fps);


        }        
    }
}
