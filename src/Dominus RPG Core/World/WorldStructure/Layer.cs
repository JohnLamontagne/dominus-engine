using Dominus_Core;
using Dominus_RPG_Core.World.Entities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Layer
    {
        private readonly Tile[] _tiles;
        private readonly OrderedDictionary<string, IGameObject> _gameObjects;
        private readonly LayerTypes _layerType;

        public int ZOrder { get; private set; }

        public LayerTypes LayerType { get { return _layerType; } }

        public Layer(Tile[] tiles, int zOrder)
        {
            _tiles = tiles;
            _layerType = LayerTypes.TileLayer;

            this.ZOrder = zOrder;
        }

        public Layer(OrderedDictionary<string, IGameObject> gameObjects, int zOrder)
        {
            _gameObjects = gameObjects;

            _layerType = LayerTypes.ObjectLayer;

            this.ZOrder = zOrder;
        }

        public void Update(GameTime gameTime)
        {
            if (this.LayerType == LayerTypes.TileLayer)
            {
            }
            else
            {
                foreach (var gameObject in _gameObjects.Values)
                    gameObject.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.LayerType == LayerTypes.TileLayer)
            {
                for (int i = 0; i < _tiles.Length; i++)
                {
                    _tiles[i].Draw(spriteBatch);
                }
            }
            else
            {
                foreach (var gameObject in _gameObjects.Values)
                    gameObject.Draw(spriteBatch);
            }
        }

        public OrderedDictionary<string, IGameObject> GetGameObjects()
        {
            return _gameObjects;
        }
    }
}