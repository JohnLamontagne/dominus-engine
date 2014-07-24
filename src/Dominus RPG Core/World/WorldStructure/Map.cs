using Dominus_Core;
using Dominus_Core.Graphics.Effects.Particles;
using Dominus_RPG_Core.World.WorldStructure.TiledSharp;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TiledSharp;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Map
    {
        private SpawnInformation _spawnInformation;
        private Layer[] _layers;
        private Point _size;

        public Point Size { get { return _size; } }

        public SpawnInformation SpawnInformation { get { return _spawnInformation; } }

        public Map()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i].Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public static Map Load(string filePath, ContentManager content)
        {
            Map map = null;

            var tmxMap = new TmxMap(filePath);

            var layers = new Layer[tmxMap.Layers.Count];

            var tilesets = new Tileset[tmxMap.Tilesets.Count];

            // Load the tilesets.
            for (int i = 0; i < tmxMap.Tilesets.Count; i++)
            {
                tilesets[i] = new Tileset(tmxMap.Tilesets[i], content);
            }

            for (int i = 0; i < tmxMap.Layers.Count; i++)
            {
                var tmxLayer = tmxMap.Layers[i];

                var graphicsDevice = (content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;

                var tiles = new Tile[tmxLayer.Tiles.Count];

                for (int b = 0; b < tmxLayer.Tiles.Count; b++)
                {
                    tiles[b] = LoadTile(tmxMap, tmxLayer.Tiles[b], tilesets, content, graphicsDevice);
                }

                layers[i] = new Layer(tiles);
            }

            SpawnInformation spawnInformation = new SpawnInformation();
            spawnInformation.LoadGameObjects(tmxMap, content);

            map = new Map()
            {
                _layers = layers,
                _size = new Point(tmxMap.Width, tmxMap.Height),
                _spawnInformation = spawnInformation
            };

            return map;
        }

        private static Tile LoadTile(TmxMap tmxMap, TmxLayerTile tmxTile, Tileset[] tilesets, ContentManager content, GraphicsDevice graphicsDevice)
        {
            Tile tile = null;

            if (tmxTile.Gid != 0)
            {
                // Try and find the tileset.
                foreach (var tileset in tilesets)
                {
                    if (tmxTile.Gid >= tileset.FirstGid && tmxTile.Gid <= tileset.LastGid)
                    {
                        int srcY = (((tmxTile.Gid - tileset.FirstGid) / tileset.TilesPerRow) * 32);
                        int srcX = (((tmxTile.Gid - tileset.FirstGid) % tileset.TilesPerRow) * 32);

                        tile = new Tile(tileset, new Rectangle(srcX, srcY, tileset.TileWidth, tileset.TileHeight), new Vector2(tmxTile.X * 32, tmxTile.Y * 32));

                        break;
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }
            }

            return tile ?? new Tile(null, new Rectangle(), Vector2.Zero);
        }
    }
}