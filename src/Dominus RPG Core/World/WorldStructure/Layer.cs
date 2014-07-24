using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class Layer
    {
        private readonly Tile[] _tiles;

        public Layer(Tile[] tiles)
        {
            _tiles = tiles;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i].Draw(spriteBatch);
            }
        }
    }
}