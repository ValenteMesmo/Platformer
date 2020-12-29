using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Desktop
{
    public class Game1 : Game
    {
        public override void LoadContent(ContentManager Content)
        {
            WorldCamera.Zoom = 0.006f;
            WorldCamera.Position.X = 500 * Const.Scale;

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
            AddActiveObjects(
                Player.Create(
                    Player1Inputs
                    , playerState
                    , ValueKeeper<int>.Create()
                    , ValueKeeper<int>.Create()
                    , ValueKeeper<int>.Create()
                    , ValueKeeper<bool>.Create()));

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

            {
                var dpadText1 = Textures.create_dpad();
                var dpadText2 = Textures.create_dpad();
                dpadText1.Color = new Color(0, 0, 0, 90);
                dpadText2.Color = new Color(240, 240, 240);
                dpadText1.Offset = new Point(6, 6);
                var dpad = GameObject.Create();
                dpad.RenderHandler = RenderGroup.Create(dpadText1, dpadText2);
                dpad.Position = new Point(0, 460);
                AddGuiObject(dpad);
            }

            {
                var dpadText1 = Textures.create_dpad();
                var dpadText2 = Textures.create_dpad();
                dpadText1.Color = new Color(0,0,0,90);
                dpadText2.Color = new Color(240,240,240);
                dpadText1.Offset = new Point(6, 6);
                var dpad = GameObject.Create();
                dpad.RenderHandler = RenderGroup.Create(dpadText1, dpadText2);
                dpad.Position = new Point(1060,460);
                
                AddGuiObject(dpad);
            }

            {
                //var preview_head = GameObject.Create();
                //preview_head.RenderHandler = Textures.head_bump;
                //preview_head.Position.X = 200 * Const.Scale * 3;
                //AddObject(preview_head);
            }
        }
    }
}
