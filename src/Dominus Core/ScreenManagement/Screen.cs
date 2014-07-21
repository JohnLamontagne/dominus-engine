using Dominus_Core.Graphics.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominus_Core.ScreenManagement
{
    public abstract class Screen
    {
        /// <summary>
        /// Specifes whether the screen will continue to update even when it is not active.
        /// </summary>
        public bool SilentUpdate { get; set; }

        /// <summary>
        /// The GUI handler for this particular screen.
        /// </summary>
        public GUIHandler GUIHandler { get; private set; }

        private readonly Dictionary<string, IGameObject> _gameObjects;

        public Screen()
        {
            _gameObjects = new Dictionary<string, IGameObject>();
            this.GUIHandler = new GUIHandler();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var gameObject in _gameObjects.Values)
                gameObject.Update(gameTime);

            this.GUIHandler.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.Values)
                gameObject.Draw(spriteBatch);

            this.GUIHandler.Draw(spriteBatch);
        }

        public void AddGameObject(IGameObject gameObject, string name)
        {
            _gameObjects.Add(name, gameObject);
        }

        public void RemoveGameObject(string name)
        {
            _gameObjects.Remove(name);
        }

        public void RemoveGameObject(IGameObject value)
        {
            var name = _gameObjects.FirstOrDefault(x => x.Value == value).Key;

            this.RemoveGameObject(name);
        }

        public IGameObject[] GetGameObjects()
        {
            var values = new IGameObject[_gameObjects.Count];

            _gameObjects.Values.CopyTo(values, 0);

            return values;
        }

        public T GetGameObject<T>(string name) where T : IGameObject
        {
            IGameObject value;

            if (_gameObjects.TryGetValue(name, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default(T);
        }

    }
}