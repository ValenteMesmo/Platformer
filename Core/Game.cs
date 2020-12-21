using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public abstract class Game : IDisposable
    {
        private readonly GameWrapper originalGameInstance;
        public Camera Camera = null;
        internal List<GameObject> Objects = null;
        internal List<GameObject> ActiveObjects = null;
        public InputController Player1Inputs = null;
        public double CurrentFramesPerSecond = 60;

        public Game()
        {
            Objects = new List<GameObject>();
            ActiveObjects = new List<GameObject>();
            Camera = new Camera() { Zoom = 0.72f, Position = new Point(500, 0) };
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
            Objects.Add(gameObject);
        }
    }
}
