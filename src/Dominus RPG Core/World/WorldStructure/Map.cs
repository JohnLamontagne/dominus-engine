using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Map
    {
        private Tile[,] _tiles;
        private SpawnInformation _spawnInformation;

        public Point Size { get { return new Point(_tiles.GetLength(0), _tiles.GetLength(1)); } }

        public SpawnInformation SpawnInformation { get { return _spawnInformation; } }

        public Map(Point mapSize)
        {
            _tiles = new Tile[mapSize.X, mapSize.Y];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < _tiles.GetLength(0); x++)
            {
                for (int y = 0; y < _tiles.GetLength(1); y++)
                {
                    foreach (var layer in _tiles[x, y].GetLayers())
                        spriteBatch.Draw(layer.Sprite, new Vector2(x * 32, y * 32), Color.White);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }

}