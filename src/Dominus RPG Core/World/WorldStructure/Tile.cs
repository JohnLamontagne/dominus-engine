using Dominus_RPG_Core.World.WorldStructure.TiledSharp;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Tile
    {
        private readonly Tileset _tileset;
        private readonly Vector2 _position;
        private readonly Rectangle _sourceRect;
        private readonly Rectangle _destRect;
        private readonly byte _opacity;

        public Tile(Tileset tileset, Rectangle sourceRect, Vector2 position, double opacity)
        {
            _tileset = tileset;
            _position = position;
            _sourceRect = sourceRect;

            _opacity = (byte)(opacity * 255);

            if (_tileset != null)
                _destRect = new Rectangle((int)(position.X), (int)(position.Y), tileset.TileWidth, tileset.TileHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_tileset != null)
            {
                spriteBatch.Draw(_tileset.Texture, _destRect, _sourceRect, Color.White * _opacity);
            }
        }
    }
}