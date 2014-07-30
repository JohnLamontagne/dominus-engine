using Dominus_Core.Graphics.GUI;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public ContentManager Content { get; private set; }

        private readonly OrderedDictionary<string, IGameObject> _gameObjects;

        public Screen(ContentManager content)
        {
            _gameObjects = new OrderedDictionary<string, IGameObject>();
            this.Content = content;
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

        public virtual void AddGameObject(IGameObject gameObject, string name)
        {
            _gameObjects.Add(name, gameObject);
        }

        public virtual void RemoveGameObject(string name)
        {
            _gameObjects.Remove(name);
        }

        public virtual void RemoveGameObject(IGameObject value)
        {
            var name = _gameObjects.FirstOrDefault(x => x.Value == value).Key;

            this.RemoveGameObject(name);
        }

        public virtual OrderedDictionary<string, IGameObject> GetGameObjects()
        {
            return _gameObjects;
        }

        public virtual T GetGameObject<T>(string name) where T : IGameObject
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