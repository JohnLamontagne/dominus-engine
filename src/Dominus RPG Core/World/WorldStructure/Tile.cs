using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Tile
    {
        private TileLayer[] _tileLayers;

        public Tile()
        {
            _tileLayers = new TileLayer[1];
        }

        public TileLayer[] GetLayers()
        {
            return _tileLayers;
        }
    }
}