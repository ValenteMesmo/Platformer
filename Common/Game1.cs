using Microsoft.Xna.Framework.Content;
using System;

namespace Platformer.Desktop
{
    public class Game1 : Game
    {
        public override void LoadContent(ContentManager Content)
        {
            Camera.Zoom = 0.006f;
            Camera.Position.X = 500 * Const.Scale;

            Textures.Load(Content);

            for (var i = 0; i < 7; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = 200 * Const.Scale * 2;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * i);
                AddObject(currentObj);
            }

            for (var i = 0; i < 7; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Const.Scale;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * i);
                AddObject(currentObj);
            }

            for (var i = 0; i < 6; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Const.Scale + (200 * Const.Scale * i);
                currentObj.Position.X = -400 * Const.Scale;
                AddObject(currentObj);
            }

            for (var i = 0; i < 6; i++)
            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -600 * Const.Scale + (200 * Const.Scale * i);
                currentObj.Position.X = -400 * Const.Scale + (200 * Const.Scale * 8);
                AddObject(currentObj);
            }

            {
                var currentObj = Block.Create();
                currentObj.Position.Y = 200 * Const.Scale;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * 3);
                AddObject(currentObj);
            }

            {
                var currentObj = Block.Create();
                currentObj.Position.Y = 0;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * 4);
                AddObject(currentObj);
            }

            {
                var currentObj = Block.Create();
                currentObj.Position.Y = 200 * Const.Scale;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * 5);
                AddObject(currentObj);
            }

            {
                var currentObj = Block.Create();
                currentObj.Position.Y = -400 * Const.Scale; ;
                currentObj.Position.X = -200 * Const.Scale + (200 * Const.Scale * 3);
                AddObject(currentObj);
            }
            var playerState = ValueKeeper<State>.Create();
            AddActiveObjects(Player.Create(Player1Inputs, playerState));

            //move to otherFile
            var fps = GameObject.Create();
            var fpsText = Textures.text;
            fpsText.scale = 1000;
            fps.RenderHandler = fpsText;
            fps.UpdateHandler = () => fpsText.Text = this.CurrentFramesPerSecond.ToString("N1");
            AddActiveObjects(fps);

            var playerStateDraw = GameObject.Create();
            playerStateDraw.Position.Y = 12000;
            var stateText = Textures.text2;
            stateText.scale = 1000;
            playerStateDraw.RenderHandler = stateText;
            playerStateDraw.UpdateHandler = () => stateText.Text = playerState.ToString();
            AddActiveObjects(playerStateDraw);
        }
    }
}
