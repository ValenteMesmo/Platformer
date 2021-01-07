using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public abstract class Game : IDisposable
    {
        private readonly GameWrapper originalGameInstance;
        public Camera WorldCamera = null;
        public Camera GuiCamera = null;
        internal List<GameObject> PassiveObjects = null;
        internal List<GameObject> ActiveObjects = null;
        internal List<GameObject> GuiObjects = null;
        public InputController Player1Inputs = null;
        public double CurrentFramesPerSecond = 60;

        public Game()
        {
            PassiveObjects = new List<GameObject>();
            ActiveObjects = new List<GameObject>();
            GuiObjects = new List<GameObject>();
            WorldCamera = new Camera() { Zoom = 0.72f, Position = new Point(500, 0) };
            GuiCamera = new Camera() { Zoom = 1f, Position = new Point(681, 381) };
            Player1Inputs = new InputController();
            //Player1Inputs.ColliderToggle.Press();
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
            PassiveObjects.Add(gameObject);
        }

        protected void AddGuiObject(GameObject gameObject)
        {
            GuiObjects.Add(gameObject);
        }

        public T GetService<T>() where T : class
        {
            return originalGameInstance.Services.GetService<T>();
        }
    }
}
