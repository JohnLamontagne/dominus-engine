using Dominus_Core.Utilities;
using Dominus_Core.World.Entities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_Core.World.WorldStructure
{
    public class WorldView
    {
        public bool Active { get; set; }

        public bool CanRender { get; set; }

        public Map Map { get; private set; }

        private readonly Dictionary<string, IEntity> _entities;

        private readonly Dictionary<string, IGameObject> _gameObjects;

        private Camera _camera;

        public WorldView(Camera camera)
        {
            _entities = new Dictionary<string, IEntity>();
            _gameObjects = new Dictionary<string, IGameObject>();
            _camera = camera;
        }

        public T GetEntity<T>(string id) where T : IEntity
        {
            IEntity value;

            if (_entities.TryGetValue(id, out value))
            {
                if (typeof(T) == value.GetType())
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public void AddEntity(IEntity entity, string id)
        {
            _entities.Add(id, entity);
        }

        public void RemoveEntity(string id)
        {
            _entities.Remove(id);
        }

        public T GetGameObject<T>(string id) where T : IGameObject
        {
            IGameObject value;

            if (_gameObjects.TryGetValue(id, out value))
            {
                if (typeof(T) == value.GetType())
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public void AddGameObject(IGameObject gameObject, string id)
        {
            _gameObjects.Add(id, gameObject);
        }

        public void RemoveGameObject(string id)
        {
            _gameObjects.Remove(id);
        }

        public void Update(GameTime gameTime)
        {
            // Update the map.
            if (this.Map != null)
                this.Map.Update(gameTime);

            foreach (var gameObject in _gameObjects.Values)
                gameObject.Update(gameTime);

            // Update the entities on the map.
            foreach (var entity in _entities.Values)
                entity.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the map.
            if (this.Map != null)
                this.Map.Draw(spriteBatch);

            foreach (var gameObject in _gameObjects.Values)
                gameObject.Draw(spriteBatch);

            // Draw the entities on the map
            foreach (var entity in _entities.Values)
                entity.Draw(spriteBatch);


        }

        public void SwitchMap(Map newMap)
        {
            this.Map = newMap;
        }

        public void Unload()
        {
            _gameObjects.Clear();
            _entities.Clear();
        }
    }
}