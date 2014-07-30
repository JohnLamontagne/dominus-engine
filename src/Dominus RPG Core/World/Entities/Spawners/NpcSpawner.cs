using Dominus_Core;
using Dominus_RPG_Core.World.Entities.Stats;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_RPG_Core.World.Entities
{
    public class NpcSpawner : IGameObject
    {
        private Texture2D _texture;
        private Vector2 _position;
        private uint _updateInterval;
        private long _nextUpdateTime;
        private OrderedDictionary<string, IGameObject> _gameObjects;
        private List<IEntity> _spawningEntities;

        public NpcSpawner(Texture2D texture, Vector2 position, uint updateInterval, OrderedDictionary<string, IGameObject> gameObjects, List<IEntity> spawningEntites)
        {
            _texture = texture;
            _updateInterval = updateInterval;
            _gameObjects = gameObjects;
            _spawningEntities = spawningEntites;

            // Give the NPC the parent layer's gameobjects.
            // We'll also perform the initial spawn whilst we're at it.
            foreach (Npc npc in _spawningEntities)
            {
                npc.ParentLayerGameObjects = _gameObjects;
                gameObjects.Add(npc.UniqueID, npc);
            }
        }

        public NpcSpawner(Vector2 position, uint updateInterval, OrderedDictionary<string, IGameObject> gameObjects, List<IEntity> spawningEntites)
        {
            _position = position;
            _updateInterval = updateInterval;
            _gameObjects = gameObjects;
            _spawningEntities = spawningEntites;

            // Give the NPC the parent layer's gameobjects.
            // We'll also perform the initial spawn whilst we're at it.
            foreach (Npc npc in _spawningEntities)
            {
                npc.ParentLayerGameObjects = _gameObjects;
                gameObjects.Add(npc.UniqueID, npc);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= _nextUpdateTime)
            {
                // Find any NPCS that need to be spawned.
                foreach (Npc npc in _spawningEntities)
                {
                    // If the NPC is dead, respawn it.
                    if (npc.Dead)
                    {
                        npc.Position = _position;

                        foreach (IStat<object> stat in npc.Stats)
                        {
                            stat.SetPoints(stat.MaxPoints);
                        }

                        // Is it still contained within the layer's entities?
                        // If not, add it back.
                        if (!_gameObjects.ContainsKey(npc.UniqueID))
                        {
                            _gameObjects.Add(npc.UniqueID, npc);
                        }
                    }
                }

                _nextUpdateTime = (long)(gameTime.TotalGameTime.TotalMilliseconds + _updateInterval);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}