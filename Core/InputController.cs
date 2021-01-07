using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platformer.Desktop
{
    public class TouchController
    {
        public Rectangle TouchArea;
        public Rectangle TouchAreaExtraSize;

        public Point delta;
        public Point center;
        public readonly Point centerOffset = new Point(60, 60);

        public bool Up;
        public bool Down;
        public bool Right;
        public bool Left;

        public TouchController(Rectangle TouchArea)
        {
            this.TouchArea = TouchArea;
            TouchAreaExtraSize = new Rectangle(
                TouchArea.X - 120
                , TouchArea.Y - 120
                , TouchArea.Width + 240
                , TouchArea.Height + 240);
            delta = TouchArea.Center;
            center = TouchArea.Center;
        }

        public void Update(List<Point> clicks)
        {
            Up = Down = Right = Left = false;
            foreach (var touch in clicks)
            {
                if (TouchAreaExtraSize.Contains(touch) == false)
                    continue;

                delta = center - touch;

                var left = delta.X > 0 ? Math.Abs(delta.X) : 0;
                var right = delta.X < 0 ? Math.Abs(delta.X) : 0;
                var up = delta.Y > 0 ? Math.Abs(delta.Y) : 0;
                var down = delta.Y < 0 ? Math.Abs(delta.Y) : 0;

                if (right > left && right > up && right > down)
                {
                    center.X = touch.X - centerOffset.X;
                    center.Y = touch.Y;
                    Right = true;
                }
                else if (left > right && left > up && left > down)
                {
                    center.X = touch.X + centerOffset.X;
                    center.Y = touch.Y;
                    Left = true;
                }
                else if (down > right && down > left && down > up)
                {
                    center.X = touch.X;
                    center.Y = touch.Y - centerOffset.Y;
                    Down = true;
                }
                else if (up > right && up > left && up > down)
                {
                    center.X = touch.X;
                    center.Y = touch.Y + centerOffset.Y;
                    Up = true;
                }
            }

        }
    }

    public class TouchPadController
    {
        public static readonly Rectangle TouchArea = new Rectangle(0, 552, 210, 210);
        public static readonly Rectangle TouchArea2 = new Rectangle(1149, 552, 210, 210);
        //public static readonly Rectangle TouchAreaExtraSize = new Rectangle(
        //    TouchArea.X - 120
        //    , TouchArea.Y - 120
        //    , TouchArea.Width + 240
        //    , TouchArea.Height + 240
        //);
   
    }

    public class InputController
    {
        public InputKey Up;
        public InputKey Down;
        public InputKey Left;
        public InputKey Right;

        public InputKey Jump;
        public InputKey Dash;
        public InputKey ColliderToggle;

        private KeyboardState key;
        private GamePadState gamePad;
        private MouseState mouse;
        private TouchCollection touchPanel;
        internal List<Point> Clicks = null;
        private readonly TouchController TouchController;
        private readonly TouchController TouchController2;

        public InputController()
        {
            Clicks = new List<Point>();
            TouchController = new TouchController(TouchPadController.TouchArea);
            TouchController2 = new TouchController(TouchPadController.TouchArea2);
        }

        public void Update(Camera GuiCamera)
        {
            key = Keyboard.GetState();
            gamePad = GamePad.GetState(0);
            mouse = Mouse.GetState();
            touchPanel = TouchPanel.GetState();

            Clicks.Clear();
            if (mouse.LeftButton == ButtonState.Pressed)
                Clicks.Add(GuiCamera.GetWorldPosition(mouse.Position));

            foreach (var touch in touchPanel)
                Clicks.Add(GuiCamera.GetWorldPosition(touch.Position));

            TouchController.Update(Clicks);
            TouchController2.Update(Clicks);

            if (key.IsKeyDown(Keys.A)
                || gamePad.IsButtonDown(Buttons.DPadLeft)
                || TouchController.Left)
                Left.Press();
            else
                Left.Release();

            if (key.IsKeyDown(Keys.D)
                || gamePad.IsButtonDown(Buttons.DPadRight)
                || TouchController.Right)
                Right.Press();
            else
                Right.Release();

            if (key.IsKeyDown(Keys.Space)
                || gamePad.IsButtonDown(Buttons.A)
                || TouchController2.Down)
                Jump.Press();
            else
                Jump.Release();

            if (key.IsKeyDown(Keys.L)
               || gamePad.IsButtonDown(Buttons.B))
                Dash.Press();
            else
                Dash.Release();

            if (key.IsKeyDown(Keys.F10))
                ColliderToggle.Press();
            else
                ColliderToggle.Release();
        }
    }
}
