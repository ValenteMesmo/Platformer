using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonogameGame = Microsoft.Xna.Framework.Game;

namespace Platformer.Desktop
{
    public class GameWrapper : MonogameGame
    {
        GraphicsDeviceManager graphics = null;
        SpriteBatch spriteBatch = null;
        private KeyboardState key;
        private GamePadState gamePad;
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
        public const double frameRate = 1.0 / 60.0;
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

            if (false)
            {
                graphics.IsFullScreen = true;
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.ApplyChanges();
            }
            Window.Title = "Platformer";
            previousUpdate = DateTime.Now;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            InactiveSleepTime = new TimeSpan(0);
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
            if (delta > 0.27)
                delta = 0.27;

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
            {
                for (i = 0; i < Parent.ActiveObjects.Count; i++)
                {
                    currentObject = Parent.ActiveObjects[i];

                    currentObject.PreviousPosition.X = (int)MathHelper.Lerp(currentObject.PreviousPosition.X, currentObject.Position.X, .5f);
                    currentObject.PreviousPosition.Y = (int)MathHelper.Lerp(currentObject.PreviousPosition.Y, currentObject.Position.Y, .5f);
                }
                //SuppressDraw();
            }

            base.Update(gameTime);
        }

        private void ActualUpdate()
        {
            key = Keyboard.GetState();
            gamePad = GamePad.GetState(0);

            if (key.IsKeyDown(Keys.A) || gamePad.IsButtonDown(Buttons.DPadLeft))
                Parent.Player1Inputs.Left.Press();
            else
                Parent.Player1Inputs.Left.Release();

            if (key.IsKeyDown(Keys.D) || gamePad.IsButtonDown(Buttons.DPadRight))
                Parent.Player1Inputs.Right.Press();
            else
                Parent.Player1Inputs.Right.Release();

            if (key.IsKeyDown(Keys.Space) || gamePad.IsButtonDown(Buttons.A))
                Parent.Player1Inputs.Jump.Press();
            else
                Parent.Player1Inputs.Jump.Release();

            if (key.IsKeyDown(Keys.Z) || gamePad.IsButtonDown(Buttons.B))
                Parent.Player1Inputs.Dash.Press();
            else
                Parent.Player1Inputs.Dash.Release();

            if (key.IsKeyDown(Keys.F10))
                Parent.Player1Inputs.ColliderToggle.Press();
            else
                Parent.Player1Inputs.ColliderToggle.Release();

            for (i = 0; i < Parent.ActiveObjects.Count; i++)
            {
                currentObject = Parent.ActiveObjects[i];
                currentObject.UpdateHandler();

                currentObject.PreviousPosition = currentObject.Position;

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
                , DepthStencilState.None
                , RasterizerState.CullNone
                , null
                , Parent.Camera.GetTransformation(GraphicsDevice)
            );

            for (i = 0; i < Parent.Objects.Count; i++)
            {
                Parent.Objects[i].RenderHandler.Draw(spriteBatch, spriteBatch, Parent.Objects[i]);

                if (Parent.Player1Inputs.ColliderToggle.IsToogled)
                    for (j = 0; j < Parent.Objects[i].Colliders.Count; j++)
                        DrawBorder(Parent.Objects[i].Colliders[j].RelativeArea, 600, Color.Red, spriteBatch);

            }

            for (i = 0; i < Parent.ActiveObjects.Count; i++)
            {
                Parent.ActiveObjects[i].RenderHandler.Draw(spriteBatch, spriteBatch, Parent.ActiveObjects[i]);

                if (Parent.Player1Inputs.ColliderToggle.IsToogled)
                    for (j = 0; j < Parent.ActiveObjects[i].Colliders.Count; j++)
                        DrawBorder(Parent.ActiveObjects[i].Colliders[j].RelativeArea, 600, Color.Red, spriteBatch);

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
