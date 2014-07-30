using Dominus_RPG_Core.World.WorldStructure.TiledSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using TiledSharp;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Map
    {
        private MapObjectLoader _spawnInformation;
        private Layer[] _layers;
        private Point _size;

        public Point Size { get { return _size; } }

        public MapObjectLoader SpawnInformation { get { return _spawnInformation; } }

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
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i].Update(gameTime);
            }
        }

        public Layer[] GetLayers()
        {
            return _layers;
        }

        public static Map Load(string filePath, ContentManager content)
        {
            Map map = null;

            var tmxMap = new TmxMap(filePath);

            Console.WriteLine("Loading map file: {0}", filePath);

            var layers = new Layer[tmxMap.Layers.Count + tmxMap.ObjectGroups.Count];

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
                    tiles[b] = LoadTile(tmxLayer, tmxLayer.Tiles[b], tilesets, content, graphicsDevice);
                }

                layers[i] = new Layer(tiles, tmxMap.Layers[i].ZOrder);
            }

            MapObjectLoader gameObjectLoader = new MapObjectLoader();
            Layer[] objectLayers = gameObjectLoader.LoadObjectLayers(tmxMap, content);

            // Copy the objectLayers array to the layers array, starting at the first available slot, which is at the end of the tile layers.
            Array.Copy(objectLayers, 0, layers, tmxMap.Layers.Count, objectLayers.Length);

            // Sort the layers properly.
            Array.Sort(layers, (a, b) => a.ZOrder.CompareTo(b.ZOrder));

            map = new Map()
            {
                _layers = layers,
                _size = new Point(tmxMap.Width, tmxMap.Height),
                _spawnInformation = gameObjectLoader
            };

            Console.WriteLine("Finished loading map file: {0}", filePath);

            return map;
        }

        private static Tile LoadTile(TmxLayer tmxLayer, TmxLayerTile tmxTile, Tileset[] tilesets, ContentManager content, GraphicsDevice graphicsDevice)
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

                        tile = new Tile(tileset, new Rectangle(srcX, srcY, tileset.TileWidth, tileset.TileHeight), new Vector2(tmxTile.X * 32, tmxTile.Y * 32), tmxLayer.Opacity);

                        break;
                    }
                }
            }

            return tile ?? new Tile(null, new Rectangle(), Vector2.Zero, tmxLayer.Opacity);
        }
    }
}