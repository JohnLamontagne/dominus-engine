using Dominus_Utilities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TiledSharp;

namespace Dominus_RPG_Core.World.WorldStructure.TiledSharp
{
    public class Tileset
    {
        private readonly TmxTileset _tileset;
        private readonly Texture2D _texture;
        private readonly int _lastGid;
        private readonly int _tilesPerRow;

        public Texture2D Texture { get { return _texture; } }

        public int FirstGid { get { return _tileset.FirstGid; } }

        public int LastGid { get { return _lastGid; } }

        public int TilesPerRow { get { return _tilesPerRow; } }

        public int TileWidth { get { return (int)_tileset.TileWidth; } }

        public int TileHeight { get { return (int)_tileset.TileHeight; } }

        public Tileset(TmxTileset tileset, ContentManager content)
        {
            _tileset = tileset;

            _lastGid = (this.FirstGid - 1) + (((int)tileset.Image.Height / tileset.TileHeight) * ((int)tileset.Image.Width / tileset.TileWidth));

            _tilesPerRow = (int)tileset.Image.Width / tileset.TileWidth;

            if (tileset.Image.Height > 2048 || tileset.Image.Width > 2048)
                throw new Exception("Textures that are greater than 2048x2048 are not supported!");

            // Load the tileset's texture.
            _texture = ContentManagerUtilities.LoadTexture2D(content, tileset.Image.Source);
        }
    }
}