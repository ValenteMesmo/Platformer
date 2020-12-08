using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonogameGame = Microsoft.Xna.Framework.Game;

namespace Platformer.Desktop
{
    public abstract class Game : IDisposable
    {
        private readonly GameWrapper originalGameInstance;
        public Camera Camera = null;
        internal List<GameObject> Objects = null;
        internal List<GameObject> ActiveObjects = null;
        public GameInputs Player1Inputs = null;
        public double CurrentFramesPerSecond = 60;

        public Game()
        {
            Objects = new List<GameObject>();
            ActiveObjects = new List<GameObject>();
            Camera = new Camera() { Zoom = 0.72f, Position = new Point(500, 0) };
            Player1Inputs = new GameInputs();
            originalGameInstance = new GameWrapper(this);
        }

        public void Run()
        {
            originalGameInstance.Run();
        }

        public abstract void LoadContent(ContentManager content);

        public void Dispose()
        {
            originalGameInstance.Dispose();
        }

        protected void AddActiveObjects(GameObject gameObject)
        {
            ActiveObjects.Add(gameObject);
        }

        protected void AddObject(GameObject gameObject)
        {
            Objects.Add(gameObject);
        }
    }

    public class GameWrapper : MonogameGame
    {
        GraphicsDeviceManager graphics = null;
        SpriteBatch spriteBatch = null;
        private KeyboardState key ;
        private GamePadState gamePad ;
        private Texture2D pixel = null;

        private readonly Game Parent;
        GameObject currentObject = null;
        Collider currentCollider = null;
        int i;
        int j;
        int k;
        int l;
        private DateTime previousUpdate;
        private DateTime currentUpdate;
        private DateTime actualCurrentUpdate;
        private double delta;
        private const double frameRate = 0.01666666666;
        private double accumulator;

        public GameWrapper(Game Parent)
        {
            this.Parent = Parent;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.Window.Title = "Platformer";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Parent.LoadContent(Content);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            previousUpdate = currentUpdate;
            currentUpdate = DateTime.Now;
            delta = (currentUpdate - previousUpdate).TotalSeconds;
            if (delta > 0.25)
                delta = 0.25;

            accumulator += delta;

            if (accumulator >= frameRate)
            {
                Parent.CurrentFramesPerSecond = Parent.CurrentFramesPerSecond * 0.99 + ((1 / ((currentUpdate - actualCurrentUpdate).TotalSeconds))) * 0.01;
                actualCurrentUpdate = currentUpdate;

                while (accumulator >= frameRate)
                {
                    ActualUpdate();
                    accumulator -= frameRate;
                }
            }
            else
                SuppressDraw();

            base.Update(gameTime);
        }

        private void ActualUpdate()
        {
            key = Keyboard.GetState();
            gamePad = GamePad.GetState(0);

            if (key.IsKeyDown(Keys.A) || gamePad.IsButtonDown(Buttons.DPadLeft))
            {
                Parent.Player1Inputs.Left++;
                if (Parent.Player1Inputs.Left > 10)
                    Parent.Player1Inputs.Left = 10;
            }
            else
            {
                Parent.Player1Inputs.Left--;
                if (Parent.Player1Inputs.Left < 0)
                    Parent.Player1Inputs.Left = 0;
            }

            if (key.IsKeyDown(Keys.D) || gamePad.IsButtonDown(Buttons.DPadRight))
            {
                Parent.Player1Inputs.Right++;
                if (Parent.Player1Inputs.Right > 10)
                    Parent.Player1Inputs.Right = 10;
            }
            else
            {
                Parent.Player1Inputs.Right--;
                if (Parent.Player1Inputs.Right < 0)
                    Parent.Player1Inputs.Right = 0;
            }

            if (key.IsKeyDown(Keys.Space) || gamePad.IsButtonDown(Buttons.A))
            {
                Parent.Player1Inputs.Jump++;
                if (Parent.Player1Inputs.Jump > 10)
                    Parent.Player1Inputs.Jump = 10;
            }
            else
            {
                Parent.Player1Inputs.Jump--;
                if (Parent.Player1Inputs.Jump < 0)
                    Parent.Player1Inputs.Jump = 0;
            }


            for (i = 0; i < Parent.ActiveObjects.Count; i++)
            {
                currentObject = Parent.ActiveObjects[i];
                currentObject.UpdateHandler();

                currentObject.Position.Y += currentObject.Velocity.Y;

                //if (!currentObject.IsPassive)
                for (j = 0; j < currentObject.Colliders.Count; j++)
                {
                    currentCollider = currentObject.Colliders[j];

                    //currentCollider.BeforeCollisionHandler();
                    for (k = 0; k < Parent.Objects.Count; k++)
                        for (l = 0; l < Parent.Objects[k].Colliders.Count; l++)
                            CheckCollisions(
                                CollisionDirection.Vertical
                                , currentCollider
                                , Parent.Objects[k].Colliders[l]);
                }

                currentObject.Position.X += currentObject.Velocity.X;
                //if (!currentObject.IsPassive)
                for (j = 0; j < currentObject.Colliders.Count; j++)
                {
                    currentCollider = currentObject.Colliders[j];

                    //currentCollider.BeforeCollisionHandler();
                    for (k = 0; k < Parent.Objects.Count; k++)
                        for (l = 0; l < Parent.Objects[k].Colliders.Count; l++)
                            CheckCollisions(
                                CollisionDirection.Horizontal
                                , currentCollider
                                , Parent.Objects[k].Colliders[l]);
                }
            }
        }

        private void CheckCollisions(CollisionDirection direction, Collider source, Collider target)
        {
            //var targets = quadtree.Get(source);

            //for (int i = 0; i < targets.Length; i++)
            {
#if DEBUG
                if (source.Parent == null || target.Parent == null)
                    throw new Exception("Collider parent cannot be null!");
#endif

                if (source.Parent == target.Parent)
                    return;

                if (direction == CollisionDirection.Vertical)
                    source.IsCollidingV(target);
                else
                    source.IsCollidingH(target);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                SpriteSortMode.Deferred
                , BlendState.NonPremultiplied
                , SamplerState.LinearClamp
                , DepthStencilState.Default
                , RasterizerState.CullNone
                , null
                , Parent.Camera.GetTransformation(GraphicsDevice)
            );

            for (i = 0; i < Parent.Objects.Count; i++)
            {
                Parent.Objects[i].RenderHandler.Draw(spriteBatch, spriteBatch, Parent.Objects[i]);
                for (j = 0; j < Parent.Objects[i].Colliders.Count; j++)
                {
                    DrawBorder(Parent.Objects[i].Colliders[j].RelativeArea, 1000, Color.Red, spriteBatch);
                }

            }

            for (i = 0; i < Parent.ActiveObjects.Count; i++)
            {
                Parent.ActiveObjects[i].RenderHandler.Draw(spriteBatch, spriteBatch, Parent.ActiveObjects[i]);

                for (j = 0; j < Parent.ActiveObjects[i].Colliders.Count; j++)
                {
                    DrawBorder(Parent.ActiveObjects[i].Colliders[j].RelativeArea, 1000, Color.Red, spriteBatch);
                }

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
